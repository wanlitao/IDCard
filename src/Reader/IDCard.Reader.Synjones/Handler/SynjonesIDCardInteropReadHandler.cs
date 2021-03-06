﻿namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证阅读交互处理程序（新中新）
    /// </summary>
    internal class SynjonesIDCardInteropReadHandler : IDCardInteropReadHandler, IIDCardInteropReadHandler
    {
        #region 构造函数
        internal SynjonesIDCardInteropReadHandler()            
        { }

        internal SynjonesIDCardInteropReadHandler(int port)
            : base(port)
        { }

        protected override IDCardActionResult<int> AutoFindReaderPort()
        {
            return SynjonesIDCardInteropAction.FindReader();
        }

        protected override IDCardActionResult OpenPort(int port)
        {
            return SynjonesIDCardInteropAction.OpenPort(port);
        }
        #endregion

        /// <summary>
        /// 执行身份证阅读前置交互操作
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        protected override IDCardActionResult ExecIDCardInteropReadPreposeAction(int port)
        {
            var result = SynjonesIDCardInteropAction.StartFindIDCard(port, false);
            if (!result.flag)
                return result;

            return SynjonesIDCardInteropAction.SelectIDCard(port, false);
        }

        #region IDisposable Support
        protected override void DisposeInternal()
        {
            SynjonesIDCardInteropAction.ClosePort(Port);
        }
        #endregion
    }
}
