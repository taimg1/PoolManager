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
    public class SubscriptionController : ControllerBase
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IRepository<SubType> _subtypesRepository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SubscriptionController(IRepository<Subscription> subscriptionRepository, IMapper mapper, IRepository<SubType> subtypesRepository, UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _subtypesRepository = subtypesRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("list")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var subscriptions = _mapper.Map<IEnumerable<SubscriptionDto>>(await _subscriptionRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(subscriptions);
        }
        [HttpGet("info")]
        public async Task<IActionResult> Getinfo()
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);
            
            var rowSubscriptions = await _subscriptionRepository.GetAllAsync();
            var subscriptions = _mapper.Map<IEnumerable<SubscriptionDto>>(rowSubscriptions.Where(x=> x.User.Id == user.Id));
            return Ok(subscriptions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscription(int id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            if (subscription is null)
                return NotFound("Subscription not found");

            var subscriptionDto = _mapper.Map<SubscriptionDto>(subscription);
            return Ok(subscriptionDto);
        }
        [HttpPost("admin/add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddSubscription(SubscriptionCreateDto subscriptionCreateDto)
        {
           var subscription = _mapper.Map<Subscription>(subscriptionCreateDto);

           var subType = await _subtypesRepository.GetByIdAsync(subscriptionCreateDto.SubTypeId);
           var user = await _userRepository.GetByIdAsync(subscriptionCreateDto.UserId);

            if(subType is null)
                return BadRequest("SubType not found");
            if(user is null)
                return BadRequest("User not found");

            subscription.SubType = subType;
            subscription.User = user;
            await _subscriptionRepository.AddAsync(subscription);
            return Ok("Subscription was added");
        }
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddSubUser(SubscriptionCreateUserDto subscriptionCreateUserDto)
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);

            var subscription = _mapper.Map<Subscription>(subscriptionCreateUserDto);
            var subType = await _subtypesRepository.GetByIdAsync(subscriptionCreateUserDto.SubTypeId);
            if(subType is null)
                return BadRequest("SubType not found");

            subscription.EndDate = DateTime.UtcNow.AddDays(subType.Days);
            subscription.StartDate = DateTime.UtcNow;
            if(subscription.StartDate >= subscription.EndDate)
                return BadRequest("Start date must be less than end date");

            subscription.SubType = subType;
            subscription.User = user;
            await _subscriptionRepository.AddAsync(subscription);

            return Ok("Subscription was added");
        }
        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);

            if (subscription is null)
                return BadRequest("Subscription not found");

            await _subscriptionRepository.DeleteAsync(subscription);
            return Ok("Subscription was deleted");
        }
        [HttpPut("update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdateSubscription(SubscriptionUpdateDto subscriptionUpdateDto)
        {
            var subscription = _mapper.Map<Subscription>(subscriptionUpdateDto);
            if (!await _subscriptionRepository.ExistItem(subscription.Id))
                return BadRequest("Subscription not found");
            
            var subType = await _subtypesRepository.GetByIdAsync(subscriptionUpdateDto.SubTypeId);
            var user = await _userRepository.GetByIdAsync(subscriptionUpdateDto.UserId);
            if(subType is null)
                return BadRequest("SubType not found");
            if(user is null)
                return BadRequest("User not found");
            if (subscription.StartDate >= subscription.EndDate)
                return BadRequest("Start date must be less than end date");
            subscription.SubType = subType;
            subscription.User = user;

            await _subscriptionRepository.UpdateAsync(subscription);
            return Ok("Subscription was updated");
        }
    }
    
}

