using System;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证阅读交互处理程序（新中新）
    /// </summary>
    internal class SynjonesIDCardInteropReadHandler : SynjonesIDCardInteropHandler, IDisposable
    {
        #region 构造函数
        internal SynjonesIDCardInteropReadHandler()
            : base()
        {
            OpenCommunicatePort();
        }

        internal SynjonesIDCardInteropReadHandler(int port)
            : base(port)
        {
            OpenCommunicatePort();
        }

        private void OpenCommunicatePort()
        {
            var result = SynjonesIDCardInteropAction.OpenPort(Port);
            if (!result.flag)
            {
                throw new IDCardReadException(result.msg, result.code);
            }
        }
        #endregion

        /// <summary>
        /// 执行身份证阅读交互操作
        /// </summary>
        /// <param name="interopReadAction"></param>
        /// <returns></returns>
        internal IDCardActionResult ExecIDCardInteropReadAction(Func<int, IDCardActionResult> interopReadAction)
        {
            if (interopReadAction == null)
                throw new ArgumentNullException(nameof(interopReadAction));

            var result = SynjonesIDCardInteropAction.StartFindIDCard(Port, false);
            if (!result.flag)
                return result;

            result = SynjonesIDCardInteropAction.SelectIDCard(Port, false);
            if (!result.flag)
                return result;

            return interopReadAction(Port);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
                SynjonesIDCardInteropAction.ClosePort(Port);
                disposedValue = true;
            }
        }
        
        ~SynjonesIDCardInteropReadHandler()
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
