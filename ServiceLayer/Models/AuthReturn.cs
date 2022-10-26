namespace ServiceLayer.Models
{
    public class AuthReturn
    {
        public string Token{ get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsValid{ get; set; }

    }
}
