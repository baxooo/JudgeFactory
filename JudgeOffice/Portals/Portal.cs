using JudgeOffice.Models;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Portals
{
    internal abstract class Portal<T>
        where T : ServiceType
    {
        public abstract Provider<T> CheckServices();
        public abstract Task<Order<T>> SendOrder(OrderRequest<T> order,Provider<T> provider);
    }
}
