using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class VisitController : ControllerBase
    {
        private readonly IRepository<Visit> _visitRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<Pool> _poolRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserRepository _userRepository;
        public VisitController(IRepository<Visit> visitRepository, IMapper mapper, IRepository<Reservation> reservationRepository, 
            IRepository<Pool> poolRepository, IHttpContextAccessor httpContextAccessor, UserRepository userRepository)
        {
            _visitRepository = visitRepository;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _poolRepository = poolRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        [HttpGet("list")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetAllVisits()
        {
            var visits = _mapper.Map<IEnumerable<VisitDto>>(await _visitRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(visits);
        }
        [HttpGet("info")]
        [Authorize]
        public async Task<IActionResult> Getinfo()
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);
            
            var rowvisits = await _visitRepository.GetAllAsync();
            var visits = _mapper.Map<IEnumerable<VisitDto>>(rowvisits.Where(x=> x.User == user));
            return Ok(visits);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisit(int id)
        {
            var visit = await _visitRepository.GetByIdAsync(id);
            if (visit is null)
                return BadRequest("Visit not found");

            var visitDto = _mapper.Map<VisitDto>(visit);
            return Ok(visitDto);
        }

        [HttpPost("add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddVisit([FromForm] VisitCreateDto visitCreateDto)
        {
            var visit = _mapper.Map<Visit>(visitCreateDto);

            var pool = await _poolRepository.GetByIdAsync(visitCreateDto.PoolId);
            if (pool is null)
                return BadRequest("Pool not found");

            visit.Pool = pool;
            if (visitCreateDto.ReservationId == null)
            {

                await _visitRepository.AddAsync(visit);
                return Ok("Visit was added");
            }
            else
            {
                var reservation = await _reservationRepository.GetByIdAsync(visitCreateDto.ReservationId);
                if (reservation is null)
                    return BadRequest("Reservation not found");

                visit.Reservation = reservation;
                await _visitRepository.AddAsync(visit);
                return Ok("Visit was added");
            }
        }

        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            var visit = await _visitRepository.GetByIdAsync(id);
            if (visit is null)
                return BadRequest("Visit not found");

            await _visitRepository.DeleteAsync(visit);

            return Ok("Visit was deleted");
        }
        [HttpPut("update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdateVisit([FromForm] VisitUpdateDto visitUpdateDto)
        {

            var visit = _mapper.Map<Visit>(visitUpdateDto);
            if (!await _visitRepository.ExistItem(visit.Id))
                return BadRequest("Visit not found");
            

            var pool = await _poolRepository.GetByIdAsync(visitUpdateDto.PoolId);
            if (pool is null)
                return BadRequest("Pool not found");

            visit.Pool = pool;
            if (visitUpdateDto.ReservationId == null)
            {
                await _visitRepository.UpdateAsync(visit);
                return Ok("Visit was updated");
            }
            else
            {
                var reservation = await _reservationRepository.GetByIdAsync(visitUpdateDto.ReservationId);
                if (reservation is null)
                    return BadRequest("Reservation not found");

                visit.Reservation = reservation;
                await _visitRepository.UpdateAsync(visit);
                return Ok("Visit was updated");
            }
        }

    }    
}

