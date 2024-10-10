namespace MVCmodel.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
