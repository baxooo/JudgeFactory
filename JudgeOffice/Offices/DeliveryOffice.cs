﻿using JudgeOffice.Models.FoodModels;
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

        public override FoodOrderResponse SendOrder(OrderRequest<Food> order, Provider<Food> provider)
        {
            FoodPortal portal = FoodPortal.Instance;
            return (FoodOrderResponse)portal.SendOrder(order, provider).Result;
        }
    }
}
