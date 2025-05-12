using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Entities;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public ProductController(OrderDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();  // Ürünleri alıyoruz
            return Ok(products);  // Ürün listesini döndürüyoruz
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);  // Ürünü ID'ye göre buluyoruz
            if (product == null)
            {
                return NotFound();  // Ürün bulunamazsa 404 döneriz
            }
            return Ok(product);  // Ürünü döndürüyoruz
        }

        // POST: api/Product
        [Authorize(Roles = "Admin")]  // 👑 Sadece Admin erişebilir
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _context.Products.Add(product);  // Yeni ürünü ekliyoruz
            _context.SaveChanges();  // Değişiklikleri kaydediyoruz
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);  // 201 (Created) döneriz
        }

        // PUT: api/Product/{id}
        [Authorize(Roles = "Admin")]  // 👑 Sadece Admin erişebilir
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();  // ID uyuşmazsa 400 döneriz
            }

            var existingProduct = _context.Products.Find(id);  // Ürünü ID'ye göre buluyoruz
            if (existingProduct == null)
            {
                return NotFound();  // Ürün bulunamazsa 404 döneriz
            }

            // Ürünü güncelliyoruz
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            _context.Entry(existingProduct).State = EntityState.Modified;  // Durumunu "modified" olarak işaretliyoruz
            _context.SaveChanges();  // Değişiklikleri kaydediyoruz

            return Ok(existingProduct);  // Güncellenmiş ürünü döndürüyoruz
        }

        // DELETE: api/Product/{id}
        [Authorize(Roles = "Admin")]  // 👑 Sadece Admin erişebilir
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);  // Ürünü ID'ye göre buluyoruz
            if (product == null)
            {
                return NotFound();  // Ürün bulunamazsa 404 döneriz
            }

            _context.Products.Remove(product);  // Ürünü siliyoruz
            _context.SaveChanges();  // Değişiklikleri kaydediyoruz

            return NoContent();  // 204 (No Content) döneriz, çünkü veri silindi
        }
    }
}
