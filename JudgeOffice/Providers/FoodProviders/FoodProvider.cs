using JudgeOffice.Delivery;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;

namespace JudgeOffice.Providers.FoodProviders;

internal abstract class FoodProvider : Provider<Food>
{
    public override List<Food> ListOfAvailableGoods { get; set; }
    public abstract TimeSpan OpenTime { get; }
    public abstract TimeSpan CloseTime { get; }
    private static Queue<Food> _foodInLine { get; set; } = new();
    public static Queue<OrderRequest<Food>> OrdersQueue { get; set; } = new();

    private static Dictionary<int, TaskCompletionSource<bool>> _spotOccupancy = new();
    private static object _lock = new();
    static bool _isProcessing = false;
    static bool _startedNew = false;

    public async Task AddOrder(OrderRequest<Food> order)
    {
        order.Id = new Random().Next(0, 1000);
        order.TotalPrice = order.Contents.Sum(x => x.Price);
        OrdersQueue.Enqueue(order);

        await ProcessOrdersSequentially(order.Contents);
    }
    private async Task ProcessOrdersSequentially(List<Food> foods)
    {
        while (OrdersQueue.Count > 0)
        {
            if (!_isProcessing)
            {
                foods.ForEach(x => _foodInLine.Enqueue(x));  
                _isProcessing = true;
                await ProcessOrderAsync();
            }
            else await Task.Delay(100);
        }
    }
    private async Task ProcessOrderAsync()
    {
        var order = OrdersQueue.Dequeue();
        var cookingTasks = new List<Task>();
        Console.BackgroundColor = ConsoleColor.Blue;
        await Console.Out.WriteLineAsync($"processing order {order.Id}");
        Console.ResetColor();
        for (int i = 0; i < order.Contents.Count; i++)
        {
            var food = order.Contents[i];
            int spot = await GetNextAvailableSpot();

            cookingTasks.Add(PrepareFoodAsync(food, spot));

            _spotOccupancy[spot] = new TaskCompletionSource<bool>();
        }

        await Task.WhenAll(cookingTasks);
        _startedNew = false;
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine("Order processed and ready for delivery.");
        Console.ResetColor();
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
        _foodInLine.Dequeue();

        if (_foodInLine.Count <= 3 && !_startedNew)
        {
            lock (_lock)
                if (_foodInLine.Count <= 3 && !_startedNew)
                {
                    _isProcessing = false;
                    _startedNew = true;
                }
        }
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
