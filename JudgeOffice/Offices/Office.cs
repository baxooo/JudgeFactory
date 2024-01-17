using JudgeOffice.Events;
using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Offices
{
    internal abstract class Office<T>
        where T : ServiceType
    {
        public delegate void OnOrderReceivedEventHandler(object sender, NotificationEventArgs<T> e);
        public event OnOrderReceivedEventHandler OnOrderReceived;
        Order<T> _orderReceived;

        public Order<T> OrderReceived
        {
            get { return _orderReceived; }
            set
            {
                if (_orderReceived != value)
                {
                    NotificationEventArgs<T> e = new NotificationEventArgs<T>(value);
                    OnOrderReceived(this, e);

                    _orderReceived = value;
                }
            }

        }
        public abstract Provider<T> GetServices();
        public abstract Task SendOrder(OrderRequest<T> order, Provider<T> provider);

        internal Task ReceivedOrder(Order<T> order) 
        {
            OrderReceived = order;
            return Task.CompletedTask;
        }
    }
}
