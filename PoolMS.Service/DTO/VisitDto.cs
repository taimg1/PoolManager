namespace PoolMS.Service.DTO;

public class VisitDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public PoolDto Pool { get; set; }
    public LowUserDto User { get; set; }
    public int StayTime { get; set; }

}
public class LowVisitDto
{
    public PoolDto Pool { get; set; }
    public int StayTime { get; set; }
}
public class VisitCreateDto
{

    public int PoolId { get; set; }
    public int StayTime { get; set; }
    public int UserId { get; set; }
}
public class VisitUpdateDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public int PoolId { get; set; }
    public int StayTime { get; set; }
    public int UserId { get; set; }
}
