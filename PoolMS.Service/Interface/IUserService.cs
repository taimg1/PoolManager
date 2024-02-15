using PoolMS.Service.DTO;

namespace PoolMS.Service.Interface
{
    public interface IUserService
    {
        List<UserDto> ItemList { get; set; }
        Task<UserDto> GetByIdAsync(int id);
        Task Registration(UserRegDto entity);
        Task UpdateAsync(UserUpdateDto entity);
        Task DeleteAsync(int id);
        Task GetAllAsync();
        Task<UserDto> GetUser();
    }
}
