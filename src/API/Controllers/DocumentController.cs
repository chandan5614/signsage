using API.DTOs.Document;
using API.Services;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService, IMapper mapper, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Document
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
        {
            try
            {
                var documents = await _documentService.GetAllDocumentsAsync();
                var documentDtos = _mapper.Map<IEnumerable<DocumentDto>>(documents);
                return Ok(documentDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching documents");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Document/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocumentById(int id)
        {
            try
            {
                var document = await _documentService.GetDocumentByIdAsync(id);
                if (document == null)
                {
                    return NotFound($"Document with ID {id} not found.");
                }

                var documentDto = _mapper.Map<DocumentDto>(document);
                return Ok(documentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching document with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Document
        [HttpPost]
        public async Task<ActionResult<DocumentDto>> CreateDocument([FromBody] CreateDocumentDto createDocumentDto)
        {
            try
            {
                if (createDocumentDto == null)
                {
                    return BadRequest("Invalid document data.");
                }

                var document = _mapper.Map<Document>(createDocumentDto);
                var createdDocument = await _documentService.CreateDocumentAsync(document);
                var createdDocumentDto = _mapper.Map<DocumentDto>(createdDocument);

                return CreatedAtAction(nameof(GetDocumentById), new { id = createdDocumentDto.Id }, createdDocumentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating document");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Document/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentDto>> UpdateDocument(int id, [FromBody] UpdateDocumentDto updateDocumentDto)
        {
            try
            {
                if (updateDocumentDto == null || id != updateDocumentDto.Id)
                {
                    return BadRequest("Invalid document data.");
                }

                var existingDocument = await _documentService.GetDocumentByIdAsync(id);
                if (existingDocument == null)
                {
                    return NotFound($"Document with ID {id} not found.");
                }

                var document = _mapper.Map<Document>(updateDocumentDto);
                var updatedDocument = await _documentService.UpdateDocumentAsync(document);
                var updatedDocumentDto = _mapper.Map<DocumentDto>(updatedDocument);

                return Ok(updatedDocumentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating document with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Document/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocument(int id)
        {
            try
            {
                var existingDocument = await _documentService.GetDocumentByIdAsync(id);
                if (existingDocument == null)
                {
                    return NotFound($"Document with ID {id} not found.");
                }

                await _documentService.DeleteDocumentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting document with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
