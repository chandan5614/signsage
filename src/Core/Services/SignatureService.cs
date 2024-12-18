using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class SignatureService : ISignatureService
    {
        private readonly IRepository<Signature> _signatureRepository;
        private readonly IRepository<AuditTrail> _auditTrailRepository;

        public SignatureService(IRepository<Signature> signatureRepository, IRepository<AuditTrail> auditTrailRepository)
        {
            _signatureRepository = signatureRepository;
            _auditTrailRepository = auditTrailRepository;
        }

        // Method to create a new signature
        public void CreateSignature(Signature signature, int userId)
        {
            signature.CreatedDate = DateTime.Now;
            _signatureRepository.Add(signature);
            _signatureRepository.Save();

            // Log the signature creation in the audit trail
            _auditTrailRepository.Add(new AuditTrail
            {
                UserId = userId,
                Action = "Create Signature",
                Details = $"Signature {signature.SignatureId} created for document {signature.DocumentId}.",
                CreatedDate = DateTime.Now
            });
            _auditTrailRepository.Save();
        }

        // Method to update an existing signature
        public void UpdateSignature(Signature signature, int userId)
        {
            var existingSignature = _signatureRepository.GetById(signature.SignatureId);
            if (existingSignature == null)
                throw new Exception("Signature not found");

            existingSignature.SignedDate = signature.SignedDate;
            existingSignature.SignatureStatus = signature.SignatureStatus;
            existingSignature.UpdatedDate = DateTime.Now;

            _signatureRepository.Update(existingSignature);
            _signatureRepository.Save();

            // Log the signature update in the audit trail
            _auditTrailRepository.Add(new AuditTrail
            {
                UserId = userId,
                Action = "Update Signature",
                Details = $"Signature {signature.SignatureId} updated for document {signature.DocumentId}.",
                CreatedDate = DateTime.Now
            });
            _auditTrailRepository.Save();
        }

        // Method to delete a signature
        public void DeleteSignature(int signatureId, int userId)
        {
            var signature = _signatureRepository.GetById(signatureId);
            if (signature == null)
                throw new Exception("Signature not found");

            _signatureRepository.Delete(signature);
            _signatureRepository.Save();

            // Log the signature deletion in the audit trail
            _auditTrailRepository.Add(new AuditTrail
            {
                UserId = userId,
                Action = "Delete Signature",
                Details = $"Signature {signature.SignatureId} deleted for document {signature.DocumentId}.",
                CreatedDate = DateTime.Now
            });
            _auditTrailRepository.Save();
        }

        // Method to retrieve a signature by ID
        public Signature GetSignatureById(int signatureId)
        {
            return _signatureRepository.GetById(signatureId);
        }

        // Method to retrieve all signatures
        public IEnumerable<Signature> GetAllSignatures()
        {
            return _signatureRepository.GetAll();
        }

        // Method to retrieve signatures by a specific document ID
        public IEnumerable<Signature> GetSignaturesByDocument(int documentId)
        {
            return _signatureRepository.Find(s => s.DocumentId == documentId).ToList();
        }
    }
}
