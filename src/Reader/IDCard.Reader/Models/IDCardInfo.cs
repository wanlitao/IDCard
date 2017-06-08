using System;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证数据
    /// </summary>
    public class IDCardData
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public int Nation { get; set; }
        
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCardNo { get; set; }

        /// <summary>
        /// 签发机关
        /// </summary>
        public string IssuingAuthority { get; set; }

        /// <summary>
        /// 有效起始日期
        /// </summary>
        public string ValidBeginDate { get; set; }

        /// <summary>
        /// 有效截止日期
        /// </summary>
        public string ValidEndDate { get; set; }
    }

    /// <summary>
    /// 身份证信息
    /// </summary>
    public class IDCardInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCardNo { get; set; }

        /// <summary>
        /// 签发机关
        /// </summary>
        public string IssuingAuthority { get; set; }

        /// <summary>
        /// 有效起始日期
        /// </summary>
        public DateTime ValidBeginDate { get; set; }

        /// <summary>
        /// 有效截止日期
        /// </summary>
        public DateTime ValidEndDate { get; set; }
    }
}
