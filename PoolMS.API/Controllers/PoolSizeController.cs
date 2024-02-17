using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoolMS.API.Auth;
using PoolMS.Core.Entities;
using PoolMS.Repository;
using PoolMS.Repository.Interface;
using PoolMS.Service.DTO;

namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolSizeController : ControllerBase
    {
        private readonly IRepository<PoolSize> _poolSizeRepository;
        private readonly IMapper _mapper;
        public PoolSizeController(IRepository<PoolSize> poolSizeRepository, IMapper mapper)
        {
            _poolSizeRepository = poolSizeRepository;
            _mapper = mapper;
        }
        [HttpGet("list")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetAllPoolSizes()
        {
            var poolSizes = _mapper.Map<IEnumerable<PoolSizeDto>>(await _poolSizeRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(poolSizes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoolSize(int id)
        {
            var poolSize = await _poolSizeRepository.GetByIdAsync(id);
            if (poolSize is null)
                return NotFound("PoolSize not found");

            var poolSizeDto = _mapper.Map<PoolSizeDto>(poolSize);
            return Ok(poolSizeDto);
        }
        [HttpPost("add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddPoolSize(PoolSizeCreateDto poolSizeCreateDto)
        {
            if (poolSizeCreateDto.Title == null)
                return BadRequest("Title cannot be null");
            if (poolSizeCreateDto.Length <= 0 || poolSizeCreateDto.Width <= 0 || poolSizeCreateDto.Height <= 0)
                return BadRequest("Length, Width, Height must be greater than 0");

            var poolSize = _mapper.Map<PoolSize>(poolSizeCreateDto);

            await _poolSizeRepository.AddAsync(poolSize);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"PoolSize {poolSize.Id} added"); 
        }
        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeletePoolSize(int id)
        {
            var poolSize = await _poolSizeRepository.GetByIdAsync(id);
            if (poolSize is null)
                return BadRequest("PoolSize not found");

            await _poolSizeRepository.DeleteAsync(poolSize);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"PoolSize {poolSize.Id} deleted");
        }
        [HttpPut("update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdatePoolSize(PoolSizeUpdateDto poolSizeUpdateDto)
        {
      

            var poolSize = _mapper.Map<PoolSize>(poolSizeUpdateDto);
            if (!await _poolSizeRepository.ExistItem(poolSize.Id))
                return BadRequest("PoolSize not found");
            
            if (poolSizeUpdateDto.Title == null)
                return BadRequest("Title cannot be null");

            if (poolSizeUpdateDto.Length <= 0 || poolSizeUpdateDto.Width <= 0 || poolSizeUpdateDto.Height <= 0)
                return BadRequest("Length, Width, Height must be greater than 0");

            await _poolSizeRepository.UpdateAsync(poolSize);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"PoolSize {poolSize.Id} updated");
        }
    }    
}

