using System;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证阅读（新中新）
    /// </summary>
    public class SynjonesIDCardReader : IDCardReader
    {
        private SynjonesIDCardReaderOptions _options;

        #region 构造函数
        public SynjonesIDCardReader(SynjonesIDCardReaderOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _options = options;
        }
        #endregion

        #region Helper Functions
        /// <summary>
        /// 是否调用成功
        /// </summary>
        /// <param name="retCode"></param>
        /// <returns></returns>
        private static bool IsRetSuccess(int retCode)
        {
            return retCode == 0;
        }

        /// <summary>
        /// 获取ifOpen参数代码值
        /// </summary>
        /// <param name="ifOpen"></param>
        /// <returns></returns>
        private static int GetIfOpenCode(bool ifOpen)
        {
            return ifOpen ? 1 : 0;
        }

        /// <summary>
        /// 获取通讯端口
        /// </summary>
        /// <returns></returns>
        private IDCardActionResult<int> FindCommunicatePort()
        {
            if (_options.Port.HasValue)
                return IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult<int>, int>(0, _options.Port.Value);

            return FindReader();
        }
        #endregion

        #region Interop Action
        /// <summary>
        /// 自动寻找读卡器
        /// </summary>
        /// <returns></returns>
        private static IDCardActionResult<int> FindReader()
        {
            var retPort = SynjonesIDCardInterop.FindReader();
            var retCode = retPort > 0 ? 0 : -1;

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult<int>, int>(retCode, retPort)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult<int>>(retCode, "connect reader fail");
        }

        /// <summary>
        /// 执行Interop操作
        /// </summary>
        /// <param name="interopFunc"></param>
        /// <returns></returns>
        private static IDCardActionResult ExecInteropAction(Func<int> interopFunc)
        {
            var retCode = interopFunc();

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult>(retCode)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult>(retCode, SynjonesIDCardRetCode.GetCodeMsg(retCode));
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static IDCardActionResult OpenPort(int port)
        {
            return ExecInteropAction(() => SynjonesIDCardInterop.OpenPort(port));
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static IDCardActionResult ClosePort(int port)
        {
            return ExecInteropAction(() => SynjonesIDCardInterop.ClosePort(port));            
        }

        /// <summary>
        /// 开始找卡
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        private static IDCardActionResult StartFindIDCard(int port, bool ifOpen)
        {
            return ExecInteropAction(() =>
            {
                var pucIIN = new byte[4];
                return SynjonesIDCardInterop.StartFindIDCard(port, pucIIN, GetIfOpenCode(ifOpen));
            });
        }

        /// <summary>
        /// 选卡
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        private static IDCardActionResult SelectIDCard(int port, bool ifOpen)
        {
            return ExecInteropAction(() =>
            {
                var pucSN = new byte[8];
                return SynjonesIDCardInterop.SelectIDCard(port, pucSN, GetIfOpenCode(ifOpen));
            });
        }

        /// <summary>
        /// 读取基本区域信息到文件
        /// </summary>
        /// <param name="port"></param>
        /// <param name="txtFileName">文字信息写入文件名</param>
        /// <param name="photoFileName">照片信息写入文件名</param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        private IDCardActionResult ReadBaseMsgToFile(int port, string txtFileName, string photoFileName, bool ifOpen)
        {
            return ExecInteropAction(() =>
            {
                uint txtFileLen = 0;
                uint photoFileLen = 0;
                return SynjonesIDCardInterop.ReadBaseMsgToFile(port,
                    txtFileName, ref txtFileLen, photoFileName, ref photoFileLen, GetIfOpenCode(ifOpen));
            });
        }
        #endregion

        #region 读文字和相片信息
        /// <summary>
        /// 读文字和相片信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>        
        protected override IDCardActionResult ReadBaseTextPhotoInfoInternal(string fileDirectory)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 内容解析
        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="textFileBytes">文字信息文件字节数组</param>
        /// <returns></returns>        
        protected override IDCardInfo ParseTextInfoInternal(byte[] textFileBytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="wltFilePath">相片文件路径</param>
        /// <returns>BMP照片路径</returns>
        protected override IDCardActionResult<string> ParsePhotoInfoInternal(string wltFilePath)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 读最新地址信息
        /// <summary>
        /// 读最新地址信息
        /// </summary>
        /// <returns></returns>
        protected override IDCardActionResult<string> ReadNewAddressInfoInternal()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}