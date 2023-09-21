using AutoMapper;
using CookingRecipes.Dtos;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using CookingRecipes.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CookingRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController: Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _mapper.Map<List<RoleDto>>(await _roleRepository.GetRoles());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(roles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRole(int id)
        {
            if (!_roleRepository.RoleExists(id))
            {
                return NotFound();
            }

            var role = _mapper.Map<RoleDto>(await _roleRepository.GetRole(id));

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(role);
        }

        [HttpGet("/users/{roleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsersByRoleId(int roleId)
        {
            var users = _mapper.Map<List<UserDto>>(await _roleRepository.GetUsersByRoleId(roleId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("/user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRoleByUser(int userId)
        {
            var role = _mapper.Map<RoleDto>(await _roleRepository.GetRoleByUser(userId));

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            return Ok(role);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto roleCreate)
        {
            if (roleCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (_roleRepository.RoleTypeExists((Data.Enums.RoleType)roleCreate.RoleType))
            {
                ModelState.AddModelError("", "Role already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var roleMap = _mapper.Map<Role>(roleCreate);

            if (!await _roleRepository.CreateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created.");
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleDto updatedRole)
        {
            if (updatedRole == null)
            {
                return BadRequest(ModelState);
            }

            if (roleId != updatedRole.Id)
            if (roleId != updatedRole.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_roleRepository.RoleExists(roleId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roleMap = _mapper.Map<Role>(updatedRole);

            if (!await _roleRepository.UpdateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating role.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
            {
                return NotFound();
            }

            var roleToDelete = await _roleRepository.GetRole(roleId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             
            if (!await _roleRepository.DeleteRole(roleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting role.");
            }

            return NoContent();

        }

    }
}
