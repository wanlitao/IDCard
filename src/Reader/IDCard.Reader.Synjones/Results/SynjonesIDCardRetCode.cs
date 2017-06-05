using System;
using System.Collections.Generic;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证操作返回值（新中新）
    /// </summary>
    internal static class SynjonesIDCardRetCode
    {
        private static readonly IDictionary<int, string> _retCodeMsgMap = new Dictionary<int, string>();

        #region 构造函数
        static SynjonesIDCardRetCode()
        {
            InitRetCodeMsgMapping();
        }

        private static void InitRetCodeMsgMapping()
        {
            _retCodeMsgMap.Add(0x90, "操作成功");
            _retCodeMsgMap.Add(0x91, "居民身份证中无此项内容");
            _retCodeMsgMap.Add(0x9F, "寻找居民身份证成功");
            _retCodeMsgMap.Add(0x01, "端口打开失败/端口尚未打开/端口号不合法");
            _retCodeMsgMap.Add(0x02, "PC接收超时，在规定的时间内未接收到规定长度的数据");
            _retCodeMsgMap.Add(0x03, "数据传输错误");
            _retCodeMsgMap.Add(0x05, "SAM_A串口不可用");
            _retCodeMsgMap.Add(0x09, "打开文件失败");
            _retCodeMsgMap.Add(0x10, "接收业务终端数据的校验和错");
            _retCodeMsgMap.Add(0x11, "接收业务终端数据的长度错");
            _retCodeMsgMap.Add(0x21, "接收业务终端的命令错误");
            _retCodeMsgMap.Add(0x23, "越权操作");
            _retCodeMsgMap.Add(0x24, "无法识别的错误");
            _retCodeMsgMap.Add(0x80, "寻找居民身份证失败");
            _retCodeMsgMap.Add(0x81, "选取居民身份证失败");
            _retCodeMsgMap.Add(0x31, "居民身份证认证SAM_A失败");
            _retCodeMsgMap.Add(0x32, "SAM_A认证居民身份证失败");
            _retCodeMsgMap.Add(0x33, "信息验证失败");
            _retCodeMsgMap.Add(0x37, "指纹信息验证错误");
            _retCodeMsgMap.Add(0x3F, "信息长度错误");
            _retCodeMsgMap.Add(0x40, "无法识别的居民身份证类型");
            _retCodeMsgMap.Add(0x41, "读居民身份证操作失败");
            _retCodeMsgMap.Add(0x47, "取随机数失败");
            _retCodeMsgMap.Add(0x60, "SAM_A自检失败，不能接收命令");
            _retCodeMsgMap.Add(0x66, "SAM_A没经过授权，无法使用");            
        }
        #endregion

        /// <summary>
        /// 获取返回值文字信息
        /// </summary>
        /// <param name="retCode"></param>
        /// <returns></returns>
        internal static string GetCodeMsg(int retCode)
        {
            if (!_retCodeMsgMap.ContainsKey(retCode))
                throw new ArgumentException("invalid retcode", nameof(retCode));

            return _retCodeMsgMap[retCode];
        }
    }
}
