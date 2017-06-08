using FCP.Util;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IDCard.Reader
{
    public static class IDCardDataExtensions
    {
        private static readonly IDictionary<int, string> _sexMap = new Dictionary<int, string>();
        private static readonly IDictionary<int, string> _nationMap = new Dictionary<int, string>();

        #region 构造函数
        static IDCardDataExtensions()
        {
            InitSexMapping();
            InitNationMapping();
        }

        private static void InitSexMapping()
        {
            _sexMap.Add(0, "未知");
            _sexMap.Add(1, "男");
            _sexMap.Add(2, "女");
            _sexMap.Add(9, "未说明");
        }

        private static void InitNationMapping()
        {
            _nationMap.Add(01, "汉");
            _nationMap.Add(02, "蒙古");
            _nationMap.Add(03, "回");
            _nationMap.Add(04, "藏");
            _nationMap.Add(05, "维吾尔");
            _nationMap.Add(06, "苗");
            _nationMap.Add(07, "彝");
            _nationMap.Add(08, "壮");
            _nationMap.Add(09, "布依");
            _nationMap.Add(10, "朝鲜");
            _nationMap.Add(11, "满");
            _nationMap.Add(12, "侗");
            _nationMap.Add(13, "瑶");
            _nationMap.Add(14, "白");
            _nationMap.Add(15, "土家");
            _nationMap.Add(16, "哈尼");
            _nationMap.Add(17, "哈萨克");
            _nationMap.Add(18, "傣");
            _nationMap.Add(19, "黎");
            _nationMap.Add(20, "傈僳");
            _nationMap.Add(21, "佤");
            _nationMap.Add(22, "畲");
            _nationMap.Add(23, "高山");
            _nationMap.Add(24, "拉祜");
            _nationMap.Add(25, "水");
            _nationMap.Add(26, "东乡");
            _nationMap.Add(27, "纳西");
            _nationMap.Add(28, "景颇");
            _nationMap.Add(29, "柯尔克孜");
            _nationMap.Add(30, "土");
            _nationMap.Add(31, "达斡尔");
            _nationMap.Add(32, "仫佬");
            _nationMap.Add(33, "羌");
            _nationMap.Add(34, "布朗");
            _nationMap.Add(35, "撒拉");
            _nationMap.Add(36, "毛南");
            _nationMap.Add(37, "仡佬");
            _nationMap.Add(38, "锡伯");
            _nationMap.Add(39, "阿昌");
            _nationMap.Add(40, "普米");
            _nationMap.Add(41, "塔吉克");
            _nationMap.Add(42, "怒");
            _nationMap.Add(43, "乌孜别克");
            _nationMap.Add(44, "俄罗斯");
            _nationMap.Add(45, "鄂温克");
            _nationMap.Add(46, "德昂");
            _nationMap.Add(47, "保安");
            _nationMap.Add(48, "裕固");
            _nationMap.Add(49, "京");
            _nationMap.Add(50, "塔塔尔");
            _nationMap.Add(51, "独龙");
            _nationMap.Add(52, "鄂伦春");
            _nationMap.Add(53, "赫哲");
            _nationMap.Add(54, "门巴");
            _nationMap.Add(55, "珞巴");
            _nationMap.Add(56, "基诺");
        }
        #endregion

        #region Helper Functions
        internal static string GetSexString(int sexCode)
        {
            if (!_sexMap.ContainsKey(sexCode))
                throw new ArgumentException("invalid sex code", nameof(sexCode));

            return _sexMap[sexCode];
        }

        internal static string GetNationString(int nationCode)
        {
            if (!_nationMap.ContainsKey(nationCode))
                throw new ArgumentException("invalid nation code", nameof(nationCode));

            return _nationMap[nationCode];
        }

        internal static DateTime ParseIDCardDateString(string cardDateString)
        {
            if (cardDateString.isNullOrEmpty())
                throw new ArgumentNullException(nameof(cardDateString));

            return DateTime.ParseExact(cardDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        internal static DateTime ParseIDCardValidEndDateString(string cardDateString)
        {
            if (cardDateString.isNullOrEmpty())
                throw new ArgumentNullException(nameof(cardDateString));

            if (StringUtil.compareIgnoreCase(cardDateString, "长期"))
                return DateTime.MaxValue;

            return ParseIDCardDateString(cardDateString);
        }
        #endregion

        /// <summary>
        /// 格式化身份证信息
        /// </summary>
        /// <param name="cardData"></param>
        /// <returns></returns>
        public static IDCardInfo FormatCardInfo(this IDCardData cardData)
        {
            if (cardData == null)
                throw new ArgumentNullException(nameof(cardData));

            return new IDCardInfo
            {
                Name = cardData.Name,
                Sex = GetSexString(cardData.Sex),
                Nation = GetNationString(cardData.Nation),
                Birthday = ParseIDCardDateString(cardData.Birthday),
                Address = cardData.Address,
                IDCardNo = cardData.IDCardNo,
                IssuingAuthority = cardData.IssuingAuthority,
                ValidBeginDate = ParseIDCardDateString(cardData.ValidBeginDate),
                ValidEndDate = ParseIDCardValidEndDateString(cardData.ValidEndDate)
            };
        }
    }
}
