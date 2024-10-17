using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;
using E_Commerce.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository.Data.Repos
{
    public class GenericRepository<TEntity, TKey> :
            IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext context;
        public GenericRepository(StoreDbContext storeDbContext)
        {
            context = storeDbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await context.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Remove(entity);
        }

        //public async Task<IEnumerable<TEntity>?> GetAllAsync()
        //{
        //    if (typeof(TEntity) == typeof(Product))
        //    {
        //        return await context.Set<Product>()
        //            .Include(p => p.Brand)
        //            .Include(p => p.Type)
        //            .ToListAsync() as IEnumerable<TEntity>;
        //    }
        //    return await context.Set<TEntity>().ToListAsync();
        //}

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> specs)
        {
            return await ApplySpecifications(specs).ToListAsync();
        }

        //public async Task<TEntity?> GetByIdAsync(TKey id)
        //{
        //    if (typeof(TEntity) == typeof(Product))
        //    {
        //        return await context.Set<Product>()
        //            .Include(p => p.Brand)
        //            .Include(p => p.Type)
        //            .FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
        //    }
        //    return await context.Set<TEntity>().FindAsync(id);
        //}

        public async Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity, TKey> specs)
        {
            return await ApplySpecifications(specs).FirstOrDefaultAsync();
        }

        public Task<TEntity> GetByNameAsync(ISpecifications<TEntity, TKey> specs)
        {
            return ApplySpecifications(specs).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            context.Update(entity);
        }
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> specs)
            => SpecificationsEvaluator.GetQuery(context.Set<TEntity>(), specs);
        
    }
}
