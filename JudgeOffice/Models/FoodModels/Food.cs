using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.FoodModels
{
    internal abstract class Food : ServiceType
    {
        public int TimeToPrepareInSeconds { get; set; }
    }
}
