using System.Runtime.InteropServices;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证交互接口（公安部一所）
    /// </summary>
    internal static class StandardIDCardInterop
    {
        /// <summary>
        /// 初始化端口
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns></returns>
        [DllImport("termb.dll", EntryPoint = "InitComm")]
        internal static extern int InitComm(int port);

        /// <summary>
        /// 初始化端口 自动查找
        /// </summary>
        /// <returns></returns>
        [DllImport("termb.dll", EntryPoint = "InitCommExt")]
        internal static extern int InitCommExt();

        /// <summary>
        /// 关闭已打开端口
        /// </summary>
        /// <returns></returns>
        [DllImport("termb.dll", EntryPoint = "CloseComm")]
        internal static extern int CloseComm();

        /// <summary>
        /// 卡认证
        /// </summary>
        /// <returns></returns>
        [DllImport("termb.dll", EntryPoint = "Authenticate")]
        internal static extern int Authenticate();

        /// <summary>
        /// 读卡操作
        /// </summary>
        /// <param name="active">读取信息类型</param>
        /// <returns></returns>
        [DllImport("termb.dll", EntryPoint = "Read_Content")]
        internal static extern int Read_Content(int active);

        /// <summary>
        /// 读卡操作 指定目录
        /// </summary>
        /// <param name="cPath">目录</param>
        /// <param name="active">读取信息类型</param>
        /// <returns></returns>
        [DllImport("termb.dll", EntryPoint = "Read_Content_Path")]
        internal static extern int Read_Content_Path(string cPath, int active);
    }
}
