namespace WarhammerCore.WebApi.Models.Response
{
    /// <summary>
    /// Response with quick description for front-end about the status of request.
    /// </summary>
    public class BasicResponse
    {
        public bool IsSuccess { get; set; }

        public BasicResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}