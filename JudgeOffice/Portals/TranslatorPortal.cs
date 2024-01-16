using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
using JudgeOffice.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Portals
{
    internal class TranslatorPortal : Portal<Translation>
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

        public override Provider<Translation> CheckServices()
        {
            throw new NotImplementedException();
        }

        public override Task<Order<Translation>> SendOrder(OrderRequest<Translation> order, Provider<Translation> provider)
        {
            throw new NotImplementedException();
        }
    }
}
