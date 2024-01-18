using JudgeOffice.Models.FoodModels;

namespace JudgeOffice.Providers.FoodProviders
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
