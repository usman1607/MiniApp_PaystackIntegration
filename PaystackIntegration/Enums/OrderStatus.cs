using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Enums
{
    public enum OrderStatus
    {
        Default = 1,
        Delivered,
        Cancelled,
        InProgress
    }
}
