using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Providers
{
    internal class LunchProvider : FoodProvider
    {
        public override TimeSpan OpenTime => new TimeSpan(12, 00, 00);

        public override TimeSpan CloseTime => new TimeSpan(16, 00, 00);
        public LunchProvider()
        {
            ListOfAvailableGoods = new List<Food>()
            {
                new Burger(),
                new Chips(),
                new Nuggets() ,
                new Coffee()
            };
        }
    }
}
