using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Portals;
using JudgeOffice.Providers;

namespace JudgeOffice.Offices
{
    internal class DeliveryOffice : Office<Food>
    {
        public override Provider<Food> GetServices()
        {
            FoodPortal portal = FoodPortal.Instance;
            return portal.CheckServices();
        }

        public override Task SendOrder(OrderRequest<Food> order, Provider<Food> provider) =>
            FoodPortal.Instance.SendOrder(order, provider);

    }
}
