
namespace disclone_api.Services
{
    public interface IMainService<T> where T : class
    {
        Task<T> GetById(int id, bool isActive = true);
        Task<List<T>> GetList();
        Task<T> EditById(T entryDTO);
        Task<T> Add(T entryDTO);
        Task<bool> DeleteById(int id);
    }
}