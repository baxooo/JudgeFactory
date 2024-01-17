using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
using JudgeOffice.Portals;
using JudgeOffice.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Offices
{
    internal class TranslationOffice : Office<Translation>
    {
        public override Provider<Translation> GetServices()
        {
            TranslatorPortal portal = TranslatorPortal.Instance;
            return portal.CheckServices();
        }

        public override async Task<Order<Translation>> SendOrder(OrderRequest<Translation> order, Provider<Translation> provider)
        {
            TranslatorPortal portal = TranslatorPortal.Instance;
            return await portal.SendOrder(order, provider);
        }
    }
}
