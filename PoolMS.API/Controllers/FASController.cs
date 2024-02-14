using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolMS.Core.Entities;
using PoolMS.Repository.Interface;

namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FASController : ControllerBase
    {
        private readonly IRepository<Pool> _poolRepository;
        private readonly IRepository<PoolSize> _poolSizeRepository;
        private readonly IMapper _mapper;
        public FASController(IRepository<Pool> poolRepository, IRepository<PoolSize> poolSizeRepository, IMapper mapper)
        {
            _poolRepository = poolRepository;
            _poolSizeRepository = poolSizeRepository;
            _mapper = mapper;
        }
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopular()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderBy(p => p.TotalCapacity);
            return Ok(pools);
        }
        [HttpGet("descending-popular")]
        public async Task<IActionResult> GetUnpopular()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderByDescending(p => p.TotalCapacity);
            return Ok(pools);
        }
        [HttpGet("poolsize")]
        public async Task<IActionResult> GetPoolSize()
        {
            var pool = await _poolRepository.GetAllAsync();
            pool = pool.OrderBy(p => p.Size.Title);
            return Ok(pool);    
        }
        [HttpGet("descending-poolsize")]
        public async Task<IActionResult> GetDescendingPoolSize()
        {
            var pool = await _poolRepository.GetAllAsync();
            pool = pool.OrderByDescending(p => p.Size.Title);
            return Ok(pool);
        }
        [HttpGet("temperature")]
        public async Task<IActionResult> GetTemperature()
        {
            var pool = await _poolRepository.GetAllAsync();
            pool = pool.OrderBy(p=> p.Temperature);
            return Ok(pool);    
        }
        [HttpGet("descending-temperature")]
        public async Task<IActionResult> GetDescendingTemperature()
        {
            var pool = await _poolRepository.GetAllAsync();
            pool = pool.OrderByDescending(p => p.Temperature);
            return Ok(pool);
        }
        [HttpGet("search{value}")]
        public async Task<IActionResult> Search(string value)
        {
            if(double.TryParse(value, out double result))
            {
                var pools = await _poolRepository.GetAllAsync();
                pools = pools.Where(p => p.Temperature == result || p.Size.Width == result ||
                p.Size.Height == result || p.Size.Length == result);
                return Ok(pools);
            }
            else
            {
                var pools = await _poolRepository.GetAllAsync();
                pools = pools.Where(p => p.Size.Title.ToLower().Contains(value.ToLower()));
                return Ok(pools);
            }
        }
    }
}
