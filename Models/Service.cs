namespace MVCmodel.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Price { get; set; }
    }
}
