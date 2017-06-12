using FCP.Util;
using System;
using System.IO;

namespace IDCard.Reader
{
    public static class IOHelper
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static byte[] ReadFileContent(string filePath)
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
        public static void WriteToFile(string filePath, byte[] fileBytes, int byteLength)
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
        /// json序列化写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileData">文件数据</param>
        public static void WriteToFileSerializeByJson<TData>(string filePath, TData fileData)
        {
            if (filePath.isNullOrEmpty())
                throw new ArgumentNullException(nameof(filePath));

            var fileBytes = SerializerFactory.JsonSerializer.Serialize(fileData);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fileStream.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetFilePath(string directoryPath, string fileName)
        {
            if (directoryPath.isNullOrEmpty())
                throw new ArgumentNullException(nameof(directoryPath));

            if (fileName.isNullOrEmpty())
                throw new ArgumentNullException(nameof(fileName));

            return Path.Combine(directoryPath, fileName);
        }
    }
}
