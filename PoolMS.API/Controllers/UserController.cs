using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoolMS.API.Auth;
using PoolMS.Core.Entities;
using PoolMS.Repository;
using PoolMS.Service.DTO;
using PoolMS.Service.Interface;



namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(UserRepository userRepository, IMapper mapper, IJwtProvider jwtProvider, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpGet("list")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _mapper.Map<IEnumerable<UserDto>>(await _userRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("info")]
        public async Task<IActionResult> Getinfo()
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetinfoById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return BadRequest("User not found");
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegDto userRegDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);  
            await _userRepository.RegisterAsync(userRegDto);
            return Ok("Succesfull registration!");
        }
        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return BadRequest("User not found");

            await _userRepository.DeleteAsync(user);
            return Ok("User was deleted");
        }
        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);
            if (user is null)
                return BadRequest("User not found");

            await _userRepository.DeleteAsync(user);
            return Ok("User was deleted");
        }
        [HttpPut("admin/update")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdateUserAdmin( UserUpdateDto userUpdateDto)
        {
            if (!await _userRepository.ExistItem(userUpdateDto.Id))
                return BadRequest("User not found");

            var user = await _userRepository.GetByIdAsync(userUpdateDto.Id);

            _mapper.Map(userUpdateDto, user);
            await _userRepository.UpdateAsync(user);
            return Ok("User was updated");
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);

            if (user is null)
                return BadRequest("User not found");

            if (!await _userRepository.ExistItem(userUpdateDto.Id))
                return BadRequest("User not found");

            user = _mapper.Map(userUpdateDto,user);

            await _userRepository.UpdateAsync(user);
            return Ok("User was updated");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login( UserLoginDto userLoginDto)
        {
            var user = await _userRepository.AuthorizeUserAsync(userLoginDto);
            if (user is null)
                return BadRequest("Invalid email or password");

            var token = _jwtProvider.Generate(user);

            return Ok(new { Token = token });
        }
        [HttpPut("admin/role")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> UpdateRole(int userid, int roleid)
        {
           if (!await _userRepository.ExistItem(userid))
                return BadRequest("User not found");

            await _userRepository.ChangeRole(userid, roleid);
            return Ok($"User {userid} get new role {roleid}");

        }
    }
}
