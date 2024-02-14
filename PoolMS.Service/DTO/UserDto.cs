namespace PoolMS.Service.DTO;

public class UserDto
{
    public int Id { get; set; }
    public string Fname { get; set; }
    public string Sname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public LowSubDto Subscription { get; set; }
    public LowVisitDto Visit { get; set; }
    public RoleDto Role { get; set; }
}
public class LowUserDto
{
    public int Id { get; set; }
    public string Fname { get; set; }
    public string Sname { get; set; }
    public string Email { get; set; }
}
public class UserRegDto
{
    public string Fname { get; set; }
    public string Sname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
}
public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class UserUpdateDto
{
    public int Id { get; set; }
    public string Fname { get; set; }
    public string Sname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
