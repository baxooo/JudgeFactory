using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
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
    public static Queue<OrderRequest<Translation>> OrdersQueue { get; set; } = new Queue<OrderRequest<Translation>>();
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

    public async Task<TranslationOrderResponse> AddOrder(OrderRequest<Translation> order)
    {
        OrdersQueue.Enqueue(order);
        return await ProcessNextOrder();
    }

    private async Task<TranslationOrderResponse> ProcessOrderAsync(OrderRequest<Translation> order)
    {
        var cookingTasks = new List<Task>();

        for (int i = 0; i < order.Contents.Count; i++)
        {
            var translation = order.Contents[i];

            cookingTasks.Add(PrepareTranslationAsync(translation));
        }

        await Task.WhenAll(cookingTasks);

        Console.WriteLine("Order processed and ready for delivery.");

        ProcessNextOrder();

        return new TranslationOrderResponse()
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

    private async Task<TranslationOrderResponse> ProcessNextOrder()
    {
        if (OrdersQueue.Count > 0)
        {
            var nextOrder = OrdersQueue.Dequeue();
            return await ProcessOrderAsync(nextOrder);
        }
        else return null;
    }
}
