using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoolMS.API.Auth;
using PoolMS.Core.Entities;
using PoolMS.Repository;
using PoolMS.Repository.Interface;
using PoolMS.Service.DTO;
using PoolMS.Service.ReportGenerator;

namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolController : ControllerBase
    {
        private readonly IRepository<Pool> _poolRepository;
        private readonly IRepository<PoolSize> _poolSizeRepository;
        private readonly IRepository<Visit> _visitRepository;
        private readonly IMapper _mapper;
        public PoolController(IRepository<Pool> poolRepository, IMapper mapper , IRepository<PoolSize> poolSizeRepository, IRepository<Visit> visitRepository)
        {
            _poolRepository = poolRepository;
            _mapper = mapper;
            _poolSizeRepository = poolSizeRepository;
            _visitRepository = visitRepository;
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
                return NotFound("Pool not found");

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
        [HttpGet("poolusagereport/{Id}")]
        public async Task<IActionResult> GetPoolUsageReport(int Id)
        {
            var pool = await _poolRepository.GetByIdAsync(Id);
            var filePath = ReportGenerator.GeneratePoolUsageReport(pool);

            var stream = new FileStream(filePath, FileMode.Open);
            var result = new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "PoolUsageReport.docx"
            };

            // Delete the temporary file
            stream.Close();
            System.IO.File.Delete(filePath);

            return result;
        }

    }
}

