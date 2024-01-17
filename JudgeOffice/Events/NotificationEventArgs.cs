using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
