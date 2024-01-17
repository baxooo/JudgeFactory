using JudgeOffice.Models.FoodModels;
using JudgeOffice.Offices;
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
        public readonly Office<T> OfficeRequester;
        public OrderRequest(Office<T> office)
        {
            Contents = new List<T>();
            OfficeRequester = office;
        }
    }
}
