namespace IDCard.Reader.Synjones
{
    /// <summary>
    /// 身份证操作结果（新中新）
    /// </summary>
    public class SynjonesIDCardActionResult : IDCardActionResult
    {
        public override bool flag { get { return code == 0; } }
    }

    public class SynjonesIDCardActionResult<T> : IDCardActionResult<T>
    {
        public override bool flag { get { return code == 0; } }
    }
}
