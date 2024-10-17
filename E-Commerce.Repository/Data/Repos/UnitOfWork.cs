using E_Commerce.Core.Models;
using E_Commerce.Repository.Data.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;
        Hashtable repositories;

        public UnitOfWork(StoreDbContext context)
        {
            this.context = context;
            repositories = new Hashtable();
        }
        public IGenericRepository<TEntity, TKey>? genericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!repositories.ContainsKey(typeof(TEntity).Name))
            {
                var repository = new GenericRepository<TEntity, TKey>(context);
                repositories.Add(type, repository);
            }
            return repositories[type] as IGenericRepository<TEntity, TKey>;
        }

        public Task<int> SaveAsync() => context.SaveChangesAsync();
    }
}
