using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Providers.FoodProviders
{
    internal class BrunchProvider : FoodProvider
    {
        public override TimeSpan OpenTime => new(08, 00, 00);
        public override TimeSpan CloseTime => new(14, 00, 00);
        public BrunchProvider()
        {
            ListOfAvailableGoods = new List<Food>()
            {
                new Burger(),
                new Chips(),
                new Nuggets() ,
                new Coffee(),
                new Cappuccino(),
                new Croissant()
            };
        }

    }
}
