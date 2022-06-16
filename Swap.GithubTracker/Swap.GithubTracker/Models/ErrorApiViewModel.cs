namespace Swap.GithubTracker.Services.Api.Models
{
    public class ErrorApiViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorApiViewModel(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
