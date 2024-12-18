namespace API.DTOs.Document
{
    public class UpdateDocumentDto
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string Content { get; set; }
        public DateTime? UpdatedDate { get; set; }  // Nullable to allow optional update
    }
}
