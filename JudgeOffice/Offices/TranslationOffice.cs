using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
using JudgeOffice.Portals;
using JudgeOffice.Providers;

namespace JudgeOffice.Offices
{
    internal class TranslationOffice : Office<Translation>
    {
        public override Provider<Translation> GetServices()
        {
            TranslatorPortal portal = TranslatorPortal.Instance;
            return portal.CheckServices();
        }

        public override async Task SendOrder(OrderRequest<Translation> order, Provider<Translation> provider) =>
             await TranslatorPortal.Instance.SendOrder(order, provider);

    }
}
