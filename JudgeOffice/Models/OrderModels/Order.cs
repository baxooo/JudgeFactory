using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.OrderModels
{
    internal abstract class Order<T>
    {
        public int Id { get; set; }
        public List<T> Contents { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
