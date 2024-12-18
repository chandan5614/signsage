namespace API.DTOs.AuditTrail
{
    public class AuditTrailDto
    {
        public int AuditTrailId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string Changes { get; set; }
    }
}
