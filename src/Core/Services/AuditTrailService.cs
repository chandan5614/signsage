using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly IRepository<AuditTrail> _auditTrailRepository;

        public AuditTrailService(IRepository<AuditTrail> auditTrailRepository)
        {
            _auditTrailRepository = auditTrailRepository;
        }

        // Method to create a new audit trail record
        public void CreateAuditTrail(int userId, string action, string details)
        {
            var auditTrail = new AuditTrail
            {
                UserId = userId,
                Action = action,
                Details = details,
                CreatedDate = DateTime.Now
            };

            _auditTrailRepository.Add(auditTrail);
            _auditTrailRepository.Save();
        }

        // Method to retrieve all audit trail records
        public IEnumerable<AuditTrail> GetAllAuditTrails()
        {
            return _auditTrailRepository.GetAll();
        }

        // Method to get audit trails based on a specific user
        public IEnumerable<AuditTrail> GetAuditTrailsByUser(int userId)
        {
            return _auditTrailRepository.Find(a => a.UserId == userId).ToList();
        }

        // Method to get audit trails by action type
        public IEnumerable<AuditTrail> GetAuditTrailsByAction(string action)
        {
            return _auditTrailRepository.Find(a => a.Action == action).ToList();
        }
    }
}
