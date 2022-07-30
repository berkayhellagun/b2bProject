namespace WebMVC.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Status { get; set; } //otomatic binding
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? UserImage { get; set; }
    }
}
