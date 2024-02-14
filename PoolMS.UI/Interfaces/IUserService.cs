using PoolMS.Service.DTO;

namespace PoolMS.UI.Interfaces
{
    public interface IUserService
    {
        List<UserDto> ItemList { get; set; }
        Task<UserDto> GetByIdAsync(int id);
        Task Registration(UserRegDto entity);
        Task UpdateAsync(UserUpdateDto entity);
        Task DeleteAsync(int id);
        Task GetAllAsync();
    }
}
