using API.DTOs.AuditTrail;
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
    public class AuditTrailController : ControllerBase
    {
        private readonly IAuditTrailService _auditTrailService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuditTrailController> _logger;

        public AuditTrailController(IAuditTrailService auditTrailService, IMapper mapper, ILogger<AuditTrailController> logger)
        {
            _auditTrailService = auditTrailService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/AuditTrail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditTrailDto>>> GetAuditTrails()
        {
            try
            {
                var auditTrails = await _auditTrailService.GetAllAuditTrailsAsync();
                var auditTrailDtos = _mapper.Map<IEnumerable<AuditTrailDto>>(auditTrails);
                return Ok(auditTrailDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching audit trails");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/AuditTrail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuditTrailDto>> GetAuditTrailById(int id)
        {
            try
            {
                var auditTrail = await _auditTrailService.GetAuditTrailByIdAsync(id);
                if (auditTrail == null)
                {
                    return NotFound($"Audit Trail with ID {id} not found.");
                }

                var auditTrailDto = _mapper.Map<AuditTrailDto>(auditTrail);
                return Ok(auditTrailDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching audit trail with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/AuditTrail
        [HttpPost]
        public async Task<ActionResult<AuditTrailDto>> CreateAuditTrail([FromBody] CreateAuditTrailDto createAuditTrailDto)
        {
            try
            {
                if (createAuditTrailDto == null)
                {
                    return BadRequest("Invalid audit trail data.");
                }

                var auditTrail = _mapper.Map<AuditTrail>(createAuditTrailDto);
                var createdAuditTrail = await _auditTrailService.CreateAuditTrailAsync(auditTrail);
                var createdAuditTrailDto = _mapper.Map<AuditTrailDto>(createdAuditTrail);

                return CreatedAtAction(nameof(GetAuditTrailById), new { id = createdAuditTrailDto.Id }, createdAuditTrailDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating audit trail");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/AuditTrail/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<AuditTrailDto>> UpdateAuditTrail(int id, [FromBody] CreateAuditTrailDto updateAuditTrailDto)
        {
            try
            {
                if (updateAuditTrailDto == null || id != updateAuditTrailDto.Id)
                {
                    return BadRequest("Invalid audit trail data.");
                }

                var existingAuditTrail = await _auditTrailService.GetAuditTrailByIdAsync(id);
                if (existingAuditTrail == null)
                {
                    return NotFound($"Audit Trail with ID {id} not found.");
                }

                var auditTrail = _mapper.Map<AuditTrail>(updateAuditTrailDto);
                var updatedAuditTrail = await _auditTrailService.UpdateAuditTrailAsync(auditTrail);
                var updatedAuditTrailDto = _mapper.Map<AuditTrailDto>(updatedAuditTrail);

                return Ok(updatedAuditTrailDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating audit trail with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/AuditTrail/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuditTrail(int id)
        {
            try
            {
                var existingAuditTrail = await _auditTrailService.GetAuditTrailByIdAsync(id);
                if (existingAuditTrail == null)
                {
                    return NotFound($"Audit Trail with ID {id} not found.");
                }

                await _auditTrailService.DeleteAuditTrailAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting audit trail with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
