namespace IDCard.Reader
{
    public interface IIDCardReader
    {
        #region 读基本信息
        /// <summary>
        /// 读基本信息
        /// </summary>
        /// <returns></returns>
        IDCardActionResult ReadBaseInfo();

        /// <summary>
        /// 读基本信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        IDCardActionResult ReadBaseInfo(string fileDirectory);
        #endregion

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
    }
}
