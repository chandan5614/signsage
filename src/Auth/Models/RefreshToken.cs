namespace Auth.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UserId { get; set; }
    }
}
