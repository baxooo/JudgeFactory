using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
using JudgeOffice.Providers;
using JudgeOffice.Providers.TranslatorProviders;

namespace JudgeOffice.Portals;

internal class TranslatorPortal : Portal<Translation>
{
    TranslatorProvider _tp = new TranslatorProvider();
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

    public override Provider<Translation> CheckServices() => _tp;

    public override async Task SendOrder(OrderRequest<Translation> order, Provider<Translation> provider)
    {
        var translatorProvider = (TranslatorProvider)provider;
        await translatorProvider.AddOrder(order);
    }
}
