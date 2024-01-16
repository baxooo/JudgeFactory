
using JudgeOffice.Models;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Offices;
using JudgeOffice.Providers;
using System.Reflection;
using System.Transactions;

namespace JudgeOffice;

internal class Program
{
    static void Main(string[] args)
    {
        

        while (ValidInput());

    }

    private static bool ValidInput()
    {
        Console.Clear();
        Console.WriteLine("1 - Delivery\n2 - Translation");
        var key = Console.ReadKey().KeyChar;

        switch (key)
        {
            case '1'://Delivery
                DeliveryOffice office = new DeliveryOffice();
                OfficeManager<Food> manager = new OfficeManager<Food>(office);
                Console.Clear();
                GetProviders(manager);
                return true;
            case '2'://Translation'
                //TranslationOffice office = new TranslationOffice();
                //OfficeManager<Translation> manager = new OfficeManager<Translation>(office);
                return true;
            default:
                Console.WriteLine("please, select 1 or 2");
                return true;
        }
    }

    private static async void GetProviders<T> (OfficeManager<T> manager)
        where T : ServiceType
    {
        var order = new OrderRequest<T>();
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
                    {
                        if (i == n)
                        {
                            order.Contents.Add(provider.ListOfAvailableGoods[n - 1]);
                            StampaContenutoBasket(order, currentCursorTop);
                            break;
                        }
                    }
                    break;
                case 'o':
                    await ConfirmOrder(manager,order,provider);
                    validInput = true;
                    break;
            }
        }
    }

    private static void StampaContenutoBasket<T>(OrderRequest<T> order, int currentCursorTop) where T : ServiceType
    {
        Dictionary<ServiceType,int> keyValuePairs = new Dictionary<ServiceType,int>();
        foreach (var item in order.Contents)
        {
            if (keyValuePairs.ContainsKey(item))
                keyValuePairs[item]++;
            else
                keyValuePairs.Add(item, 1);
        }

        RimuoviUltimeNLinee(keyValuePairs.Count, currentCursorTop);

        var values = keyValuePairs.OrderByDescending(x => x.Key.Price * x.Value);
        foreach (var item in values)
        {
            Console.WriteLine((item.Key.Name + " x" + item.Value).PadRight(20) + item.Key.Price * item.Value +"$");
        }
    }

    static void RimuoviUltimeNLinee(int n, int curretCursorTop)
    {
        for (int i = 0; i < n; i++)
        {
            if(curretCursorTop != Console.CursorTop)
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
            }
        }
    }

    private static async Task<bool> ConfirmOrder<T>(OfficeManager<T> manager, OrderRequest<T> order, Provider<T> provider)
        where T : ServiceType
    {
        Console.WriteLine("confirmed");
        await manager.Office.SendOrder(order, provider);
        return true;
    }
}
