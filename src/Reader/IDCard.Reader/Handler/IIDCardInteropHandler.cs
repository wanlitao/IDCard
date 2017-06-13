using System;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证交互处理程序 接口
    /// </summary>
    public interface IIDCardInteropHandler
    {
        /// <summary>
        /// 执行身份证交互操作
        /// </summary>
        /// <param name="interopAction"></param>
        /// <returns></returns>
        IDCardActionResult ExecIDCardInteropAction(Func<int, IDCardActionResult> interopAction);
    }
}
