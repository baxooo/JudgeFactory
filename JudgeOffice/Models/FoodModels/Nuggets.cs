using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.FoodModels
{
    internal class Nuggets : Food
    {
        public Nuggets()
        {
            Id = 6;
            Name = "Nuggets";
            Price = 5.00m;
            TimeToPrepareInSeconds = 10;
        }
    }
}
