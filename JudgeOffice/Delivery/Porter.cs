using JudgeOffice.Enums;
using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Offices;

namespace JudgeOffice.Delivery;

internal class Porter : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task TransportOrder<T>(Order<T> order, Office<T> to)
    where T : ServiceType
    {
        order.State = StateEnum.OnTheGo;
        await Console.Out.WriteLineAsync($"Transporter on the way with order {order.Id}");
        await Task.Delay(TimeSpan.FromSeconds(new Random().Next(6, 12)));
        await to.ReceivedOrder(order);
        order.State = StateEnum.Delivered;
        Dispose();
    }
}
