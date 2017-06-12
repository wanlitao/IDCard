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
        protected const string DefaultBmpPhotoFileName = "xp.bmp";

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

            ExecOptionsSettings();
        }

        /// <summary>
        /// 执行Options设置
        /// </summary>
        private void ExecOptionsSettings()
        {
            SynjonesIDCardInterop.SetPhotoPath((int)_options.PhotoPathType, _options.PhotoPath);
            SynjonesIDCardInterop.SetPhotoType((int)_options.PhotoContentFormat);
            SynjonesIDCardInterop.SetPhotoName((int)_options.PhotoNameType, _options.PhotoName);
        }
        #endregion

        #region Helper Functions
        /// <summary>
        /// 获取交互处理程序
        /// </summary>
        /// <returns></returns>
        private SynjonesIDCardInteropHandler GetInteropHandler()
        {
            return _options.Port.HasValue ? new SynjonesIDCardInteropHandler(_options.Port.Value)
                : new SynjonesIDCardInteropHandler();
        }

        /// <summary>
        /// 获取阅读交互处理程序
        /// </summary>
        /// <returns></returns>
        private SynjonesIDCardInteropReadHandler GetInteropReadHandler()
        {
            return _options.Port.HasValue ? new SynjonesIDCardInteropReadHandler(_options.Port.Value)
                : new SynjonesIDCardInteropReadHandler();
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
            var txtFilePath = IOHelper.GetFilePath(fileDirectory, DefaultTextFileName);
            var photoFilePath = IOHelper.GetFilePath(fileDirectory, DefaultPhotoFileName);

            using (var interopHandler = GetInteropReadHandler())
            {
                return interopHandler.ExecIDCardInteropReadAction(
                    (port) => SynjonesIDCardInteropAction.ReadBaseMsg(port, false, txtFilePath, photoFilePath));
            }
        }
        #endregion

        #region 读最新地址信息
        /// <summary>
        /// 读最新地址信息
        /// </summary>
        /// <param name="fileDirectory">文件输出目录</param>
        /// <returns></returns>
        protected override IDCardActionResult ReadNewAddressInfoInternal(string fileDirectory)
        {
            var newAddressFilePath = IOHelper.GetFilePath(fileDirectory, DefaultNewAddressFileName);

            using (var interopHandler = GetInteropReadHandler())
            {
                return interopHandler.ExecIDCardInteropReadAction(
                    (port) => SynjonesIDCardInteropAction.ReadNewAppMsg(port, false, newAddressFilePath));
            }
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
            var txtFilePath = IOHelper.GetFilePath(fileDirectory, DefaultTextFileName);
            var fileBytes = IOHelper.ReadFileContent(txtFilePath);

            var cardData = ParseIDCardTextBytes(fileBytes);
            return cardData.FormatCardInfo();
        }

        /// <summary>
        /// 解析照片信息
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns>BMP照片路径</returns>
        protected override IDCardActionResult ParsePhotoInfoInternal(string fileDirectory)
        {
            var photoFilePath = IOHelper.GetFilePath(fileDirectory, DefaultPhotoFileName);

            var interopHandler = GetInteropHandler();
            return interopHandler.ExecIDCardInteropAction((port) => SynjonesIDCardInteropAction.GetBmp(port, photoFilePath));            
        }

        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <param name="fileDirectory">最新地址信息所属目录</param>
        /// <returns></returns>
        protected override string ParseNewAddressInfoInternal(string fileDirectory)
        {
            var newAddressFilePath = IOHelper.GetFilePath(fileDirectory, DefaultNewAddressFileName);
            var fileBytes = IOHelper.ReadFileContent(newAddressFilePath);

            return ParseNewAddressBytes(fileBytes);
        }
        #endregion

        #region 获取照片路径
        /// <summary>
        /// 获取Bmp照片路径
        /// </summary>
        /// <param name="fileDirectory">照片信息所属目录</param>
        /// <returns></returns>
        protected override string GetBmpPhotoPathInternal(string fileDirectory)
        {
            return IOHelper.GetFilePath(fileDirectory, DefaultBmpPhotoFileName);
        }
        #endregion
    }
}