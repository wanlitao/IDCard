using System;
using System.Collections.Generic;

namespace IDCard.Reader.Standard
{
    /// <summary>
    /// 身份证阅读操作返回值（公安部一所）
    /// </summary>
    internal static class StandardIDCardReadRetCode
    {
        private static readonly IDictionary<int, string> _readRetCodeMsgMap = new Dictionary<int, string>();

        #region 构造函数
        static StandardIDCardReadRetCode()
        {
            InitReadRetCodeMsgMapping();
        }

        private static void InitReadRetCodeMsgMapping()
        {
            _readRetCodeMsgMap.Add(1, "正确");
            _readRetCodeMsgMap.Add(0, "读卡错误");            
            _readRetCodeMsgMap.Add(2, "没有最新住址信息");
            _readRetCodeMsgMap.Add(-1, "相片解码错误");
            _readRetCodeMsgMap.Add(-2, "wlt文件后缀错误");
            _readRetCodeMsgMap.Add(-3, "wlt文件打开错误");
            _readRetCodeMsgMap.Add(-4, "wlt文件格式错误");
            _readRetCodeMsgMap.Add(-5, "软件未授权");
            _readRetCodeMsgMap.Add(-11, "无效参数");
            _readRetCodeMsgMap.Add(-12, "路径太长");            
        }
        #endregion

        /// <summary>
        /// 获取返回值文字信息
        /// </summary>
        /// <param name="retCode"></param>
        /// <returns></returns>
        internal static string GetCodeMsg(int retCode)
        {
            if (!_readRetCodeMsgMap.ContainsKey(retCode))
                throw new ArgumentException("invalid retcode", nameof(retCode));

            return _readRetCodeMsgMap[retCode];
        }
    }
}
