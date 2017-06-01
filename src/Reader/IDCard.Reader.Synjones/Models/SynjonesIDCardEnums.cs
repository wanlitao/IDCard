namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证照片路径类型（新中新）
    /// </summary>
    public enum SynjonesIDCardPhotoPathType
    {
        /// <summary>
        /// C盘根目录
        /// </summary>
        SystemDiskRoot = 0,
        /// <summary>
        /// 当前运行路径
        /// </summary>
        CurrentBaseDirectory = 1,
        /// <summary>
        /// 指定路径
        /// </summary>
        SpecialDirectory = 2
    }

    /// <summary>
    /// 身份证照片内容格式（新中新）
    /// </summary>
    public enum SynjonesIDCardPhotoContentFormat
    {
        /// <summary>
        /// bmp图片
        /// </summary>
        Bmp = 0,
        /// <summary>
        /// jpeg图片
        /// </summary>
        Jpeg = 1,
        /// <summary>
        /// base64字符串
        /// </summary>
        Base64 = 2,
        /// <summary>
        /// wlt格式
        /// </summary>
        Wlt = 3,
        /// <summary>
        /// 不生成图片
        /// </summary>
        None = 4
    }

    /// <summary>
    /// 身份证照片文件名类型（新中新）
    /// </summary>
    public enum SynjonesIDCardPhotoNameType
    {
        /// <summary>
        /// temp
        /// </summary>
        Tmp = 0,
        /// <summary>
        /// 姓名
        /// </summary>
        IDCardName = 1,
        /// <summary>
        /// 身份证号
        /// </summary>
        IDCardNo = 2,
        /// <summary>
        /// 姓名_身份证号
        /// </summary>
        IDCardNameWithNo = 3,
        /// <summary>
        /// 指定文件名
        /// </summary>
        SpecialName = 4
    }
}
