namespace IDCard.Reader
{
    /// <summary>
    /// 身份证操作结果
    /// </summary>
    public abstract class IDCardActionResult
    {
        public abstract bool flag { get; }

        public int code { get; set; }

        /// <summary>
        /// error message
        /// </summary>
        public string msg { get; set; }
    }

    public abstract class IDCardActionResult<T> : IDCardActionResult
    {
        public T data { get; set; }
    }
}
