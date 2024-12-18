using Microsoft.AspNetCore.Mvc;
using API.DTOs.Template;
using Core.Entities;
using Core.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly IMapper _mapper;

        public TemplateController(ITemplateService templateService, IMapper mapper)
        {
            _templateService = templateService;
            _mapper = mapper;
        }

        // GET: api/Template
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateDto>>> GetTemplates()
        {
            var templates = await _templateService.GetAllTemplatesAsync();
            if (templates == null)
            {
                return NotFound();
            }

            var templateDtos = _mapper.Map<IEnumerable<TemplateDto>>(templates);
            return Ok(templateDtos);
        }

        // GET: api/Template/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateDto>> GetTemplate(int id)
        {
            var template = await _templateService.GetTemplateByIdAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            var templateDto = _mapper.Map<TemplateDto>(template);
            return Ok(templateDto);
        }

        // POST: api/Template
        [HttpPost]
        public async Task<ActionResult<TemplateDto>> CreateTemplate([FromBody] CreateTemplateDto createTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = _mapper.Map<Template>(createTemplateDto);
            var createdTemplate = await _templateService.CreateTemplateAsync(template);

            var templateDto = _mapper.Map<TemplateDto>(createdTemplate);
            return CreatedAtAction(nameof(GetTemplate), new { id = templateDto.TemplateId }, templateDto);
        }

        // PUT: api/Template/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TemplateDto>> UpdateTemplate(int id, [FromBody] UpdateTemplateDto updateTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await _templateService.GetTemplateByIdAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            _mapper.Map(updateTemplateDto, template);
            var updatedTemplate = await _templateService.UpdateTemplateAsync(template);

            var templateDto = _mapper.Map<TemplateDto>(updatedTemplate);
            return Ok(templateDto);
        }

        // DELETE: api/Template/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemplate(int id)
        {
            var template = await _templateService.GetTemplateByIdAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            await _templateService.DeleteTemplateAsync(id);
            return NoContent();
        }
    }
}
