namespace JudgeOffice.Models.TranslationModels
{
    internal class EnglishTranslation : Translation
    {
        public EnglishTranslation()
        {
            Id = 1;
            Name = "English";
            Price = 10.00m;
            TimeToTranslateInSeconds = 10;
        }
    }
}
