using System;

namespace IDCard.Reader.Standard
{
    /// <summary>
    /// 身份证交互操作（公安部一所）
    /// </summary>
    internal static class StandardIDCardInteropAction
    {
        internal const int SuccessRetCode = 1;

        #region Helper Functions
        /// <summary>
        /// 是否调用成功
        /// </summary>
        /// <param name="retCode"></param>
        /// <returns></returns>
        private static bool IsRetSuccess(int retCode)
        {
            return retCode == SuccessRetCode;
        }

        /// <summary>
        /// 获取 操作错误信息格式化函数
        /// </summary>
        /// <param name="interopActionName"></param>
        /// <returns></returns>
        private static Func<int, string> GetRetCodeMsgFunction(string interopActionName)
        {
            return (code) =>
            {
                if (IsRetSuccess(code))
                    return string.Empty;

                return $"{interopActionName ?? "操作"}失败";
            };
        }

        /// <summary>
        /// 执行Interop函数
        /// </summary>
        /// <param name="interopFunc"></param>
        /// <returns></returns>
        private static IDCardActionResult ExecInteropFunction(Func<int> interopFunc, Func<int, string> codeMsgFunc)
        {
            var retCode = interopFunc();

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<StandardIDCardActionResult>(retCode)
                : IDCardActionResultHelper.FormatFail<StandardIDCardActionResult>(retCode, codeMsgFunc(retCode));
        }
        #endregion

        #region Interop Action
        /// <summary>
        /// 初始化端口
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns></returns>
        internal static IDCardActionResult InitComm(int port)
        {
            return ExecInteropFunction(() => StandardIDCardInterop.InitComm(port),
                GetRetCodeMsgFunction("初始化端口"));
        }

        /// <summary>
        /// 初始化端口 自动查找
        /// </summary>
        /// <returns></returns>
        internal static IDCardActionResult<int> InitCommExt()
        {
            var retPort = StandardIDCardInterop.InitCommExt();
            var retCode = retPort > 0 ? SuccessRetCode : -99;

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<StandardIDCardActionResult<int>, int>(retCode, retPort)
                : IDCardActionResultHelper.FormatFail<StandardIDCardActionResult<int>>(retCode, "自动寻找阅读器失败");
        }

        /// <summary>
        /// 关闭已打开端口
        /// </summary>
        /// <returns></returns>
        internal static IDCardActionResult CloseComm()
        {
            return ExecInteropFunction(StandardIDCardInterop.CloseComm,
                GetRetCodeMsgFunction("关闭已打开端口"));
        }

        /// <summary>
        /// 卡认证
        /// </summary>
        /// <returns></returns>
        internal static IDCardActionResult Authenticate()
        {
            return ExecInteropFunction(StandardIDCardInterop.Authenticate,
                GetRetCodeMsgFunction("卡认证"));
        }

        /// <summary>
        /// 读卡操作
        /// </summary>
        /// <param name="active">读取信息类型</param>
        /// <returns></returns>
        internal static IDCardActionResult ReadContent(int active)
        {
            return ExecInteropFunction(() => StandardIDCardInterop.ReadContent(active),
                StandardIDCardReadRetCode.GetCodeMsg);
        }

        /// <summary>
        /// 读卡操作 指定目录
        /// </summary>
        /// <param name="cPath">目录</param>
        /// <param name="active">读取信息类型</param>
        /// <returns></returns>
        internal static IDCardActionResult ReadContentPath(string cPath, int active)
        {
            return ExecInteropFunction(() => StandardIDCardInterop.ReadContentPath(cPath, active),
                StandardIDCardReadRetCode.GetCodeMsg);
        }

        /// <summary>
        /// 获取BMP照片
        /// </summary>
        /// <param name="wlt_File">wlt文件名（含路径）</param>
        /// <returns></returns>
        internal static IDCardActionResult GetPhoto(string wlt_File)
        {
            return ExecInteropFunction(() => StandardIDCardInterop.GetPhoto(wlt_File),
                GetRetCodeMsgFunction("获取BMP照片"));
        }
        #endregion
    }
}
