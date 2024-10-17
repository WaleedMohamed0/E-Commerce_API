using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Repos
{
    public interface IUnitOfWork
    {
        // This method is used to get the generic repository of the entity
        IGenericRepository<TEntity,TKey> genericRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;
        // This method is used to save the changes in the database
        Task<int> SaveAsync();
    }
}
