using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Portals;
using JudgeOffice.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JudgeOffice.Offices
{
    internal class DeliveryOffice : Office<Food>
    {
        public override Provider<Food> GetServices()
        {
            FoodPortal portal = FoodPortal.Instance;
            return portal.CheckServices();
        }

        public async override Task<Order<Food>> SendOrder(OrderRequest<Food> order, Provider<Food> provider)
        {
            FoodPortal portal = FoodPortal.Instance;
            return await portal.SendOrder(order, provider);
        }
    }
}
