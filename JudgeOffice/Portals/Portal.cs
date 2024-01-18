using JudgeOffice.Models;
using JudgeOffice.Models.OrderModels;
using JudgeOffice.Providers;

namespace JudgeOffice.Portals
{
    internal abstract class Portal<T>
        where T : ServiceType
    {
        protected static TranslatorPortal _instance;
        protected static object _lock = new object();
        public abstract Provider<T> CheckServices();
        public abstract Task SendOrder(OrderRequest<T> order, Provider<T> provider);
        

    }
}
