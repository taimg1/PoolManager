using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PoolMS.API.Auth;
using PoolMS.Core.Entities;
using PoolMS.Repository.Interface;
using PoolMS.Repository.Repository;
using PoolMS.Service.DataConvertor;
using PoolMS.Service.DTO;
using PoolMS.Service.Interface;
using PoolMS.Service.ReportGenerator;
using System.Text;

namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController: ControllerBase
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
     

        public PaymentController(IRepository<Payment> paymentRepository, IMapper mapper, UserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;

        }
       
        [HttpGet("list")]
        [RoleAuth(Role ="Admin")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = _mapper.Map<IEnumerable<PaymentDto>>(await _paymentRepository.GetAllAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(payments);
        }
        [HttpGet("info")]
        public async Task<IActionResult> Getinfo()
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);

            var rowPayments = await _paymentRepository.GetAllAsync();
            var payments = _mapper.Map<IEnumerable<PaymentDto>>(rowPayments.Where(x => x.User == user));
            return Ok(payments);
        }
        [HttpPost("admin/add")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> AddPaymentAdmin(PaymentCreateDto paymentCreateDto)
        {
            var payment = _mapper.Map<Payment>(paymentCreateDto);
            if(!await _paymentRepository.ExistItem(payment.Id))
                return BadRequest("Payment already exist");

            var user = await _userRepository.GetByIdAsync(paymentCreateDto.UserId);
            if (paymentCreateDto.Amount <= 0)
                return BadRequest("Amount must be greater than 0");

            if(user is null)
                return BadRequest("User not found");

            payment.User = user;
            payment.PaymentDay = DateTime.UtcNow;

            await _paymentRepository.AddAsync(payment);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(true);
        }
        [HttpPost("pay")]
        [Authorize]
        public async Task<IActionResult> AddPayment(PaymentCreateUserDto paymentCreateUserDto)
        {
            var email = _httpContextAccessor.HttpContext.Items["email"].ToString();
            var user = await _userRepository.GetByEmail(email);
            if (user is null)
                return BadRequest("User not found");

            var payment = new Payment
            {
                User = user,
                Amount = paymentCreateUserDto.Amount,
                PaymentDay = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(true);

        }
        [HttpDelete("delete/{id}")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> DeletePayment( int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);

            if (payment is null)
                return NotFound("Payment not found");

            await _paymentRepository.DeleteAsync(payment);

            return Ok($"Payment {payment.Id} deleted");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment is null)
                return NotFound("Payment not found");

            var paymentDto = _mapper.Map<PaymentDto>(payment);
            return Ok(paymentDto);
        }
    
        [HttpGet("paymentreport")]
        [RoleAuth(Role = "Admin")]
        public async Task<IActionResult> GetPaymentReport()
        {
            var payments = await _paymentRepository.GetAllAsync();
            var reportPath = ReportGenerator.GeneratePaymentReport(payments.ToList());
            var fileBytes = System.IO.File.ReadAllBytes(reportPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "PaymentReport.docx");
        }
    }
}
