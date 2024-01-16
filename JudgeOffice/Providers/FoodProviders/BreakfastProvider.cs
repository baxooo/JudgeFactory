using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Providers.FoodProviders
{
    internal class BreakfastProvider : FoodProvider
    {
        public override TimeSpan OpenTime => new(07, 00, 00);
        public override TimeSpan CloseTime => new(11, 00, 00);

        public BreakfastProvider()
        {
            ListOfAvailableGoods = new List<Food>()
            {
                new Coffee(),
                new Cappuccino(),
                new Croissant()
            };
        }

    }
}
