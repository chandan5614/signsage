using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISignatureService
    {
        // Method to get a signature by its ID
        Signature GetSignatureById(int signatureId);

        // Method to get all signatures for a document
        IEnumerable<Signature> GetSignaturesByDocumentId(int documentId);

        // Method to create a new signature
        void CreateSignature(Signature signature);

        // Method to update an existing signature
        void UpdateSignature(Signature signature);

        // Method to delete a signature
        void DeleteSignature(int signatureId);

        // Method to validate a signature based on certain conditions (e.g., document integrity)
        bool ValidateSignature(int signatureId);

        // Method to retrieve signature status (e.g., pending, signed)
        string GetSignatureStatus(int signatureId);
    }
}
