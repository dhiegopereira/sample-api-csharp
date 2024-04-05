using sample_api_csharp.Data;
using sample_api_csharp.Interfaces;

namespace sample_api_csharp.Abstracts
{
    public abstract class AbstractRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        protected AbstractRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual TEntity Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual List<TEntity> ReadAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual TEntity ReadOne(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID.");
            }

            var entityExisting = _context.Set<TEntity>().Find(id);

            if (entityExisting == null)
            {
                throw new ArgumentException("Entity not found");
            }

            return entityExisting;
        }

        public virtual TEntity Update(long id, TEntity entity)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID.");
            }

            var existingEntity = _context.Set<TEntity>().Find(id);

            if (existingEntity != null)
            {
                var entityType = typeof(TEntity);
                var properties = entityType.GetProperties()
                    .Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));

                foreach (var property in properties)
                {
                    var value = property.GetValue(entity);
                    property.SetValue(existingEntity, value);
                }

                _context.SaveChanges();
            } else
            {
                throw new ArgumentException("Entity not found for update.");
            }

            return existingEntity;
        }

        public virtual TEntity Delete(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID.");
            }

            var entityToDelete = _context.Set<TEntity>().Find(id);

            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
                _context.SaveChanges();
            } else
            {
                throw new ArgumentException("Entity not found for deletion.");
            }

            return entityToDelete;
        }

    }
}
