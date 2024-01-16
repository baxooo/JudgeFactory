using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Portals
{
    internal class TranslatorPortal
    {
        private static TranslatorPortal _instance;
        private static object _lock = new object();
        public TranslatorPortal()
        {
                
        }
        public static TranslatorPortal Instance
        {
            get
            {
                if (_instance == null)
                    lock (_lock)
                        if (_instance == null)
                            return new TranslatorPortal();

                return _instance;
            }
        }
    }
}
