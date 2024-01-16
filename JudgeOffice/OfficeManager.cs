using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Offices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice
{
    internal class OfficeManager<T>
        where T : ServiceType
    {
        public Office<T> Office { get; }
        public OrderRequest<T> Order { get; set; }
        public OfficeManager(Office<T> office)
        {
            Office = office;
        }

        
    }
}
