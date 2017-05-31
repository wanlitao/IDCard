using System;

namespace IDCard.Reader.Synjones
{
    public class SynjonesIDCardReader : IDCardReader
    {
        #region 读文字和相片信息
        /// <summary>
        /// 读文字和相片信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>        
        protected override IDCardActionResult ReadBaseTextPhotoInfoInternal(string fileDirectory)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 内容解析
        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="textFileBytes">文字信息文件字节数组</param>
        /// <returns></returns>        
        protected override IDCardInfo ParseTextInfoInternal(byte[] textFileBytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="wltFilePath">相片文件路径</param>
        /// <returns>BMP照片路径</returns>
        protected override IDCardActionResult<string> ParsePhotoInfoInternal(string wltFilePath)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 读最新地址信息
        /// <summary>
        /// 读最新地址信息
        /// </summary>
        /// <returns></returns>
        protected override IDCardActionResult<string> ReadNewAddressInfoInternal()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}