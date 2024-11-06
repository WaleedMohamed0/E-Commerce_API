using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Repos
{
    public interface IBasketRepository
    {
        Task<CustomerBasketDTO?> GetBasketAsync(string basketId);
        Task<CustomerBasketDTO?> UpdateBasketAsync(CustomerBasketDTO basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
