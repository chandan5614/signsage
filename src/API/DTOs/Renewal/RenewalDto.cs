namespace API.DTOs.Renewal
{
    public class RenewalDto
    {
        public int RenewalId { get; set; }
        public int DocumentId { get; set; }
        public DateTime RenewalDate { get; set; }
        public string RenewalReason { get; set; }
        public string DocumentName { get; set; }
    }
}
