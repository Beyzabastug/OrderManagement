namespace OrderManagement.Infrastructure.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Burada Description özelliğini ekliyoruz
        public string Description { get; set; }
    }
}
