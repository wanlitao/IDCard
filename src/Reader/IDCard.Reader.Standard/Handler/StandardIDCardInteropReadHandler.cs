namespace IDCard.Reader.Standard
{
    /// <summary>
    /// 身份证阅读交互处理程序（公安部一所）
    /// </summary>
    internal class StandardIDCardInteropReadHandler : IDCardInteropReadHandler, IIDCardInteropReadHandler
    {
        #region 构造函数
        internal StandardIDCardInteropReadHandler()
        { }

        internal StandardIDCardInteropReadHandler(int port)
            : base(port)
        { }

        protected override IDCardActionResult<int> AutoFindReaderPort()
        {
            return StandardIDCardInteropAction.InitCommExt();
        }

        protected override IDCardActionResult OpenPort(int port)
        {
            return StandardIDCardInteropAction.InitComm(port);
        }
        #endregion

        /// <summary>
        /// 执行身份证阅读前置交互操作
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        protected override IDCardActionResult ExecIDCardInteropReadPreposeAction(int port)
        {
            return StandardIDCardInteropAction.Authenticate();
        }

        #region IDisposable Support
        protected override void DisposeInternal()
        {
            StandardIDCardInteropAction.CloseComm();
        }
        #endregion
    }
}
