namespace JudgeOffice.Models
{
    internal abstract class ServiceType
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}