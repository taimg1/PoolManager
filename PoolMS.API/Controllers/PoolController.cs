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
    public class PoolController : ControllerBase
    {
        private readonly IRepository<Pool> _poolRepository;
        private readonly IRepository<PoolSize> _poolSizeRepository;
        private readonly IMapper _mapper;
        public PoolController(IRepository<Pool> poolRepository, IMapper mapper , IRepository<PoolSize> poolSizeRepository)
        {
            _poolRepository = poolRepository;
            _mapper = mapper;
            _poolSizeRepository = poolSizeRepository;
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetAllPools()
        {
            var pools = _mapper.Map<IEnumerable<PoolDto>>(await _poolRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pools);
        }
        [HttpPost("add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddPool(PoolCreateDto poolCreateDto)
        {
            var pool = _mapper.Map<Pool>(poolCreateDto);
            var poolSize = await _poolSizeRepository.GetByIdAsync(poolCreateDto.SizeId);
            if (pool.TotalCapacity < 0)
                return BadRequest("Capacity must be greater than 0");

            if (poolSize is null)
                return BadRequest("PoolSize not found");

            pool.Size = poolSize;
            await _poolRepository.AddAsync(pool);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Pool {pool.Id} added");
        }
        [HttpGet("{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetPool(int id)
        {
            var pool = await _poolRepository.GetByIdAsync(id);
            if (pool is null)
                return BadRequest("Pool not found");

            var poolDto = _mapper.Map<PoolDto>(pool);
            return Ok(poolDto);
        }
        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeletePool(int id)
        {
            var pool = await _poolRepository.GetByIdAsync(id);
            if (pool is null)
                return BadRequest("Pool not found");

            await _poolRepository.DeleteAsync(pool);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Pool {pool.Id} deleted");
        }
        [HttpPut("update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdatePool(PoolUpdateDto poolDto)
        {
         
            var pool = _mapper.Map<Pool>(poolDto);
            if (!await _poolRepository.ExistItem(pool.Id))
                return BadRequest("Pool not found");
            var poolSize = await _poolSizeRepository.GetByIdAsync(poolDto.SizeId);
            

            if (poolDto.TotalCapacity < 0)
                return BadRequest("Capacity must be greater than 0");

            if (poolSize is null)
                return BadRequest("PoolSize not found");

            pool.Size = poolSize;
            await _poolRepository.UpdateAsync(pool);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Pool {pool.Id} updated");
        }
    }
}

