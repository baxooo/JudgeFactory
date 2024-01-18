using JudgeOffice.Events;
using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;

namespace JudgeOffice.Providers
{
    internal abstract class Provider<T>
        where T : ServiceType
    {
        public string Name { get; set; }
        public abstract List<T> ListOfAvailableGoods { get; set; }

        public delegate void OnOrderCompletedEventHandler(object sender, NotificationEventArgs<T> e);
        public event OnOrderCompletedEventHandler OnOrderCompleted;

        Order<T> _orderCompleted;

        public Order<T> OrderCompleted
        {
            get { return _orderCompleted; }
            set
            {
                if (_orderCompleted != value)
                {
                    NotificationEventArgs<T> e = new NotificationEventArgs<T>(value);
                    OnOrderCompleted(this, e);

                    _orderCompleted = value;
                }
            }
        }

        protected void SetOrderCompleted(Order<T> order)
        {
            OrderCompleted = order;
        }
    }
}