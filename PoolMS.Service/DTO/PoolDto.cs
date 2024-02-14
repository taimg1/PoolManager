namespace PoolMS.Service.DTO;

public class PoolDto
{
    public int Id { get; set; }

    public PoolSizeDto Size { get; set; }
    public int TotalCapacity { get; set; }
    public int Temperature { get; set; }
}
public class PoolCreateDto
{
    public int SizeId { get; set; }
    public int TotalCapacity { get; set; }
    public int Temperature { get; set; }
}
public class PoolUpdateDto
{
    public int Id { get; set; }
    public int SizeId { get; set; }
    public int TotalCapacity { get; set; }
    public int Temperature { get; set; }
}