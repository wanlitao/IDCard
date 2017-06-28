using System;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证阅读交互处理程序
    /// </summary>
    public abstract class IDCardInteropReadHandler : IDCardInteropHandler, IIDCardInteropReadHandler
    {
        #region 构造函数
        public IDCardInteropReadHandler()
        {
            OpenCommunicatePort();
        }

        public IDCardInteropReadHandler(int port)
            : base(port)
        {
            OpenCommunicatePort();
        }

        private void OpenCommunicatePort()
        {
            var result = OpenPort(Port);
            if (!result.flag)
            {
                throw new IDCardReadException(result.msg, result.code);
            }
        }

        protected abstract IDCardActionResult OpenPort(int port);
        #endregion

        /// <summary>
        /// 执行身份证阅读交互操作
        /// </summary>
        /// <param name="interopReadAction"></param>
        /// <returns></returns>
        public virtual IDCardActionResult ExecIDCardInteropReadAction(Func<int, IDCardActionResult> interopReadAction)
        {
            if (interopReadAction == null)
                throw new ArgumentNullException(nameof(interopReadAction));

            var result = ExecIDCardInteropReadPreposeAction(Port);
            if (!result.flag)
                return result;

            return interopReadAction(Port);
        }

        /// <summary>
        /// 执行身份证阅读前置交互操作
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        protected abstract IDCardActionResult ExecIDCardInteropReadPreposeAction(int port);

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                DisposeInternal();
                disposedValue = true;
            }
        }

        protected abstract void DisposeInternal();

        ~IDCardInteropReadHandler()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
