using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.DTOs;
using OrderService.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext _context;

        public OrdersController(OrdersDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                return BadRequest("O pedido precisa ter ao menos 1 item.");

            var order = new Order
            {
                UserId = request.UserId,
                TotalAmount = request.Items.Sum(i => i.UnitPrice * i.Quantity),
                Items = request.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrders(Guid userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return NotFound("Pedido não encontrado");

            order.Status = status;
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}
