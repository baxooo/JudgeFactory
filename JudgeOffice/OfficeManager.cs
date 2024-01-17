using JudgeOffice.Events;
using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Offices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice
{
    internal class OfficeManager<T>
        where T : ServiceType
    {
        public List<Order<T>> Orders = new List<Order<T>>();
        public Office<T> Office { get; }
        public OfficeManager(Office<T> office)
        {
            Office = office;
            office.OnOrderReceived += GetNotification;
        }

        public void GetNotification(object sender, NotificationEventArgs<T> e )
        {
            Console.WriteLine("Order has Arrived");
            Orders.Add(e.OrderReceived);
        }
    }
}
