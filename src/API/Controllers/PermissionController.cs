using Microsoft.AspNetCore.Mvc;
using API.DTOs.Permission;
using Core.Entities;
using Core.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        // GET: api/Permission
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            if (permissions == null)
            {
                return NotFound();
            }

            var permissionDtos = _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            return Ok(permissionDtos);
        }

        // GET: api/Permission/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> GetPermission(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            var permissionDto = _mapper.Map<PermissionDto>(permission);
            return Ok(permissionDto);
        }

        // POST: api/Permission
        [HttpPost]
        public async Task<ActionResult<PermissionDto>> CreatePermission([FromBody] CreatePermissionDto createPermissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permission = _mapper.Map<Permission>(createPermissionDto);
            var createdPermission = await _permissionService.CreatePermissionAsync(permission);

            var permissionDto = _mapper.Map<PermissionDto>(createdPermission);
            return CreatedAtAction(nameof(GetPermission), new { id = permissionDto.PermissionId }, permissionDto);
        }

        // PUT: api/Permission/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<PermissionDto>> UpdatePermission(int id, [FromBody] UpdatePermissionDto updatePermissionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            _mapper.Map(updatePermissionDto, permission);
            var updatedPermission = await _permissionService.UpdatePermissionAsync(permission);

            var permissionDto = _mapper.Map<PermissionDto>(updatedPermission);
            return Ok(permissionDto);
        }

        // DELETE: api/Permission/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePermission(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            await _permissionService.DeletePermissionAsync(id);
            return NoContent();
        }
    }
}
