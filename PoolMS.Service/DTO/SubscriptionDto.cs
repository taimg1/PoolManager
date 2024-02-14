namespace PoolMS.Service.DTO;

public class SubscriptionDto
{
    public int Id { get; set; }
    public LowUserDto User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubTypeDto SubType { get; set; }
}
public class LowSubDto
{
    public SubTypeDto SubType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
public class SubscriptionCreateDto
{
    public int UserId { get; set; }
    public int SubTypeId { get; set; }

    public DateTime EndDate { get; set; }
}
public class SubscriptionCreateUserDto
{
    public int SubTypeId { get; set; }

    public DateTime EndDate { get; set; }
}
public class SubscriptionUpdateDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubTypeId { get; set; }
    public DateTime EndDate { get; set; }
}
