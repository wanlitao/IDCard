using System;

namespace IDCard.Reader
{
    public interface IIDCardReader : IDisposable
    {
        #region 读文字和相片信息
        /// <summary>
        /// 读文字和相片信息
        /// </summary>
        /// <returns></returns>
        IDCardActionResult ReadBaseTextPhotoInfo();

        /// <summary>
        /// 读文字和相片信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        IDCardActionResult ReadBaseTextPhotoInfo(string fileDirectory);
        #endregion

        #region 内容解析
        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="txtFilePath">文字信息文件路径</param>
        /// <returns></returns>
        IDCardInfo ParseTextInfo(string txtFilePath);

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="wltFilePath">相片文件路径</param>
        /// <returns>BMP照片路径</returns>
        IDCardActionResult<string> ParsePhotoInfo(string wltFilePath);
        #endregion

        #region 读最新地址信息
        /// <summary>
        /// 读最新地址信息
        /// </summary>        
        /// <returns></returns>
        IDCardActionResult<string> ReadNewAddressInfo();
        #endregion
    }
}
