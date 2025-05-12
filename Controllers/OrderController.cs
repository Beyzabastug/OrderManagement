using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Entities;
using System.Security.Claims;
using System;
using System.Linq;

namespace OrderManagement.Api.Controllers
{
    [Authorize]  // 🔐 Giriş yapmış herkes erişebilir
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            model.UserId = userId;
            model.OrderDate = DateTime.UtcNow;

            _context.Orders.Add(model);
            _context.SaveChanges();

            return Ok("Sipariş başarıyla oluşturuldu.");
        }

        [HttpGet]
        public IActionResult GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var orders = _context.Orders.Where(o => o.UserId == userId).ToList();
            return Ok(orders);
        }
    }
}
