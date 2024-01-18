namespace JudgeOffice.Models.FoodModels
{
    internal class Burger : Food
    {
        public Burger()
        {
            Id = 1;
            Name = "Burger";
            Price = 7.00m;
            TimeToPrepareInSeconds = 10; //480 = 8 minuti
        }
    }
}
