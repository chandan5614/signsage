using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAuditTrailService
    {
        // Method to get a list of audit logs with optional filters (pagination, date range, etc.)
        IEnumerable<AuditLog> GetAuditLogs(int pageNumber, int pageSize);

        // Method to get a single audit log by ID
        AuditLog GetAuditLogById(int auditLogId);

        // Method to create a new audit log entry
        void CreateAuditLog(AuditLog auditLog);

        // Method to update an existing audit log (if needed)
        void UpdateAuditLog(AuditLog auditLog);

        // Method to delete an audit log entry
        void DeleteAuditLog(int auditLogId);

        // Method to get audit logs by user or document, useful for filtering based on specific entities
        IEnumerable<AuditLog> GetAuditLogsByUserId(int userId);
        IEnumerable<AuditLog> GetAuditLogsByDocumentId(int documentId);
    }
}
