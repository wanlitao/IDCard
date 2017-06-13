using System;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证交互处理程序（新中新）
    /// </summary>
    internal class SynjonesIDCardInteropHandler : IIDCardInteropHandler
    {
        private int _port = 0;

        #region 构造函数
        internal SynjonesIDCardInteropHandler()
        {
            var portResult = SynjonesIDCardInteropAction.FindReader();
            if (!portResult.flag)
                throw new IDCardReadException(portResult.msg, portResult.code);

            _port = portResult.data;
        }

        internal SynjonesIDCardInteropHandler(int port)            
        {
            if (port < 1 || port > 9999)
                throw new ArgumentOutOfRangeException(nameof(port), "port must between 1 and 9999");

            _port = port;
        }
        #endregion

        protected int Port { get { return _port; } }

        /// <summary>
        /// 执行身份证交互操作
        /// </summary>
        /// <param name="interopAction"></param>
        /// <returns></returns>
        public IDCardActionResult ExecIDCardInteropAction(Func<int, IDCardActionResult> interopAction)
        {
            if (interopAction == null)
                throw new ArgumentNullException(nameof(interopAction));

            return interopAction(Port);
        }
    }
}
