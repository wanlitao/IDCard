using System;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证阅读交互处理程序 接口
    /// </summary>
    public interface IIDCardInteropReadHandler : IIDCardInteropHandler, IDisposable
    {
        /// <summary>
        /// 执行身份证阅读交互操作
        /// </summary>
        /// <param name="interopReadAction"></param>
        /// <returns></returns>
        IDCardActionResult ExecIDCardInteropReadAction(Func<int, IDCardActionResult> interopReadAction);
    }
}
