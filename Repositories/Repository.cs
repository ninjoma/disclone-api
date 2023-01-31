using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Repositories
{
    public class Repository<T, V> : IRepository<T> where T : class where V : class
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<V> dbSet;
        private bool disposed = false;

        public Repository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            dbSet = _context.Set<V>();
        }

        public async Task<T> Add(T newDto)
        {
            var newEntity = _mapper.Map<V>(newDto);
            await dbSet.AddAsync(newEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<T>(newEntity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<T> Edit(T newDto)
        {
            var entity = _mapper.Map<V>(newDto);
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return _mapper.Map<T>(entity);
        }

        public async Task<T> GetById(int id)
        {
            return _mapper.Map<T>(await dbSet.FindAsync(id));
        }

        public async Task<IEnumerable<T>> List()
        {
            return _mapper.Map<IEnumerable<T>>(await dbSet.ToListAsync());
        }

        public async Task<bool> Save()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}