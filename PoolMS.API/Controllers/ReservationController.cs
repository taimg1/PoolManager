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
    public class ReservationController : ControllerBase
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<Pool> _poolRepository;
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserRepository _userRepository;

        public ReservationController(IRepository<Reservation> reservationRepository, IMapper mapper ,
            IRepository<Pool> poolRepository, IRepository<Subscription> subscriptionRepository, 
            IHttpContextAccessor httpContextAccessor, UserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _poolRepository = poolRepository;
            _subscriptionRepository = subscriptionRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        [HttpGet("list")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = _mapper.Map<IEnumerable<ReservationDto>>(await _reservationRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reservations);
        }
        [HttpGet("info")]
        public async Task<IActionResult> Getinfo()
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);
            
            var rowReservations = await _reservationRepository.GetAllAsync();
            var reservations = _mapper.Map<IEnumerable<ReservationDto>>(rowReservations.Where(x=> x.Subscription.User == user));
            return Ok(reservations);
        }
        [HttpGet("{id}")]  
        
        public async Task<IActionResult> GetReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation is null)
                return BadRequest("Reservation not found");

            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return Ok(reservationDto);
        }
        [HttpPost("admin/add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddReservationAdmin(ReservationCreateDto reservationCreateDto)
        {

            var reservation = _mapper.Map<Reservation>(reservationCreateDto);
            var subscription = await _subscriptionRepository.GetByIdAsync(reservationCreateDto.SubscriptionId);
            
            if(subscription is null)
                return BadRequest("Subscription not found");

      
            reservation.Subscription = subscription;
            await _reservationRepository.AddAsync(reservation);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Reservation {reservation.Id} added");
        }
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddReservation(ReservationCreateDto reservationCreateDto)
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);

            var reservation = _mapper.Map<Reservation>(reservationCreateDto);
            var subscription = await _subscriptionRepository.GetByIdAsync(reservationCreateDto.SubscriptionId);

            if(subscription.User != user)
                return BadRequest("Subscription not found");


            if (subscription is null)
                return BadRequest("Subscription not found");

 
            reservation.Subscription = subscription;
            await _reservationRepository.AddAsync(reservation);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Reservation {reservation.Id} added");
        }
        
        [HttpDelete("admin/delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation is null)
                return BadRequest("Reservation not found");

            await _reservationRepository.DeleteAsync(reservation);

            return Ok("Reservation was deleted");
        }
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservationUser(int id)
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);

            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation is null)
                return BadRequest("Reservation not found");

            if (reservation.Subscription.User != user)
                return BadRequest("Reservation not found");

            await _reservationRepository.DeleteAsync(reservation);

            return Ok("Reservation was deleted");
        }
        [HttpPut("admin/update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdateReservation( ReservationUpdateDto reservationUpdateDto)
        {

            var reservation = _mapper.Map<Reservation>(reservationUpdateDto);
            if (!await _reservationRepository.ExistItem(reservation.Id))
                return BadRequest("Reservation not found");
            
            var subscription = await _subscriptionRepository.GetByIdAsync(reservationUpdateDto.SubscriptionId);

            if (subscription is null)
                return BadRequest("Subscription not found");

  
            reservation.Subscription = subscription;
            await _reservationRepository.UpdateAsync(reservation);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Reservation {reservation.Id} updated");
        }
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateReservationUser(ReservationUpdateDto reservationUpdateDto)
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);

            var reservation = _mapper.Map<Reservation>(reservationUpdateDto);
            if (!await _reservationRepository.ExistItem(reservation.Id))
                return BadRequest("Reservation not found");
            
            var subscription = await _subscriptionRepository.GetByIdAsync(reservationUpdateDto.SubscriptionId);


            if (subscription is null)
                return BadRequest("Subscription not found");

            if (subscription.User != user)
                return BadRequest("Subscription not found");
   
            reservation.Subscription = subscription;
            await _reservationRepository.UpdateAsync(reservation);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok($"Reservation {reservation.Id} updated");
        }
    }
}
