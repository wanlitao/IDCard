using System;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证交互操作（新中新）
    /// </summary>
    internal static class SynjonesIDCardInteropAction
    {
        internal const int SuccessRetCode = 0;

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
        /// 获取ifOpen参数代码值
        /// </summary>
        /// <param name="ifOpen"></param>
        /// <returns></returns>
        private static int GetIfOpenCode(bool ifOpen)
        {
            return ifOpen ? 1 : 0;
        }

        /// <summary>
        /// 获取通讯类型
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static int GetCommunicateType(int port)
        {
            if (port < 1)
                throw new ArgumentException("invalid port", nameof(port));

            var communicateType = port > 1000 ? SynjonesIDCardCommunicateType.USB : SynjonesIDCardCommunicateType.RS232C;

            return (int)communicateType;
        }

        /// <summary>
        /// 执行Interop函数
        /// </summary>
        /// <param name="interopFunc"></param>
        /// <returns></returns>
        private static IDCardActionResult ExecInteropFunction(Func<int> interopFunc)
        {
            var retCode = interopFunc();

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult>(retCode)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult>(retCode, SynjonesIDCardRetCode.GetCodeMsg(retCode));
        }
        #endregion

        #region Interop Action
        /// <summary>
        /// 自动寻找读卡器
        /// </summary>
        /// <returns></returns>
        internal static IDCardActionResult<int> FindReader()
        {
            var retPort = SynjonesIDCardInterop.FindReader();
            var retCode = retPort > 0 ? SuccessRetCode : -99;

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult<int>, int>(retCode, retPort)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult<int>>(retCode, "自动寻找阅读器失败");
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        internal static IDCardActionResult OpenPort(int port)
        {
            return ExecInteropFunction(() => SynjonesIDCardInterop.OpenPort(port));
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        internal static IDCardActionResult ClosePort(int port)
        {
            return ExecInteropFunction(() => SynjonesIDCardInterop.ClosePort(port));
        }

        /// <summary>
        /// 开始找卡
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        internal static IDCardActionResult StartFindIDCard(int port, bool ifOpen)
        {
            return ExecInteropFunction(() =>
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
        internal static IDCardActionResult SelectIDCard(int port, bool ifOpen)
        {
            return ExecInteropFunction(() =>
            {
                var pucSN = new byte[8];
                return SynjonesIDCardInterop.SelectIDCard(port, pucSN, GetIfOpenCode(ifOpen));
            });
        }

        /// <summary>
        /// 读取基本区域信息
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <param name="txtFilePath">文字信息写入文件路径</param>
        /// <param name="photoFilePath">照片信息写入文件路径</param>
        /// <returns></returns>
        internal static IDCardActionResult ReadBaseMsg(int port, bool ifOpen, string txtFilePath, string photoFilePath)
        {
            return ExecInteropFunction(() =>
            {
                var txtBytes = new byte[512];
                uint txtByteLen = 0;
                var photoBytes = new byte[4096];
                uint photoByteLen = 0;
                var retCode = SynjonesIDCardInterop.ReadBaseMsg(port,
                    txtBytes, ref txtByteLen, photoBytes, ref photoByteLen, GetIfOpenCode(ifOpen));

                if (IsRetSuccess(retCode))
                {
                    IOHelper.WriteToFile(txtFilePath, txtBytes, (int)txtByteLen);
                    IOHelper.WriteToFile(photoFilePath, photoBytes, (int)photoByteLen);
                }

                return retCode;
            });
        }

        /// <summary>
        /// 读取基本区域信息到文件
        /// </summary>
        /// <param name="port"></param>
        /// <param name="txtFilePath">文字信息写入文件路径</param>
        /// <param name="photoFilePath">照片信息写入文件路径</param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        internal static IDCardActionResult ReadBaseMsgToFile(int port, string txtFilePath, string photoFilePath, bool ifOpen)
        {
            return ExecInteropFunction(() =>
            {
                uint txtFileLen = 0;
                uint photoFileLen = 0;
                return SynjonesIDCardInterop.ReadBaseMsgToFile(port,
                    txtFilePath, ref txtFileLen, photoFilePath, ref photoFileLen, GetIfOpenCode(ifOpen));
            });
        }

        /// <summary>
        /// 读取基本区域信息，并按设置转化文本和照片
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <param name="cardDataFilePath">身份证数据写入文件路径</param>
        /// <returns></returns>
        [Obsolete("as this method often lead to crash, please use ReadBaseMsg instead.", true)]
        internal static IDCardActionResult ReadMsg(int port, bool ifOpen, string cardDataFilePath)
        {
            return ExecInteropFunction(() =>
            {
                SynjonesIDCardData idCardData = new SynjonesIDCardData();
                var retCode = SynjonesIDCardInterop.ReadMsg(port, GetIfOpenCode(ifOpen), ref idCardData);

                if (IsRetSuccess(retCode))
                {
                    IOHelper.WriteToFileSerializeByJson(cardDataFilePath, idCardData);
                }

                return retCode;
            });
        }

        /// <summary>
        /// 将wlt文件解码成bmp文件
        /// </summary>
        /// <param name="port"></param>
        /// <param name="photoFilePath">wlt照片文件路径</param>
        /// <returns></returns>
        internal static IDCardActionResult GetBmp(int port, string photoFilePath)
        {
            return ExecInteropFunction(() => SynjonesIDCardInterop.GetBmp(port, photoFilePath));
        }

        /// <summary>
        /// 读取追加地址信息
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <param name="newAddressFilePath">追加地址信息写入文件路径</param>
        /// <returns></returns>
        internal static IDCardActionResult ReadNewAppMsg(int port, bool ifOpen, string newAddressFilePath)
        {
            return ExecInteropFunction(() =>
            {
                var addressBytes = new byte[70];
                uint addressByteLen = 0;
                var retCode = SynjonesIDCardInterop.ReadNewAppMsg(port,
                    addressBytes, ref addressByteLen, GetIfOpenCode(ifOpen));

                if (IsRetSuccess(retCode))
                {
                    IOHelper.WriteToFile(newAddressFilePath, addressBytes, (int)addressByteLen);
                }

                return retCode;
            });
        }
        #endregion
    }
}
