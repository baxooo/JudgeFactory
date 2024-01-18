using JudgeOffice.Models;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
using JudgeOffice.Offices;
using JudgeOffice.Providers;

namespace JudgeOffice;

internal class Program
{
    static void Main(string[] args)
    {
        while (ValidInput()) ;
    }

    private static bool ValidInput()
    {
        Console.Clear();
        Console.WriteLine("1 - Delivery\n2 - Translation");

        var key = Console.ReadKey().KeyChar;
        switch (key)
        {
            case '1'://Delivery
                DeliveryOffice foodOffice = new DeliveryOffice();
                OfficeManager<Food> foodManager = new OfficeManager<Food>(foodOffice);
                GetProviders(foodManager);
                return true;
            case '2'://Translation
                TranslationOffice translationOffice = new TranslationOffice();
                OfficeManager<Translation> TranslationManager = new OfficeManager<Translation>(translationOffice);
                GetProviders(TranslationManager);
                return true;
            default:
                Console.WriteLine("please, select 1 or 2");
                return true;
        }
    }

    private static async void GetProviders<T>(OfficeManager<T> manager)
        where T : ServiceType
    {
        Console.Clear();
        var order = new OrderRequest<T>(manager.Office);
        bool validInput = false;
        var provider = manager.Office.GetServices();
        Console.WriteLine("available services at " + DateTime.Now.ToString("HH:mm"));
        for (int i = 0; i < provider.ListOfAvailableGoods.Count; i++)
        {
            Console.WriteLine((i + 1) + " "
                + provider.ListOfAvailableGoods[i].Name.PadRight(20) +
                 provider.ListOfAvailableGoods[i].Price + "$"
                );
        }

        Console.WriteLine("------------------------\nBasket:");
        int currentCursorTop = Console.CursorTop;
        while (!validInput)
        {
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case var c when char.IsDigit(key):

                    int n = int.Parse(key.ToString());
                    if (n > provider.ListOfAvailableGoods.Count)
                        break;

                    for (int i = 1; i < provider.ListOfAvailableGoods.Count + 1; i++)
                        if (i == n)
                        {
                            order.Contents.Add(provider.ListOfAvailableGoods[n - 1]);
                            StampaContenutoBasket(order, currentCursorTop);
                            break;
                        }
                    break;
                case 'o':
                    await ConfirmOrder(manager, order, provider);
                    validInput = true;
                    break;
            }
        }
    }

    private static void StampaContenutoBasket<T>(OrderRequest<T> order, int currentCursorTop) where T : ServiceType
    {
        Dictionary<ServiceType, int> keyValuePairs = new();
        foreach (var item in order.Contents)
        {
            if (keyValuePairs.ContainsKey(item))
                keyValuePairs[item]++;
            else
                keyValuePairs.Add(item, 1);
        }

        RimuoviUltimeNLinee(keyValuePairs.Count, currentCursorTop);

        var values = keyValuePairs.OrderByDescending(x => x.Key.Price * x.Value);
        decimal total = 0m;
        foreach (var item in values)
        {
            var itemTotal = item.Key.Price * item.Value;
            Console.WriteLine((item.Key.Name + " x" + item.Value).PadRight(26 - itemTotal.ToString().Length) + itemTotal + "$");
            total += item.Key.Price * item.Value;
        }
        Console.WriteLine("Total".PadRight(26 - total.ToString().Length) + total + "$");
    }

    static void RimuoviUltimeNLinee(int n, int curretCursorTop)
    {
        for (int i = 0; i < n + 1; i++)
        {
            if (curretCursorTop != Console.CursorTop)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            else
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
                break;
            }
        }
    }

    private static async Task<bool> ConfirmOrder<T>(OfficeManager<T> manager, OrderRequest<T> order, Provider<T> provider)
        where T : ServiceType
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.WriteLine("confirmed");
        await manager.Office.SendOrder(order, provider);
        return true;
    }
}
