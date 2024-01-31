using JudgeOffice.Events;
using JudgeOffice.Models;
using JudgeOffice.Models.FoodModels;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Offices;
using System.Reflection.Metadata;

namespace JudgeOffice
{
    internal class OfficeManager<T>
        where T : ServiceType,new()
    {
        public List<Order<T>> Orders = new List<Order<T>>();
        public Office<T> Office { get; set; }
        public OfficeManager()
        {
            T entity = new();
            Office = entity is Food ? new DeliveryOffice() as Office<T> : new TranslationOffice() as Office<T>;
        }


        public void GetNotification(object sender, NotificationEventArgs<T> e)
        {
            Console.WriteLine($"Manager: {typeof(T).Name} Order has Arrived");
            Console.WriteLine($"Manager: Judge, {typeof(T).Name} Order is on the table");
            Orders.Add(e.OrderReceived);
        }

        public void GetNotificationOrderOnTheWay(object sender, NotificationEventArgs<T> e)
        {
            Console.WriteLine($"Order {e.OrderReceived.Id} is on the way");
        }
    }
}
