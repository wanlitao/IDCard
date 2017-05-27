namespace IDCard.Reader
{
    public class IDCardActionResult
    {
        public bool flag { get { return code == 1; } }

        public int code { get; set; }

        /// <summary>
        /// error message
        /// </summary>
        public string msg { get; set; }
    }

    public class IDCardActionResult<T>
    {
        public T data { get; set; }
    }
}
