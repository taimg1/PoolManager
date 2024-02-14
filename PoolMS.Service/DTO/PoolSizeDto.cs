namespace PoolMS.Service.DTO;

public class PoolSizeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Length { get; set; }
}
public class PoolSizeCreateDto
{
    public string Title { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Length { get; set; }
}
public class PoolSizeUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Length { get; set; }
}