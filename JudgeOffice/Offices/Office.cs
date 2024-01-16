using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Offices
{
    internal abstract class Office<T>
        where T : ServiceType
    {
        public abstract Provider<T> GetServices();
        public abstract Task<Order<T>> SendOrder(OrderRequest<T> order, Provider<T> provider);
    }
}
