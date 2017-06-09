using System;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证阅读接口
    /// </summary>
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

        #region 读最新地址信息
        /// <summary>
        /// 读最新地址信息
        /// </summary>        
        /// <returns></returns>
        IDCardActionResult ReadNewAddressInfo();

        /// <summary>
        /// 读最新地址信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        IDCardActionResult ReadNewAddressInfo(string fileDirectory);
        #endregion

        #region 内容解析
        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <returns></returns>
        IDCardInfo ParseTextInfo();

        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="fileDirectory">文字信息所属目录</param>
        /// <returns></returns>
        IDCardInfo ParseTextInfo(string fileDirectory);

        /// <summary>
        /// 解析照片信息
        /// </summary>        
        /// <returns>BMP照片路径</returns>
        IDCardActionResult ParsePhotoInfo();

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns>BMP照片路径</returns>
        IDCardActionResult ParsePhotoInfo(string fileDirectory);

        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <returns></returns>
        string ParseNewAddressInfo();

        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <param name="fileDirectory">最新地址信息所属目录</param>
        /// <returns></returns>
        string ParseNewAddressInfo(string fileDirectory);
        #endregion
    }
}
