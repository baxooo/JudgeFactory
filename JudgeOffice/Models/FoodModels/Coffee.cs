namespace JudgeOffice.Models.FoodModels
{
    internal class Coffee : Food
    {
        public Coffee()
        {
            Id = 5;
            Name = "Coffee";
            Price = 1.00m;
            TimeToPrepareInSeconds = 10;
        }
    }
}
