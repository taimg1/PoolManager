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
    public class SubTypeController : ControllerBase
    {
        private readonly IRepository<SubType> _subTypeRepository;
        private readonly IMapper _mapper;
        public SubTypeController(IRepository<SubType> subTypeRepository, IMapper mapper)
        {
            _subTypeRepository = subTypeRepository;
            _mapper = mapper;
        }
        [HttpGet("list")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetAllSubTypes()
        {
            var subTypes = _mapper.Map<IEnumerable<SubTypeDto>>(await _subTypeRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(subTypes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubType(int id)
        {
            var subType = await _subTypeRepository.GetByIdAsync(id);
            if (subType is null)
                return BadRequest("SubType not found");

            var subTypeDto = _mapper.Map<SubTypeDto>(subType);
            return Ok(subTypeDto);
        }
        [HttpPost("add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddSubType(SubTypeCreateDto subTypeCreateDto)
        {
            if (subTypeCreateDto.Title == null)
                return BadRequest("Title cannot be null");
            if (subTypeCreateDto.Price <= 0)
                return BadRequest("Price must be greater than 0");


            var subType = _mapper.Map<SubType>(subTypeCreateDto);

            await _subTypeRepository.AddAsync(subType);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"SubType {subType.Id} added");
        }
        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeleteSubType(int id)
        {
            var subType = await _subTypeRepository.GetByIdAsync(id);
            if (subType is null)
                return BadRequest("SubType not found");

            await _subTypeRepository.DeleteAsync(subType);

            return Ok("SubType was deleted");
        }
        [HttpPut("update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdateSubType(SubTypeUpdateDto subTypeUpdateDto)
        {

            var subType = _mapper.Map<SubType>(subTypeUpdateDto);
            if(!await _subTypeRepository.ExistItem(subType.Id))
                return BadRequest("SubType not found");
            

            if (subTypeUpdateDto.Title == null)
                return BadRequest("Title cannot be null");
            if (subTypeUpdateDto.Price <= 0)
                return BadRequest("Price must be greater than 0");


            await _subTypeRepository.UpdateAsync(subType);

            return Ok("SubType was updated");
        }

    }
}

