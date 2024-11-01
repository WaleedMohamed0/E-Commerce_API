using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Order
{
    public enum OrderStatus
    {
        // To make sure that the value of the enum is the same as the string value
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed  
    }
}
