using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Repos
{
    public interface IGenericRepository<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        //Task<TEntity> GetByIdAsync(TKey id);
        //Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> specs);
        Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity, TKey> specs);
        Task<TEntity> GetByNameAsync(ISpecifications<TEntity, TKey> specs);
        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
