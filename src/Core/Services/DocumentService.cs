using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;
        private readonly IRepository<AuditTrail> _auditTrailRepository;

        public DocumentService(IRepository<Document> documentRepository, IRepository<AuditTrail> auditTrailRepository)
        {
            _documentRepository = documentRepository;
            _auditTrailRepository = auditTrailRepository;
        }

        // Method to create a new document
        public void CreateDocument(Document document, int userId)
        {
            document.CreatedDate = DateTime.Now;
            _documentRepository.Add(document);
            _documentRepository.Save();

            // Log the document creation in the audit trail
            _auditTrailRepository.Add(new AuditTrail
            {
                UserId = userId,
                Action = "Create Document",
                Details = $"Document {document.DocumentId} created.",
                CreatedDate = DateTime.Now
            });
            _auditTrailRepository.Save();
        }

        // Method to update an existing document
        public void UpdateDocument(Document document, int userId)
        {
            var existingDocument = _documentRepository.GetById(document.DocumentId);
            if (existingDocument == null)
                throw new Exception("Document not found");

            existingDocument.Title = document.Title;
            existingDocument.Content = document.Content;
            existingDocument.UpdatedDate = DateTime.Now;

            _documentRepository.Update(existingDocument);
            _documentRepository.Save();

            // Log the document update in the audit trail
            _auditTrailRepository.Add(new AuditTrail
            {
                UserId = userId,
                Action = "Update Document",
                Details = $"Document {document.DocumentId} updated.",
                CreatedDate = DateTime.Now
            });
            _auditTrailRepository.Save();
        }

        // Method to delete a document
        public void DeleteDocument(int documentId, int userId)
        {
            var document = _documentRepository.GetById(documentId);
            if (document == null)
                throw new Exception("Document not found");

            _documentRepository.Delete(document);
            _documentRepository.Save();

            // Log the document deletion in the audit trail
            _auditTrailRepository.Add(new AuditTrail
            {
                UserId = userId,
                Action = "Delete Document",
                Details = $"Document {document.DocumentId} deleted.",
                CreatedDate = DateTime.Now
            });
            _auditTrailRepository.Save();
        }

        // Method to retrieve a document by ID
        public Document GetDocumentById(int documentId)
        {
            return _documentRepository.GetById(documentId);
        }

        // Method to retrieve all documents
        public IEnumerable<Document> GetAllDocuments()
        {
            return _documentRepository.GetAll();
        }

        // Method to retrieve documents by a specific user
        public IEnumerable<Document> GetDocumentsByUser(int userId)
        {
            return _documentRepository.Find(d => d.UserId == userId).ToList();
        }
    }
}
