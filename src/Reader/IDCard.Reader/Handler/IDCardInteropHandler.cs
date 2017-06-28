using System;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证交互处理程序
    /// </summary>
    public abstract class IDCardInteropHandler : IIDCardInteropHandler
    {
        private int _port = 0;

        #region 构造函数
        public IDCardInteropHandler()
        {
            var portResult = AutoFindReaderPort();
            if (!portResult.flag)
                throw new IDCardReadException(portResult.msg, portResult.code);

            _port = portResult.data;
        }

        public IDCardInteropHandler(int port)
        {
            if (port < 1 || port > 9999)
                throw new ArgumentOutOfRangeException(nameof(port), "port must between 1 and 9999");

            _port = port;
        }

        protected abstract IDCardActionResult<int> AutoFindReaderPort();
        #endregion

        protected int Port { get { return _port; } }

        /// <summary>
        /// 执行身份证交互操作
        /// </summary>
        /// <param name="interopAction"></param>
        /// <returns></returns>
        public virtual IDCardActionResult ExecIDCardInteropAction(Func<int, IDCardActionResult> interopAction)
        {
            if (interopAction == null)
                throw new ArgumentNullException(nameof(interopAction));

            return interopAction(Port);
        }
    }
}
