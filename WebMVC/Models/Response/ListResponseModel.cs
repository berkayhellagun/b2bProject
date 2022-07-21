namespace WebMVC.Models.Response
{
    public class ListResponseModel<T> : ResponseModel
    {
        private readonly List<T> _data;
        
    }
}
