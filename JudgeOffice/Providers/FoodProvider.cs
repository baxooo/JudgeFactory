using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Providers;

internal abstract class FoodProvider : Provider<Food>
{
    public override List<Food> ListOfAvailableGoods { get; set; }
    public abstract TimeSpan OpenTime { get; }
    public abstract TimeSpan CloseTime { get; }
    public static Queue<OrderRequest<Food>> OrdersQueue { get; set; } = new Queue<OrderRequest<Food>>();

    private static Dictionary<int, TaskCompletionSource<bool>> _spotOccupancy = new Dictionary<int, TaskCompletionSource<bool>>();

    public async void AddOrder(OrderRequest<Food> order)
    {
        OrdersQueue.Enqueue(order);
    }
    private async Task<FoodOrderResponse> ProcessOrderAsync(OrderRequest<Food> order)
    {
        var cookingTasks = new List<Task>();

        for (int i = 0; i < order.Contents.Count; i++)
        {
            var food = order.Contents[i];
            var spot = GetNextAvailableSpot().Result;

            cookingTasks.Add(PrepareFoodAsync(food, spot));

            _spotOccupancy[spot] = new TaskCompletionSource<bool>();
        }

        await Task.WhenAll(cookingTasks);

        Console.WriteLine("Order processed and ready for delivery.");

        ProcessNextOrder();

        return new FoodOrderResponse()
        {
            Id = order.Id,
            Contents = order.Contents,
            TotalPrice = order.Contents.Sum(x => x.Price),
        };
    }

    private static async Task PrepareFoodAsync(Food food, int spot)
    {
        Console.WriteLine($"Cooking {food.Name} at spot {spot}...");
        await Task.Delay(TimeSpan.FromSeconds(food.TimeToPrepareInSeconds)); 
        Console.WriteLine($"Finished cooking {food.Name} at spot {spot}.");
        _spotOccupancy[spot].SetResult(true);
    }

    private static async Task<int>  GetNextAvailableSpot()
    {
        int spot = 0;

        while (_spotOccupancy.ContainsKey(spot))
        {
            var spotAvailableTask = _spotOccupancy[spot].Task;
            await Task.WhenAny(spotAvailableTask, Task.Delay(100));

            if (spotAvailableTask.IsCompleted)
                _spotOccupancy.Remove(spot);
            else
            {
                spot++;
                spot %= 4;
            }
        }

        return spot;
    }

    private async void ProcessNextOrder()
    {
        if (OrdersQueue.Count > 0)
        {
            var nextOrder = OrdersQueue.Dequeue();
            await ProcessOrderAsync(nextOrder); 
        }
    }
}
