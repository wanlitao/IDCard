using System.Runtime.InteropServices;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证交互接口（新中新）
    /// </summary>
    internal static class SynjonesIDCardInterop
    {
        #region 端口
        /// <summary>
        /// 自动寻找读卡器
        /// </summary>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_FindReader", CharSet = CharSet.Ansi)]
        public static extern int FindReader();

        /// <summary>
        /// 自动寻找USB读卡器
        /// </summary>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_FindUSBReader", CharSet = CharSet.Ansi)]
        internal static extern int FindUSBReader();

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_OpenPort", CharSet = CharSet.Ansi)]
        internal static extern int OpenPort(int iPort);

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ClosePort", CharSet = CharSet.Ansi)]
        internal static extern int ClosePort(int iPort);
        #endregion

        #region 找卡
        /// <summary>
        /// 开始寻卡
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pucIIN">读到的IIN</param>
        /// <param name="iIfOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_StartFindIDCard", CharSet = CharSet.Ansi)]
        internal static extern int StartFindIDCard(int iPort, [In, Out] byte[] pucIIN, int iIfOpen);

        /// <summary>
        /// 选卡
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pucSN">读到的SN/param>
        /// <param name="iIfOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SelectIDCard", CharSet = CharSet.Ansi)]
        internal static extern int SelectIDCard(int iPort, [In, Out] byte[] pucSN, int iIfOpen);
        #endregion

        #region 读卡
        /// <summary>
        /// 读取身份证内基本信息区域信息
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pucCHMsg">读到的文字信息</param>
        /// <param name="puiCHMsgLen">读到的文字信息长度</param>
        /// <param name="pucPHMsg">读到的照片信息</param>
        /// <param name="puiPHMsgLen">读到的照片信息长度</param>
        /// <param name="iIfOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadBaseMsg", CharSet = CharSet.Ansi)]
        internal static extern int ReadBaseMsg(int iPort, [In, Out] byte[] pucCHMsg, ref uint puiCHMsgLen, [In, Out] byte[] pucPHMsg, ref uint puiPHMsgLen, int iIfOpen);

        /// <summary>
        /// 读取身份证内基本信息区域信息并写入指定文件
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pcCHMsgFileName">文字信息写入文件名</param>
        /// <param name="puiCHMsgFileLen">存储文字信息的文件的长度</param>
        /// <param name="pcPHMsgFileName">照片信息写入文件名</param>
        /// <param name="puiPHMsgFileLen">存储照片信息的文件的长度</param>
        /// <param name="iIfOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadBaseMsgToFile", CharSet = CharSet.Ansi)]
        internal static extern int ReadBaseMsgToFile(int iPort, string pcCHMsgFileName, ref uint puiCHMsgFileLen, string pcPHMsgFileName, ref uint puiPHMsgFileLen, int iIfOpen);

        /// <summary>
        /// 读取追加地址信息
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pucAppMsg">读到的追加地址信息</param>
        /// <param name="puiAppMsgLen">读到的追加地址信息长度，最长70字节</param>
        /// <param name="iIfOpen">是否在函数内部打开和关闭端口</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadNewAppMsg", CharSet = CharSet.Ansi)]
        internal static extern int ReadNewAppMsg(int iPort, [In, Out] byte[] pucAppMsg, ref uint puiAppMsgLen, int iIfOpen);

        /// <summary>
        /// 将wlt文件解码成bmp文件
        /// </summary>
        /// <param name="Wlt_File">wlt文件名</param>
        /// <param name="intf">阅读设备通讯接口类型（1—RS-232C，2—USB）</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_GetBmp", CharSet = CharSet.Ansi)]
        internal static extern int GetBmp(string Wlt_File, int intf);

        /// <summary>
        /// 读取身份证文字信息和照片信息，并按设置格式格式化
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="iIfOpen">是否在函数内部打开和关闭端口</param>
        /// <param name="pIDCardData">身份证信息结构体</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadMsg", CharSet = CharSet.Ansi)]
        internal static extern int ReadMsg(int iPort, int iIfOpen, ref SynjonesIDCardData pIDCardData);
        #endregion

        #region 设置
        /// <summary>
        /// 设置照片存储路径
        /// </summary>
        /// <param name="iOption">0=C:根目录，1=当前路径，2=指定路径</param>
        /// <param name="cPhotoPath">路径名</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoPath", CharSet = CharSet.Ansi)]
        internal static extern int SetPhotoPath(int iOption, string cPhotoPath);

        /// <summary>
        /// 设置照片文件存储的格式
        /// </summary>
        /// <param name="iType">0=bmp，1=jpeg，2=base64，3=WLT，4=不生成图片</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoType", CharSet = CharSet.Ansi)]
        internal static extern int SetPhotoType(int iType);

        /// <summary>
        /// 设置照片文件的文件名
        /// </summary>
        /// <param name="iType">0=tmp，1=姓名，2=身份证号，3=姓名_身份证号，4=指定文件名（不包括后缀）</param>
        /// <param name="cPhotoname">指定的图片文件名，不包括后缀</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoNameEx", CharSet = CharSet.Ansi)]
        internal static extern int SetPhotoName(int iType, string cPhotoname);

        /// <summary>
        /// 设置返回性别的格式
        /// </summary>
        /// <param name="iType">0=卡内存储的数据，1=解释之后的数据</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetSexType", CharSet = CharSet.Ansi)]
        internal static extern int SetSexType(int iType);

        /// <summary>
        /// 设置返回民族的格式
        /// </summary>
        /// <param name="iType">0=卡内存储的数据，1=解释之后的数据，2=解释之后+“族”</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetNationType", CharSet = CharSet.Ansi)]
        internal static extern int SetNationType(int iType);

        /// <summary>
        /// 设置返回出生日期的格式
        /// </summary>
        /// <param name="iType">0=YYYYMMDD,1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetBornType", CharSet = CharSet.Ansi)]
        internal static extern int SetBornType(int iType);

        /// <summary>
        /// 设置返回有效开始日期的格式
        /// </summary>
        /// <param name="iType">0=YYYYMMDD,1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetUserLifeBType", CharSet = CharSet.Ansi)]
        internal static extern int SetUserLifeBType(int iType);

        /// <summary>
        /// 设置返回有效结束日期的格式
        /// </summary>
        /// <param name="iType">0=YYYYMMDD,1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD</param>
        /// <param name="iOption">0=长期不转换 1=长期转换为 有效期开始加50年</param>
        /// <returns></returns>
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetUserLifeEType", CharSet = CharSet.Ansi)]
        internal static extern int SetUserLifeEType(int iType, int iOption);
        #endregion
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct SynjonesIDCardData
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] //结构中使用的字串，[]类似限定说明 说明此公共结构传输到非托管代码时封装定义
        internal string Name; //姓名   
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
        internal string Sex;   //性别
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        internal string Nation; //民族
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        internal string Born; //出生日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
        internal string Address; //住址
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        internal string IDCardNo; //身份证号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        internal string GrantDept; //发证机关
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        internal string UserLifeBegin; // 有效开始日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
        internal string UserLifeEnd;  // 有效截止日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
        internal string reserved; // 保留
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        internal string PhotoFileName; // 照片路径
    }
}
