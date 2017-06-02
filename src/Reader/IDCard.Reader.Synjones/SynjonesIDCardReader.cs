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
        private bool IsRetSuccess(int retCode)
        {
            return retCode == 0;
        }

        /// <summary>
        /// 获取通讯端口
        /// </summary>
        /// <returns></returns>
        private IDCardActionResult<int> FindCommunicatePort()
        {
            if (_options.Port.HasValue)
                return IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult<int>, int>(0, _options.Port.Value);

            var retPort = SynjonesIDCardInterop.FindReader();
            var retCode = retPort > 0 ? 0 : -1;

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult<int>, int>(retCode, retPort)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult<int>>(retCode, "connect reader fail");
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private IDCardActionResult OpenPort(int port)
        {
            var retCode = SynjonesIDCardInterop.OpenPort(port);

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult>(retCode)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult>(retCode, "open port fail");
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private IDCardActionResult ClosePort(int port)
        {
            var retCode = SynjonesIDCardInterop.ClosePort(port);

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult>(retCode)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult>(retCode, "invalid port");
        }

        /// <summary>
        /// 开始找卡
        /// </summary>
        /// <param name="port"></param>        
        /// <returns></returns>
        private IDCardActionResult StartFindIDCard(int port)
        {
            var pucIIN = new byte[4];
            var retCode = SynjonesIDCardInterop.StartFindIDCard(port, pucIIN, 0);

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult>(retCode)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult>(retCode, "find card fail");
        }

        /// <summary>
        /// 选卡
        /// </summary>
        /// <param name="port"></param>        
        /// <returns></returns>
        private IDCardActionResult SelectIDCard(int port)
        {
            var pucSN = new byte[8];
            var retCode = SynjonesIDCardInterop.SelectIDCard(port, pucSN, 0);

            return IsRetSuccess(retCode) ? IDCardActionResultHelper.FormatSuccess<SynjonesIDCardActionResult>(retCode)
                : IDCardActionResultHelper.FormatFail<SynjonesIDCardActionResult>(retCode, "select card fail");
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