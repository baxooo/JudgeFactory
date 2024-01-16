using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.TranslationModels
{
    internal class Translation : ServiceType
    {
        public int TimeToTranslateInSeconds { get; set; }
        public List<string> TranslatedText { get; set; } = new List<string>();
    }
}
