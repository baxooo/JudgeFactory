using JudgeOffice.Enums;
using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Offices;

namespace JudgeOffice.Delivery;

internal class Porter
{
    public async Task TransportOrder<T> (Order<T> order, Office<T> to)
        where T : ServiceType
    {
        order.State = StateEnum.OnTheGo;
        await Task.Delay(TimeSpan.FromSeconds(new Random().Next(6, 12)));
        order.State = StateEnum.Delivered;
        to.ReceivedOrder(order);
    }
}
