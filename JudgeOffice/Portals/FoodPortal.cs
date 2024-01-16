using JudgeOffice.Models;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Portals;

internal class FoodPortal : Portal<Food>
{
    List<FoodProvider> _foodProviders = null;

    private static FoodPortal _instance => null;
    private static readonly object _lock = new object();

    private FoodPortal()
    {
        _foodProviders = new List<FoodProvider>()
        {
            new BreakfastProvider(),
            new BreakfastProvider(),
            new BrunchProvider(),
            new BrunchProvider(),
            new BrunchProvider(),
            new LunchProvider(),
            new DinnerProvider()
        };
    }
    public static FoodPortal Instance
    {
        get
        {
            if (_instance == null)
                lock (_lock)
                    if (_instance == null)
                        return new FoodPortal();

            return _instance;
        }
    }

    public override Provider<Food> CheckServices()
    {
        TimeSpan now = DateTime.Now.TimeOfDay;
        // TODO add a real selection based on the provider with less orders
        FoodProvider? fp = _foodProviders.FirstOrDefault(fp => fp.OpenTime <= now && fp.CloseTime >= now);
        if (fp == null)
            throw new Exception("No food provider available");
        return fp;
    }

    public override async Task<Order<Food>> SendOrder(OrderRequest<Food> order, Provider<Food> provider)
    {
        var foodProvider = (FoodProvider)provider;
        return await foodProvider.ProcessOrderAsync(order);
    }
}
