﻿using JudgeOffice.Models.TranslationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Models.OrderModels
{
    internal class TranslationOrderResponse : Order<Translation> 
    {
        public TranslationOrderResponse()
        {
            Contents = new List<Translation>();
        }
    }
}
