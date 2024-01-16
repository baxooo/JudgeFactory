using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Providers
{
    internal class DinnerProvider : FoodProvider
    {
        public override TimeSpan OpenTime => new TimeSpan(16, 0, 0);

        public override TimeSpan CloseTime => new TimeSpan(22, 0, 0);

        public DinnerProvider()
        {
            ListOfAvailableGoods = new List<Food>()
            {
                new Burger(),
                new Chips(),
                new Nuggets() ,
            };
        }
    }
}
