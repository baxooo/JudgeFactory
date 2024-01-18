namespace JudgeOffice.Models.FoodModels
{
    internal abstract class Food : ServiceType
    {
        public int TimeToPrepareInSeconds { get; set; }
    }
}
