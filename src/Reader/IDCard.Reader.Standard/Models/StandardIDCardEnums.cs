namespace IDCard.Reader.Standard
{
    /// <summary>
    /// 身份证读取信息类型（公安部一所）
    /// </summary>
    public enum StandardIDCardReadActiveType
    {
        /// <summary>
        /// 文字和相片（包括Bmp）
        /// </summary>
        TextAndWltWithBmp = 1,
        /// <summary>
        /// 文字和相片
        /// </summary>
        TextAndWlt = 2,
        /// <summary>
        /// 最新住址
        /// </summary>
        NewAddress = 3
    }
}
