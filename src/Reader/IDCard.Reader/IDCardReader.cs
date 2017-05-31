using System;
using System.IO;
using FCP.Util;

namespace IDCard.Reader
{
    public abstract class IDCardReader : IIDCardReader
    {
        protected const string TextFileName = "wz.txt";
        protected const string PhotoFileName = "xp.wlt";

        protected static string AppDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        #region Helper Functions
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        protected static byte[] ReadFileContent(string filePath)
        {
            if (filePath.isNullOrEmpty())
                throw new ArgumentNullException(nameof(filePath));

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, (int)fileStream.Length);

                return fileBytes;
            }
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

        #region 内容解析
        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="txtFilePath">文字信息文件路径</param>
        /// <returns></returns>
        public virtual IDCardInfo ParseTextInfo(string txtFilePath)
        {
            if (txtFilePath.isNullOrEmpty())
                throw new ArgumentNullException(txtFilePath);

            CheckDisposed();

            var txtFileBytes = ReadFileContent(txtFilePath);
            return ParseTextInfoInternal(txtFileBytes);
        }

        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="textFileBytes">文字信息文件字节数组</param>
        /// <returns></returns>
        protected abstract IDCardInfo ParseTextInfoInternal(byte[] textFileBytes);

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="wltFilePath">相片文件路径</param>
        /// <returns>BMP照片路径</returns>
        public virtual IDCardActionResult<string> ParsePhotoInfo(string wltFilePath)
        {
            if (wltFilePath.isNullOrEmpty())
                throw new ArgumentNullException(wltFilePath);

            CheckDisposed();

            return ParsePhotoInfoInternal(wltFilePath);
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="wltFilePath">相片文件路径</param>
        /// <returns>BMP照片路径</returns>
        protected abstract IDCardActionResult<string> ParsePhotoInfoInternal(string wltFilePath);
        #endregion

        #region 读最新地址信息
        /// <summary>
        /// 读最新地址信息
        /// </summary>        
        /// <returns></returns>
        public virtual IDCardActionResult<string> ReadNewAddressInfo()
        {
            CheckDisposed();

            return ReadNewAddressInfoInternal();
        }

        /// <summary>
        /// 读最新地址信息
        /// </summary>
        /// <returns></returns>
        protected abstract IDCardActionResult<string> ReadNewAddressInfoInternal();
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
