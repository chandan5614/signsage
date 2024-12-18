namespace API.DTOs.Document
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string Content { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
