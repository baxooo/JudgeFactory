using JudgeOffice.Delivery;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;

namespace JudgeOffice.Providers.FoodProviders;

internal abstract class FoodProvider : Provider<Food>
{
    public override List<Food> ListOfAvailableGoods { get; set; }
    public abstract TimeSpan OpenTime { get; }
    public abstract TimeSpan CloseTime { get; }
    public static Queue<OrderRequest<Food>> OrdersQueue { get; set; } = new Queue<OrderRequest<Food>>();

    private static Dictionary<int, TaskCompletionSource<bool>> _spotOccupancy = new Dictionary<int, TaskCompletionSource<bool>>();
    static bool IsProcessing = false;

    public async Task AddOrder(OrderRequest<Food> order)
    {
        order.Id = new Random().Next(0, 1000);
        order.TotalPrice = order.Contents.Sum(x => x.Price);
        OrdersQueue.Enqueue(order);

        await ProcessOrdersSequentially();
    }
    private async Task ProcessOrdersSequentially()
    {
        while (OrdersQueue.Count > 0)
        {
            if (!IsProcessing)
            {
                IsProcessing = true;
                await ProcessOrderAsync();
            }
            else await Task.Delay(100);
        }
    }
    private async Task ProcessOrderAsync()
    {
        var order = OrdersQueue.Peek();
        var cookingTasks = new List<Task>();
        await Console.Out.WriteLineAsync($"processing order {order.Id}");
        for (int i = 0; i < order.Contents.Count; i++)
        {
            var food = order.Contents[i];
            int spot = await GetNextAvailableSpot();

            cookingTasks.Add(PrepareFoodAsync(food, spot));

            _spotOccupancy[spot] = new TaskCompletionSource<bool>();
        }

        await Task.WhenAll(cookingTasks);
        IsProcessing = false;
        OrdersQueue.Dequeue();
        Console.WriteLine("Order processed and ready for delivery.");
        SetOrderCompleted(order);

        Porter porter = new Porter();
        await porter.TransportOrder(order, order.OfficeRequester);
    }

    private static async Task PrepareFoodAsync(Food food, int spot)
    {
        Console.WriteLine($"Preparing {food.Name} at spot {spot}...");
        await Task.Delay(TimeSpan.FromSeconds(food.TimeToPrepareInSeconds));
        Console.WriteLine($"Finished preparing {food.Name} at spot {spot}.");
        _spotOccupancy[spot].SetResult(true);
    }

    private static async Task<int> GetNextAvailableSpot()
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
}
