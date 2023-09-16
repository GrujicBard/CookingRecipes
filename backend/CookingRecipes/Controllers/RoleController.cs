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
        public IActionResult GetRoles()
        {
            var roles = _mapper.Map<List<RoleDto>>(_roleRepository.GetRoles());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(roles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult GetRole(int id)
        {
            if (!_roleRepository.RoleExists(id))
            {
                return NotFound();
            }

            var role = _mapper.Map<RoleDto>(_roleRepository.GetRole(id));

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            return Ok(role);
        }

        [HttpGet("/users/{roleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersByRoleId(int roleId)
        {
            var users = _mapper.Map<List<UserDto>>(_roleRepository.GetUsersByRoleId(roleId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("/user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [ProducesResponseType(400)]
        public IActionResult GetRoleByUser(int userId)
        {
            var role = _mapper.Map<RoleDto>(_roleRepository.GetRoleByUser(userId));

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            return Ok(role);
        }

    }
}
