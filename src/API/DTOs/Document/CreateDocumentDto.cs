namespace API.DTOs.Document
{
    public class CreateDocumentDto
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string Content { get; set; }
        public int CustomerId { get; set; }  // Assuming a document belongs to a customer
        public DateTime CreatedDate { get; set; }
    }
}
