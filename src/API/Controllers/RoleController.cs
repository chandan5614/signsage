using Microsoft.AspNetCore.Mvc;
using API.DTOs.Role;
using Core.Entities;
using Core.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            if (roles == null)
            {
                return NotFound();
            }

            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(roleDtos);
        }

        // GET: api/Role/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }

        // POST: api/Role
        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = _mapper.Map<Role>(createRoleDto);
            var createdRole = await _roleService.CreateRoleAsync(role);

            var roleDto = _mapper.Map<RoleDto>(createdRole);
            return CreatedAtAction(nameof(GetRole), new { id = roleDto.RoleId }, roleDto);
        }

        // PUT: api/Role/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> UpdateRole(int id, [FromBody] UpdateRoleDto updateRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _mapper.Map(updateRoleDto, role);
            var updatedRole = await _roleService.UpdateRoleAsync(role);

            var roleDto = _mapper.Map<RoleDto>(updatedRole);
            return Ok(roleDto);
        }

        // DELETE: api/Role/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
