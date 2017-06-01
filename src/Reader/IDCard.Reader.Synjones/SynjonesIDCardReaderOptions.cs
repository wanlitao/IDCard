using FCP.Util;
using System;

namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证阅读设置（新中新）
    /// </summary>
    public class SynjonesIDCardReaderOptions
    {
        public SynjonesIDCardReaderOptions()
        {           
            PhotoPath_CurrentBaseDirectory()
                .PhotoFormat_Wlt()
                .PhotoName_IDCardNameWithNo();
        }

        #region 照片路径
        internal SynjonesIDCardPhotoPathType PhotoPathType { get; private set; }

        internal string PhotoPath { get; private set; }

        public SynjonesIDCardReaderOptions PhotoPath_SystmeDiskRoot()
        {
            PhotoPathType = SynjonesIDCardPhotoPathType.SystemDiskRoot;
            PhotoPath = string.Empty;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoPath_CurrentBaseDirectory()
        {
            PhotoPathType = SynjonesIDCardPhotoPathType.CurrentBaseDirectory;
            PhotoPath = string.Empty;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoPath_SpecialDirectory(string specialDirectory)
        {
            if (specialDirectory.isNullOrEmpty())
                throw new ArgumentNullException(nameof(specialDirectory));

            PhotoPathType = SynjonesIDCardPhotoPathType.SpecialDirectory;
            PhotoPath = specialDirectory;

            return this;
        }
        #endregion

        #region 照片存储格式
        internal SynjonesIDCardPhotoContentFormat PhotoContentFormat { get; private set; }

        public SynjonesIDCardReaderOptions PhotoFormat_Bmp()
        {
            PhotoContentFormat = SynjonesIDCardPhotoContentFormat.Bmp;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoFormat_Jpeg()
        {
            PhotoContentFormat = SynjonesIDCardPhotoContentFormat.Jpeg;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoFormat_Base64()
        {
            PhotoContentFormat = SynjonesIDCardPhotoContentFormat.Base64;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoFormat_Wlt()
        {
            PhotoContentFormat = SynjonesIDCardPhotoContentFormat.Wlt;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoFormat_None()
        {
            PhotoContentFormat = SynjonesIDCardPhotoContentFormat.None;

            return this;
        }
        #endregion

        #region 照片文件名
        internal SynjonesIDCardPhotoNameType PhotoNameType { get; private set; }

        internal string PhotoName { get; private set; }

        public SynjonesIDCardReaderOptions PhotoName_Temp()
        {
            PhotoNameType = SynjonesIDCardPhotoNameType.Tmp;
            PhotoName = string.Empty;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoName_IDCardName()
        {
            PhotoNameType = SynjonesIDCardPhotoNameType.IDCardName;
            PhotoName = string.Empty;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoName_IDCardNo()
        {
            PhotoNameType = SynjonesIDCardPhotoNameType.IDCardNo;
            PhotoName = string.Empty;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoName_IDCardNameWithNo()
        {
            PhotoNameType = SynjonesIDCardPhotoNameType.IDCardNameWithNo;
            PhotoName = string.Empty;

            return this;
        }

        public SynjonesIDCardReaderOptions PhotoName_SpecialName(string specialName)
        {
            if (specialName.isNullOrEmpty())
                throw new ArgumentNullException(nameof(specialName));

            PhotoNameType = SynjonesIDCardPhotoNameType.SpecialName;
            PhotoName = specialName;

            return this;
        }
        #endregion
    }
}
