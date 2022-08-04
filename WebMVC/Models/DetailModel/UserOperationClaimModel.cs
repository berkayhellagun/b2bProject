namespace WebMVC.Models.DetailModel
{
    public class UserOperationClaimModel
    {
        public int UserOperationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OperationId { get; set; }
        public string OperationName { get; set; }
    }
}
