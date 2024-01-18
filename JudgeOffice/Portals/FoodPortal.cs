using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Providers;
using JudgeOffice.Providers.FoodProviders;

namespace JudgeOffice.Portals;

internal class FoodPortal : Portal<Food>
{
    readonly List<FoodProvider> _foodProviders = null;
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
        return fp ?? throw new Exception("No food provider available");
    }

    public override async Task SendOrder(OrderRequest<Food> order, Provider<Food> provider)
    {
        var foodProvider = (FoodProvider)provider;
        await foodProvider.AddOrder(order);
    }
}
