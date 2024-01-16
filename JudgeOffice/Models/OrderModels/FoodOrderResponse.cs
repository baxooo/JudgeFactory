using JudgeOffice.Models.FoodModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.OrderModels
{
    internal class FoodOrderResponse : Order<Food>
    {
        public FoodOrderResponse()
        {
            Contents = new List<Food>();
        }
    }
}
