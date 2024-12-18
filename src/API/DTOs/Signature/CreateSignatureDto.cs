namespace API.DTOs.Signature
{
    public class CreateSignatureDto
    {
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public DateTime SignedDate { get; set; }
    }
}
