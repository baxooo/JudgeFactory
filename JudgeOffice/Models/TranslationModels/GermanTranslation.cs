using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.TranslationModels
{
    internal class GermanTranslation : Translation
    {
        public GermanTranslation()
        {
            Id = 3;
            Name = "German";
            Price = 10.00m;
            TimeToTranslateInSeconds = 10;
        }
    }
}
