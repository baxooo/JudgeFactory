using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;

namespace JudgeOffice.Events;

internal class NotificationEventArgs<T> : EventArgs
    where T : ServiceType
{
    Order<T> _order;
    public Order<T> OrderReceived { get { return _order; } }

    public NotificationEventArgs(Order<T> order)
    {
        _order = order;
    }
}
