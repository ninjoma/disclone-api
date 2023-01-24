namespace disclone_api.Repositories
{
    public interface IMainRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> List();
        Task<T> GetById(int id);
        Task<T> Add(T newDto);
        Task<bool> Delete(int id);
        Task<T> Edit(T newDto);
        Task<bool> Save();
    }
}