using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Services.Payments
{
    public interface IPaymentService
    {
        Task<CustomerBasketDTO> CreateOrUpdatePaymentIntent(string basketId);
    }
}
