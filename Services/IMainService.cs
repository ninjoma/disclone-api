
namespace disclone_api.Services
{
    public interface IMainService<T,V> where T : class
    {
        Task<V> GetById(int id, bool isActive = true);
        //Task<List<T>> GetList();
        Task<T> EditById(T entryDTO);
        Task<T> Add(T entryDTO);
        Task<T> DeleteById(int id);
    }
}