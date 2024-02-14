using Microsoft.AspNetCore.Http;
using PoolMS.Core.Entities;

namespace PoolMS.Service.DTO;

public class PaymentDto
{
    public int Id { get; set; }
    public UserDto User { get; set; }
    public int Amount { get; set; }
    public DateTime PaymentDay { get; set; }
}
public class PaymentCreateDto
{
    public int UserId { get; set; }
    public int Amount { get; set; }
}
public class ImportPaymentDto
{
    public IFormFile formFile { get; set; }
}
public class PaymentUpdateDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Amount { get; set; }
    public DateTime PaymentDay { get; set; }
}
