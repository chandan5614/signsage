namespace API.DTOs.Renewal
{
    public class CreateRenewalDto
    {
        public int DocumentId { get; set; }
        public DateTime RenewalDate { get; set; }
        public string RenewalReason { get; set; }
    }
}
