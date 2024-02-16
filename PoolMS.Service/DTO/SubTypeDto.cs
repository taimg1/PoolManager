namespace PoolMS.Service.DTO;

public class SubTypeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Days { get; set; }

    public int Price { get; set; }
}
public class SubTypeCreateDto
{
    public string Title { get; set; }
    public int Days { get; set; }
    public int Price { get; set; }
}
public class SubTypeUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Days { get; set; }

    public int Price { get; set; }
}