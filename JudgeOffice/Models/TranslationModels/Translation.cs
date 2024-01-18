namespace JudgeOffice.Models.TranslationModels
{
    internal class Translation : ServiceType
    {
        public int TimeToTranslateInSeconds { get; set; }
        public List<string> TranslatedText { get; set; } = new List<string>();
    }
}
