using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly SignSageDbContext _context;

        public DocumentRepository(SignSageDbContext context)
        {
            _context = context;
        }

        public async Task<Document> GetDocumentByIdAsync(Guid documentId)
        {
            return await _context.Documents
                .Include(d => d.User)  // Eager loading related User
                .FirstOrDefaultAsync(d => d.DocumentId == documentId);
        }

        public async Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(Guid userId)
        {
            return await _context.Documents
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

        public async Task AddDocumentAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(Guid documentId)
        {
            var document = await GetDocumentByIdAsync(documentId);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
