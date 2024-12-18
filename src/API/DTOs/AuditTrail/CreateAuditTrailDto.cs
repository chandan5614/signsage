namespace API.DTOs.AuditTrail
{
    public class CreateAuditTrailDto
    {
        public string Action { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string Changes { get; set; }
    }
}
