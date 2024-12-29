namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        // [] is the simplified version of new()
        public List<string> Category { get; set; } = [];

        // default! allow null and tells the compiler to ignore the null warning
        public string Description { get; set; } = default!;

        public string ImageFile { get; set; } = default!;

        public decimal Price { get; set; }
    }
}
