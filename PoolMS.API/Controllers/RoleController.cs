using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolMS.API.Auth;
using PoolMS.Core.Entities;
using PoolMS.Service.DTO;
using PoolMS.Repository.Interface;

namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        public RoleController(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        [HttpGet("list")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = _mapper.Map<IEnumerable<RoleDto>>(await _roleRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                return BadRequest("Role not found");

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }
        [HttpPost("add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddRole([FromForm] RoleCreateDto roleCreateDto)
        {
            var role = _mapper.Map<Role>(roleCreateDto);
            await _roleRepository.AddAsync(role);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Role {role.Id} added");
        }
        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                return BadRequest("Role not found");

            await _roleRepository.DeleteAsync(role);
            return Ok("Role was deleted");
        }
        [HttpPut("update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdateRole([FromForm] RoleUpdateDto roleUpdateDto)
        {
            if (!await _roleRepository.ExistItem(roleUpdateDto.Id))
                return BadRequest("Role not found");

            var role = _mapper.Map<Role>(roleUpdateDto);

            await _roleRepository.UpdateAsync(role);
            return Ok("Role was updated");
        }
    }
}
