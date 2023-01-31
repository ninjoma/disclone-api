namespace disclone_api.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> List(bool isActive = true);
        Task<T> GetById(int id, bool isActive = true);
        Task<T> Add(T newDto);
        Task<bool> Delete(int id);
        Task<T> Edit(T newDto);
        Task<bool> Save();
    }
}