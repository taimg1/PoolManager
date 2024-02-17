using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolMS.Core.Entities;
using PoolMS.Repository.Interface;
using PoolMS.Service.DTO;

namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FASController : ControllerBase
    {
        private readonly IRepository<Pool> _poolRepository;
        private readonly IMapper _mapper;

        public FASController(IRepository<Pool> poolRepository,IMapper mapper)
        {
            _poolRepository = poolRepository;
            _mapper = mapper;
        }
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopular()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderByDescending(p => p.TotalCapacity);
            var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools);

            return Ok(poolDtos);
        }
        [HttpGet("descending-popular")]
        public async Task<IActionResult> GetUnpopular()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderBy(p => p.TotalCapacity);
            var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools);

            return Ok(poolDtos);
        }
        [HttpGet("poolsize")]
        public async Task<IActionResult> GetPoolSize()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderByDescending(p => p.Size.Title);
            var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools);

            return Ok(poolDtos);
        }
        [HttpGet("descending-poolsize")]
        public async Task<IActionResult> GetDescendingPoolSize()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderBy(p => p.Size.Title);
            var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools);

            return Ok(poolDtos);
        }
        [HttpGet("temperature")]
        public async Task<IActionResult> GetTemperature()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderByDescending(p => p.Temperature);
            var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools);

            return Ok(poolDtos);
        }
        [HttpGet("descending-temperature")]
        public async Task<IActionResult> GetDescendingTemperature()
        {
            var pools = await _poolRepository.GetAllAsync();
            pools = pools.OrderBy(p => p.Temperature);
            var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools);

            return Ok(poolDtos);
        }
        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchDto searchDto)
        {
            if(double.TryParse(searchDto.Search, out double result))
            {
                var pools = await _poolRepository.GetAllAsync();
                pools = pools.Where(p => p.Temperature == result || p.Size.Width == result ||
                p.Size.Height == result || p.Size.Length == result);
                var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools); 

                return Ok(poolDtos);
            }
            else
            {
                var pools = await _poolRepository.GetAllAsync();
                pools = pools.Where(p => p.Size.Title.ToLower().Contains(searchDto.Search.ToLower()));
                var poolDtos = _mapper.Map<IEnumerable<PoolDto>>(pools); 

                return Ok(poolDtos);
            }
        }
    }
}
