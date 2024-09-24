namespace Demo.Products.Models
{
    public partial class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
