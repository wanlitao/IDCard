namespace IDCard.Reader
{
    public static class IDCardActionResultHelper
    {
        public static TIDCardActionResult FormatSuccess<TIDCardActionResult>(int retCode)
            where TIDCardActionResult : IDCardActionResult, new()
        {
            var actionResult = new TIDCardActionResult() { code = retCode };           

            return actionResult;
        }

        public static TIDCardActionResult FormatSuccess<TIDCardActionResult, TResultData>(int retCode, TResultData resultData)
            where TIDCardActionResult : IDCardActionResult<TResultData>, new()
        {
            var actionResult = new TIDCardActionResult() { code = retCode, data = resultData };

            return actionResult;
        }

        public static TIDCardActionResult FormatFail<TIDCardActionResult>(int retCode, string errMsg)
            where TIDCardActionResult : IDCardActionResult, new()
        {
            var actionResult = new TIDCardActionResult() { code = retCode, msg = errMsg };

            return actionResult;
        }
    }
}
