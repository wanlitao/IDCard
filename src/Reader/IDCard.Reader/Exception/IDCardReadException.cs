using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace IDCard.Reader
{
    /// <summary>
    /// 身份证 阅读异常
    /// </summary>
    [Serializable]
    public sealed class IDCardReadException : ExternalException
    {
        public IDCardReadException()
            : base()
        { }

        public IDCardReadException(string message)
            : base(message)
        { }

        public IDCardReadException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public IDCardReadException(string message, int errorCode)
            : base(message, errorCode)
        { }

        public IDCardReadException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        { }
    }
}
