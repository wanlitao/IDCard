﻿using System;
using System.IO;
using FCP.Util;
using System.Text;

namespace IDCard.Reader
{
    public abstract class IDCardReader : IIDCardReader
    {
        protected const string DefaultTextFileName = "wz.txt";
        protected const string DefaultPhotoFileName = "xp.wlt";
        protected const string DefaultNewAddressFileName = "newadd.txt";

        protected static string AppDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        #region Helper Functions
        /// <summary>
        /// 解析身份证文本字节
        /// </summary>
        /// <param name="idCardTextBytes"></param>
        /// <returns></returns>
        protected static IDCardData ParseIDCardTextBytes(byte[] idCardTextBytes)
        {
            if (idCardTextBytes.isEmpty())
                throw new ArgumentNullException(nameof(idCardTextBytes));

            if (idCardTextBytes.Length < 256)
                throw new ArgumentException("invalid idcard text bytes", nameof(idCardTextBytes));

            var cardData = new IDCardData();

            cardData.Name = ConvertIDCardBytesToUTF8String(idCardTextBytes, 0, 30).Trim();
            cardData.Sex = TypeHelper.parseInt(ConvertIDCardBytesToUTF8String(idCardTextBytes, 30, 2));
            cardData.Nation = TypeHelper.parseInt(ConvertIDCardBytesToUTF8String(idCardTextBytes, 32, 4));
            cardData.Birthday = ConvertIDCardBytesToUTF8String(idCardTextBytes, 36, 16).Trim();
            cardData.Address = ConvertIDCardBytesToUTF8String(idCardTextBytes, 52, 70).Trim();
            cardData.IDCardNo = ConvertIDCardBytesToUTF8String(idCardTextBytes, 122, 36).Trim();
            cardData.IssuingAuthority = ConvertIDCardBytesToUTF8String(idCardTextBytes, 158, 30).Trim();
            cardData.ValidBeginDate = ConvertIDCardBytesToUTF8String(idCardTextBytes, 188, 16).Trim();
            cardData.ValidEndDate = ConvertIDCardBytesToUTF8String(idCardTextBytes, 204, 16).Trim();

            return cardData;
        }

        /// <summary>
        /// 解析最新地址字节
        /// </summary>
        /// <param name="newAddressBytes"></param>
        /// <returns></returns>
        protected static string ParseNewAddressBytes(byte[] newAddressBytes)
        {
            if (newAddressBytes.isEmpty())
                throw new ArgumentNullException(nameof(newAddressBytes));

            if (newAddressBytes.Length < 70)
                throw new ArgumentException("invalid new address bytes", nameof(newAddressBytes));

            return ConvertIDCardBytesToUTF8String(newAddressBytes, 0, 70).Trim();
        }

        /// <summary>
        /// 将身份证文本字节转换为UTF8字符串
        /// </summary>
        /// <param name="idCardBytes"></param>
        /// <param name="index">起始位置</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        protected static string ConvertIDCardBytesToUTF8String(byte[] idCardBytes, int index, int count)
        {
            if (idCardBytes.isEmpty())
                throw new ArgumentNullException(nameof(idCardBytes));

            byte[] utf8Bytes = Encoding.Convert(Encoding.GetEncoding("UCS-2"), Encoding.UTF8, idCardBytes, index, count);
            return Encoding.UTF8.GetString(utf8Bytes);
        }
        #endregion

        #region 读文字和相片信息
        /// <summary>
        /// 读文字和相片信息
        /// </summary>
        /// <returns></returns>
        public IDCardActionResult ReadBaseTextPhotoInfo()
        {
            return ReadBaseTextPhotoInfo(AppDomainBaseDirectory);
        }

        /// <summary>
        /// 读文字和相片信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        public virtual IDCardActionResult ReadBaseTextPhotoInfo(string fileDirectory)
        {
            if (fileDirectory.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileDirectory));

            CheckDisposed();

            return ReadBaseTextPhotoInfoInternal(fileDirectory);
        }

        /// <summary>
        /// 读文字和相片信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        protected abstract IDCardActionResult ReadBaseTextPhotoInfoInternal(string fileDirectory);
        #endregion

        #region 读最新地址信息
        /// <summary>
        /// 读最新地址信息
        /// </summary>        
        /// <returns></returns>
        public IDCardActionResult ReadNewAddressInfo()
        {
            return ReadNewAddressInfo(AppDomainBaseDirectory);
        }

        /// <summary>
        /// 读最新地址信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        public virtual IDCardActionResult ReadNewAddressInfo(string fileDirectory)
        {
            if (fileDirectory.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileDirectory));

            CheckDisposed();

            return ReadNewAddressInfoInternal(fileDirectory);
        }

        /// <summary>
        /// 读最新地址信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        protected abstract IDCardActionResult ReadNewAddressInfoInternal(string fileDirectory);
        #endregion

        #region 内容解析

        #region 解析文字
        /// <summary>
        /// 解析文字信息
        /// </summary>       
        /// <returns></returns>
        public IDCardInfo ParseTextInfo()
        {
            return ParseTextInfo(AppDomainBaseDirectory);
        }

        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="fileDirectory">文字信息所属目录</param>
        /// <returns></returns>
        public virtual IDCardInfo ParseTextInfo(string fileDirectory)
        {
            if (fileDirectory.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileDirectory));

            CheckDisposed();
            
            return ParseTextInfoInternal(fileDirectory);
        }

        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="fileDirectory">文字信息所属目录</param>
        /// <returns></returns>
        protected abstract IDCardInfo ParseTextInfoInternal(string fileDirectory);

        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="fileDirectory">文字信息所属目录</param>
        /// <param name="textFileName">文字信息 文件名</param>
        /// <returns></returns>
        protected virtual IDCardInfo ParseTextInfoInternal(string fileDirectory, string textFileName)
        {
            var txtFilePath = IOHelper.GetFilePath(fileDirectory, textFileName);
            var fileBytes = IOHelper.ReadFileContent(txtFilePath);

            var cardData = ParseIDCardTextBytes(fileBytes);
            return cardData.FormatCardInfo();
        }
        #endregion

        #region 解析照片
        /// <summary>
        /// 解析照片信息
        /// </summary>        
        /// <returns>BMP照片路径</returns>
        public IDCardActionResult ParsePhotoInfo()
        {
            return ParsePhotoInfo(AppDomainBaseDirectory);
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns>BMP照片路径</returns>
        public virtual IDCardActionResult ParsePhotoInfo(string fileDirectory)
        {
            if (fileDirectory.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileDirectory));

            CheckDisposed();

            return ParsePhotoInfoInternal(fileDirectory);
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns>BMP照片路径</returns>
        protected abstract IDCardActionResult ParsePhotoInfoInternal(string fileDirectory);
        #endregion

        #region 解析最新地址
        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <returns></returns>
        public string ParseNewAddressInfo()
        {
            return ParseNewAddressInfo(AppDomainBaseDirectory);
        }

        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <param name="fileDirectory">最新地址信息所属目录</param>
        /// <returns></returns>
        public virtual string ParseNewAddressInfo(string fileDirectory)
        {
            if (fileDirectory.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileDirectory));

            CheckDisposed();

            return ParseNewAddressInfoInternal(fileDirectory);
        }

        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <param name="fileDirectory">最新地址信息所属目录</param>
        /// <returns></returns>
        protected abstract string ParseNewAddressInfoInternal(string fileDirectory);

        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <param name="fileDirectory">最新地址信息所属目录</param>
        /// <param name="newAddressFileName">最新地址信息 文件名</param>
        /// <returns></returns>
        protected virtual string ParseNewAddressInfoInternal(string fileDirectory, string newAddressFileName)
        {
            var newAddressFilePath = IOHelper.GetFilePath(fileDirectory, newAddressFileName);
            var fileBytes = IOHelper.ReadFileContent(newAddressFilePath);

            return ParseNewAddressBytes(fileBytes);
        }
        #endregion

        #endregion

        #region 获取照片路径
        /// <summary>
        /// 获取Bmp照片路径
        /// </summary>
        /// <returns></returns>
        public string GetBmpPhotoPath()
        {
            return GetBmpPhotoPath(AppDomainBaseDirectory);
        }

        /// <summary>
        /// 获取Bmp照片路径
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns></returns>
        public virtual string GetBmpPhotoPath(string fileDirectory)
        {
            if (fileDirectory.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileDirectory));

            CheckDisposed();

            return GetBmpPhotoPathInternal(fileDirectory);
        }

        /// <summary>
        /// 获取Bmp照片路径
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns></returns>
        protected abstract string GetBmpPhotoPathInternal(string fileDirectory);
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected void CheckDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    DisposeInternal();
                }
                disposedValue = true;
            }
        }

        protected virtual void DisposeInternal()
        {

        }

        ~IDCardReader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
