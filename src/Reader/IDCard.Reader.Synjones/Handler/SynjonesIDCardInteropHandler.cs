namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证交互处理程序（新中新）
    /// </summary>
    internal class SynjonesIDCardInteropHandler : IDCardInteropHandler, IIDCardInteropHandler
    {
        #region 构造函数
        internal SynjonesIDCardInteropHandler()            
        { }

        internal SynjonesIDCardInteropHandler(int port)
            : base(port)
        { }

        protected override IDCardActionResult<int> AutoFindReaderPort()
        {
            return SynjonesIDCardInteropAction.FindReader();
        }
        #endregion
    }
}
