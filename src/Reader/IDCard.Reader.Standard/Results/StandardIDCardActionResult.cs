namespace IDCard.Reader.Standard
{
    /// <summary>
    /// 身份证操作结果（公安部一所）
    /// </summary>
    public class StandardIDCardActionResult : IDCardActionResult
    {       
        public override bool flag { get { return code == 1; } }
    }

    public class StandardIDCardActionResult<T> : IDCardActionResult<T>
    {
        public override bool flag { get { return code == 1; } }
    }
}
