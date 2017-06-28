namespace IDCard.Reader.Standard
{
    /// <summary>
    /// 身份证交互处理程序（公安部一所）
    /// </summary>
    internal class StandardIDCardInteropHandler : IDCardInteropHandler, IIDCardInteropHandler
    {
        #region 构造函数
        internal StandardIDCardInteropHandler()            
        { }

        internal StandardIDCardInteropHandler(int port)
            : base(port)
        { }

        protected override IDCardActionResult<int> AutoFindReaderPort()
        {
            return StandardIDCardInteropAction.InitCommExt();
        }
        #endregion
    }
}
