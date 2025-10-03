using System;
using System.Collections.Generic;

namespace OrderService.DTOs
{
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
    }

    public class OrderItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
