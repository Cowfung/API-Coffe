using WebApp.Infrastructure.enumExtension;

namespace WebApp.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string ImagePath { get; set; }
        public string Key { get; set; }

        public ProductStatus Status { get; set; } = 0;   // ← Duy nhất
        public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
        public List<ProductImage> ProductImages { get; set; } = new();

    }

}
