namespace PoolMS.Service.Interface
{
    public interface IService<T, TCreateDto, TUpdateDto> where T : class where TCreateDto : class where TUpdateDto : class
    {
        List<T> ItemList { get; set; }
        Task GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(TCreateDto entity);
        Task UpdateAsync(TUpdateDto entity);
        Task DeleteAsync(int id);
    }

}
