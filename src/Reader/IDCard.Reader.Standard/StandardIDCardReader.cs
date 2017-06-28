using System;

namespace IDCard.Reader.Standard
{
    /// <summary>
    /// 身份证阅读（公安部一所）
    /// </summary>
    public class StandardIDCardReader : IDCardReader
    {
        protected const string DefaultBmpPhotoFileName = "zp.bmp";

        private int? _port;

        #region 构造函数
        public StandardIDCardReader()
        { }

        public StandardIDCardReader(int port)
        {
            if (port < 1 || port > 9999)
                throw new ArgumentOutOfRangeException(nameof(port), "port must between 1 and 9999");

            _port = port;
        }
        #endregion

        #region Helper Functions
        /// <summary>
        /// 获取交互处理程序
        /// </summary>
        /// <returns></returns>
        private IIDCardInteropHandler GetInteropHandler()
        {
            return _port.HasValue ? new StandardIDCardInteropHandler(_port.Value)
                : new StandardIDCardInteropHandler();
        }

        /// <summary>
        /// 获取阅读交互处理程序
        /// </summary>
        /// <returns></returns>
        private IIDCardInteropReadHandler GetInteropReadHandler()
        {
            return _port.HasValue ? new StandardIDCardInteropReadHandler(_port.Value)
                : new StandardIDCardInteropReadHandler();
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
            using (var interopHandler = GetInteropReadHandler())
            {
                return interopHandler.ExecIDCardInteropReadAction(
                    (port) => StandardIDCardInteropAction.ReadContentPath(fileDirectory, (int)StandardIDCardReadActiveType.TextAndWlt));
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
            using (var interopHandler = GetInteropReadHandler())
            {
                return interopHandler.ExecIDCardInteropReadAction(
                    (port) => StandardIDCardInteropAction.ReadContentPath(fileDirectory, (int)StandardIDCardReadActiveType.NewAddress));
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
            return ParseTextInfoInternal(fileDirectory, DefaultTextFileName);
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
            return interopHandler.ExecIDCardInteropAction((port) => StandardIDCardInteropAction.GetPhoto(photoFilePath));
        }

        /// <summary>
        /// 解析最新地址信息
        /// </summary>
        /// <param name="fileDirectory">最新地址信息所属目录</param>
        /// <returns></returns>
        protected override string ParseNewAddressInfoInternal(string fileDirectory)
        {
            return ParseNewAddressInfoInternal(fileDirectory, DefaultNewAddressFileName);
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
