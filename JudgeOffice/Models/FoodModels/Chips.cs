using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.FoodModels
{
    internal class Chips : Food
    {
        public Chips()
        {
            Id = 4;
            Name = "Chips";
            Price = 3.00m;
            TimeToPrepareInSeconds = 10;
        }
    }
}
