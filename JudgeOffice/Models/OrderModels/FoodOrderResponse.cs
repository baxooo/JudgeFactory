using JudgeOffice.Models.FoodModels;

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
