using API.DTOs.Signature;
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
    public class SignatureController : ControllerBase
    {
        private readonly ISignatureService _signatureService;
        private readonly IMapper _mapper;
        private readonly ILogger<SignatureController> _logger;

        public SignatureController(ISignatureService signatureService, IMapper mapper, ILogger<SignatureController> logger)
        {
            _signatureService = signatureService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Signature
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SignatureDto>>> GetSignatures()
        {
            try
            {
                var signatures = await _signatureService.GetAllSignaturesAsync();
                var signatureDtos = _mapper.Map<IEnumerable<SignatureDto>>(signatures);
                return Ok(signatureDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching signatures");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Signature/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SignatureDto>> GetSignatureById(int id)
        {
            try
            {
                var signature = await _signatureService.GetSignatureByIdAsync(id);
                if (signature == null)
                {
                    return NotFound($"Signature with ID {id} not found.");
                }

                var signatureDto = _mapper.Map<SignatureDto>(signature);
                return Ok(signatureDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching signature with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Signature
        [HttpPost]
        public async Task<ActionResult<SignatureDto>> CreateSignature([FromBody] CreateSignatureDto createSignatureDto)
        {
            try
            {
                if (createSignatureDto == null)
                {
                    return BadRequest("Invalid signature data.");
                }

                var signature = _mapper.Map<Signature>(createSignatureDto);
                var createdSignature = await _signatureService.CreateSignatureAsync(signature);
                var createdSignatureDto = _mapper.Map<SignatureDto>(createdSignature);

                return CreatedAtAction(nameof(GetSignatureById), new { id = createdSignatureDto.Id }, createdSignatureDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating signature");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Signature/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<SignatureDto>> UpdateSignature(int id, [FromBody] UpdateSignatureDto updateSignatureDto)
        {
            try
            {
                if (updateSignatureDto == null || id != updateSignatureDto.Id)
                {
                    return BadRequest("Invalid signature data.");
                }

                var existingSignature = await _signatureService.GetSignatureByIdAsync(id);
                if (existingSignature == null)
                {
                    return NotFound($"Signature with ID {id} not found.");
                }

                var signature = _mapper.Map<Signature>(updateSignatureDto);
                var updatedSignature = await _signatureService.UpdateSignatureAsync(signature);
                var updatedSignatureDto = _mapper.Map<SignatureDto>(updatedSignature);

                return Ok(updatedSignatureDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating signature with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Signature/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSignature(int id)
        {
            try
            {
                var existingSignature = await _signatureService.GetSignatureByIdAsync(id);
                if (existingSignature == null)
                {
                    return NotFound($"Signature with ID {id} not found.");
                }

                await _signatureService.DeleteSignatureAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting signature with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
