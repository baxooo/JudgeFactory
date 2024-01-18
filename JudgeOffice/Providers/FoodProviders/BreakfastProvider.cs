using JudgeOffice.Models.FoodModels;

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
