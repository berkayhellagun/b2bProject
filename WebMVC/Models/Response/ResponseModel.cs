namespace WebMVC.Models.Response
{
    public class ResponseModel : IResponse
    {
        public bool Success { get; }

        public string Message { get; }
    }
}
