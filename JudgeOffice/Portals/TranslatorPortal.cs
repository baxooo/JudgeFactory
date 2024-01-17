using JudgeOffice.Models.OrderModels;
using JudgeOffice.Models.TranslationModels;
using JudgeOffice.Providers;
using JudgeOffice.Providers.TranslatorProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeOffice.Portals;

internal class TranslatorPortal : Portal<Translation>
{
    TranslatorProvider _tp = new TranslatorProvider();
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
        return _tp;
    }

    public override async Task<Order<Translation>> SendOrder(OrderRequest<Translation> order, Provider<Translation> provider)
    {
        var translatorProvider = (TranslatorProvider)provider;
        return await translatorProvider.AddOrder(order);
    }
}
