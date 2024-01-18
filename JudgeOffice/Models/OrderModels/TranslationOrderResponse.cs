using JudgeOffice.Models.TranslationModels;

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
