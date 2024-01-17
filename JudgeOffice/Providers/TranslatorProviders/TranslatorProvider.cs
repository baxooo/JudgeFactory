using JudgeOffice.Delivery;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
using JudgeOffice.Offices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Providers.TranslatorProviders;

internal class TranslatorProvider : Provider<Translation>
{
    public TimeSpan OpenTime { get; } = new TimeSpan(8, 0, 0);
    public TimeSpan CloseTime { get; } = new TimeSpan(20, 0, 0);
    public override List<Translation> ListOfAvailableGoods { get; set; }
    public static Queue<OrderRequest<Translation>> OrdersQueue { get; set; } = new();
    public TranslatorProvider()
    {
        ListOfAvailableGoods = new List<Translation>()
        {
            new EnglishTranslation(),
            new SpanishTranslation(),
            new GermanTranslation(),
            new FrenchTranslation()
        };
    }

    public async Task AddOrder(OrderRequest<Translation> order)
    {
        order.Id = new Random().Next(0, 1000);
        order.TotalPrice = order.Contents.Sum(x => x.Price);
        order.State = Enums.StateEnum.Received;
        OrdersQueue.Enqueue(order);
        await ProcessNextOrder();
    }

    private async Task ProcessOrderAsync(OrderRequest<Translation> order)// TODO sistema delivery per tornare indietro l'ordine
    {
        var cookingTasks = new List<Task>();
        order.State = Enums.StateEnum.Processing;
        for (int i = 0; i < order.Contents.Count; i++)
        {
            var translation = order.Contents[i];

            cookingTasks.Add(PrepareTranslationAsync(translation));
        }

        await Task.WhenAll(cookingTasks);

        Console.WriteLine("Order processed and ready for delivery.");

        //TODO delivery guy take order in charge and change order state to OnTheGo, notify the portal of the new state

        Porter porter = new Porter();
        await porter.TransportOrder(order,order.OfficeRequester);
        await ProcessNextOrder();

        /*send*/ new TranslationOrderResponse()
        {
            Id = order.Id,
            Contents = order.Contents,
            TotalPrice = order.Contents.Sum(x => x.Price),
        };
    }

    private static async Task PrepareTranslationAsync(Translation translation)
    {
        Console.WriteLine($"Preparing {translation.Name} translation...");
        await Task.Delay(TimeSpan.FromSeconds(translation.TimeToTranslateInSeconds));
        Console.WriteLine($"Finished preparing {translation.Name} translation");
    }

    private async Task ProcessNextOrder()
    {
        if (OrdersQueue.Count > 0)
        {
            var nextOrder = OrdersQueue.Dequeue();
            await ProcessOrderAsync(nextOrder);
        }
    }
}
