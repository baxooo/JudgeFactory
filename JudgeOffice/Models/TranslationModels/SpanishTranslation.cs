using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.TranslationModels
{
    internal class SpanishTranslation : Translation
    {
        public SpanishTranslation()
        {
            Id = 4;
            Name = "Spanish";
            Price = 10.00m;
            TimeToTranslateInSeconds = 10;
        }
    }
}
