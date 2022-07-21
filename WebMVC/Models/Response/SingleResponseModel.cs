namespace WebMVC.Models.Response
{
    public class SingleResponseModel<T> : ResponseModel
    {
        public T Data { get; }
    }
}
