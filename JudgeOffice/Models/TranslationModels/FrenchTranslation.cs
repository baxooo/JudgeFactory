using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.TranslationModels
{
    internal class FrenchTranslation : Translation
    {
        public FrenchTranslation()
        {
            Id = 2;
            Name = "French";
            Price = 12.00m;
            TimeToTranslateInSeconds = 10;
        }
    }
}
