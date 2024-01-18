using JudgeOffice.Models.FoodModels;

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
