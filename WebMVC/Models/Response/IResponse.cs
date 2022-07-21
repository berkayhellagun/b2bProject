namespace WebMVC.Models.Response
{
    public interface IResponse
    {
        public bool Success { get; }
        public string Message { get;  }
    }
}
