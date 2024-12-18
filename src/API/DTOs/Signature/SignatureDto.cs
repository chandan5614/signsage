namespace API.DTOs.Signature
{
    public class SignatureDto
    {
        public int SignatureId { get; set; }
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public DateTime SignedDate { get; set; }
    }
}
