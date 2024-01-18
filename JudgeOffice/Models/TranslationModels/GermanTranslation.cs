namespace JudgeOffice.Models.TranslationModels
{
    internal class GermanTranslation : Translation
    {
        public GermanTranslation()
        {
            Id = 3;
            Name = "German";
            Price = 10.00m;
            TimeToTranslateInSeconds = 10;
        }
    }
}
