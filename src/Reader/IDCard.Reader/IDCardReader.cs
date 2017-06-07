using System;
using System.IO;
using FCP.Util;

namespace IDCard.Reader
{
    public abstract class IDCardReader : IIDCardReader
    {
        protected const string DefaultTextFileName = "wz.txt";
        protected const string DefaultPhotoFileName = "xp.wlt";

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

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileBytes">写入字节</param>
        /// <param name="byteLength">字节数</param>
        protected static void WriteToFile(string filePath, byte[] fileBytes, int byteLength)
        {
            if (filePath.isNullOrEmpty())
                throw new ArgumentNullException(nameof(filePath));

            if (fileBytes.isEmpty())
                throw new ArgumentNullException(nameof(fileBytes));

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fileStream.Write(fileBytes, 0, byteLength);
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        protected static string GetFilePath(string directoryPath, string fileName)
        {
            if (directoryPath.isNullOrEmpty())
                throw new ArgumentNullException(nameof(directoryPath));

            if (fileName.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileName));

            return Path.Combine(directoryPath, fileName);
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
        /// 解析照片信息
        /// </summary>        
        /// <returns>BMP照片路径</returns>
        public IDCardActionResult<string> ParsePhotoInfo()
        {
            return ParsePhotoInfo(AppDomainBaseDirectory);
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns>BMP照片路径</returns>
        public virtual IDCardActionResult<string> ParsePhotoInfo(string fileDirectory)
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
        protected abstract IDCardActionResult<string> ParsePhotoInfoInternal(string fileDirectory);
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
