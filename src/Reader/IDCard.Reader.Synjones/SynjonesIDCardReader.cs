using FCP.Util;
using System;
using System.IO;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证阅读（新中新）
    /// </summary>
    public class SynjonesIDCardReader : IDCardReader
    {
        protected const string DefaultCardDataFileName = "idcard.txt";

        private SynjonesIDCardReaderOptions _options;        

        #region 构造函数
        public SynjonesIDCardReader()
            : this(new SynjonesIDCardReaderOptions())
        { }

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

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileData">文件数据</param>
        private static void WriteToFile<TData>(string filePath, TData fileData)
        {
            if (filePath.isNullOrEmpty())
                throw new ArgumentNullException(nameof(filePath));

            var fileBytes = SerializerFactory.JsonSerializer.Serialize(fileData);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fileStream.Write(fileBytes, 0, fileBytes.Length);
            }
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
        /// 读取基本区域信息
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ifOpen">是否在函数内部打开和关闭端口</param>
        /// <param name="txtFilePath">文字信息写入文件路径</param>
        /// <param name="photoFilePath">照片信息写入文件路径</param>
        /// <returns></returns>
        private static IDCardActionResult ReadBaseMsg(int port, bool ifOpen, string txtFilePath, string photoFilePath)
        {
            return ExecInteropAction(() =>
            {
                var txtBytes = new byte[512];
                uint txtByteLen = 0;
                var photoBytes = new byte[4096];
                uint photoByteLen = 0;
                var retCode = SynjonesIDCardInterop.ReadBaseMsg(port,
                    txtBytes, ref txtByteLen, photoBytes, ref photoByteLen, GetIfOpenCode(ifOpen));

                WriteToFile(txtFilePath, txtBytes, (int)txtByteLen);
                WriteToFile(photoFilePath, photoBytes, (int)photoByteLen);

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
        private static IDCardActionResult ReadBaseMsgToFile(int port, string txtFilePath, string photoFilePath, bool ifOpen)
        {
            return ExecInteropAction(() =>
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
        private static IDCardActionResult ReadMsg(int port, bool ifOpen, string cardDataFilePath)
        {
            return ExecInteropAction(() =>
            {
                SynjonesIDCardData idCardData = new SynjonesIDCardData();
                var retCode = SynjonesIDCardInterop.ReadMsg(port, GetIfOpenCode(ifOpen), ref idCardData);

                WriteToFile(cardDataFilePath, idCardData);

                return retCode;
            });
        }
        #endregion

        #region Interop Read Action
        /// <summary>
        /// 执行Interop读操作
        /// </summary>
        /// <param name="interopReadFunc"></param>
        /// <returns></returns>
        private IDCardActionResult ExecInteropReadAction(Func<int, IDCardActionResult> interopReadFunc)
        {
            var portResult = FindCommunicatePort();
            if (!portResult.flag)
                return portResult;

            var port = portResult.data;

            var result = OpenPort(port);
            if (!result.flag)
                return result;

            result = StartFindIDCard(port, false);
            if (result.flag)
            {
                result = SelectIDCard(port, false);
                if (result.flag)
                {
                    result = interopReadFunc(port);
                }
            }

            ClosePort(port);
            return result;
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
            var txtFilePath = GetFilePath(fileDirectory, DefaultTextFileName);
            var photoFilePath = GetFilePath(fileDirectory, DefaultPhotoFileName);

            return ExecInteropReadAction((port) => ReadBaseMsg(port, false, txtFilePath, photoFilePath));
        }
        #endregion

        #region 内容解析
        /// <summary>
        /// 解析文字信息
        /// </summary>
        /// <param name="fileDirectory">文字信息所属目录</param>
        /// <returns></returns>
        protected override IDCardInfo ParseTextInfoInternal(string fileDirectory)
        {
            var txtFilePath = GetFilePath(fileDirectory, DefaultTextFileName);
            var fileBytes = ReadFileContent(txtFilePath);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns>BMP照片路径</returns>
        protected override IDCardActionResult<string> ParsePhotoInfoInternal(string fileDirectory)
        {
            var photoFilePath = GetFilePath(fileDirectory, DefaultPhotoFileName);

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