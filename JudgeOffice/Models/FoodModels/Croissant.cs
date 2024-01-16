using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.FoodModels
{
    internal class Croissant : Food
    {
        public Croissant()
        {
            Name = "Croissant";
            Price = 2.00m;
            Id = 3;
            TimeToPrepareInSeconds = 10;
        }
    }
}
