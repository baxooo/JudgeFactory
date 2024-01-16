using JudgeOffice.Models;

namespace JudgeOffice.Providers
{
    internal abstract class Provider<T>
        where T : ServiceType
    {
        public string Name { get; set; }
        public abstract List<T> ListOfAvailableGoods { get; set; }

    }
}