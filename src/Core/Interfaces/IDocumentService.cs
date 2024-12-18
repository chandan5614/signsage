using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IDocumentService
    {
        // Method to get a list of documents with optional filters (pagination, date range, etc.)
        IEnumerable<Document> GetDocuments(int pageNumber, int pageSize);

        // Method to get a single document by its ID
        Document GetDocumentById(int documentId);

        // Method to create a new document
        void CreateDocument(Document document);

        // Method to update an existing document
        void UpdateDocument(Document document);

        // Method to delete a document
        void DeleteDocument(int documentId);

        // Method to get documents by user ID
        IEnumerable<Document> GetDocumentsByUserId(int userId);

        // Method to get documents based on their status (e.g., active, archived)
        IEnumerable<Document> GetDocumentsByStatus(string status);
    }
}
