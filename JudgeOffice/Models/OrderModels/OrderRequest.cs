using JudgeOffice.Models.FoodModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.OrderModels
{
    internal class OrderRequest<T> : Order<T>
        where T : ServiceType
    {
        public OrderRequest()
        {
            Contents = new List<T>();
        }
    }
}
