using System;

namespace OrderManagement.Infrastructure.Entities
{
    public class Order
    {
        public int Id { get; set; }                   // Sipariş ID
        public int ProductId { get; set; }            // Hangi ürün
        public int Quantity { get; set; }             // Miktar
        public DateTime OrderDate { get; set; }       // Sipariş Tarihi
        public string UserId { get; set; }  // ← Bu çok önemli

        public Product Product { get; set; }          // Ürün bilgisi
    }
}
