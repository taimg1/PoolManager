namespace PoolMS.Service.DTO;

public class ReservationDto
{
    public int Id { get; set; }
    public SubscriptionDto Subscription { get; set; }
    public DateTime Date { get; set; }
    public PoolDto Pool { get; set; }
}
public class ReservationCreateDto
{
    public int SubscriptionId { get; set; }
    public DateTime Date { get; set; }
    public int PoolId { get; set; }
}
public class ReservationUpdateDto
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public DateTime Date { get; set; }
    public int PoolId { get; set; }
}
