using JudgeOffice.Offices;

namespace JudgeOffice.Models.OrderModels
{
    internal class OrderRequest<T> : Order<T>
        where T : ServiceType
    {
        public readonly Office<T> OfficeRequester;
        public OrderRequest(Office<T> office)
        {
            Contents = new List<T>();
            OfficeRequester = office;
        }
    }
}
