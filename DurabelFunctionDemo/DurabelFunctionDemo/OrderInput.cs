namespace DurabelFunctionDemo
{
    public class OrderInput
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
