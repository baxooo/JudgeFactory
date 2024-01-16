using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.FoodModels
{
    internal class Cappuccino : Food
    {
        public Cappuccino()
        {
            Id = 2;
            Name = "Cappuccino";
            Price = 2.00m;
            TimeToPrepareInSeconds = 10; //60 = 1 min
        }
    }
}
