using CleanArchitecture.Domain.AggregatesModels.Categories;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Products;

public sealed class Product : BaseEntityRoot
{
    private Product(
        string name,
        string? description,
        decimal price,
        string? imagePath,
        Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        ImagePath = imagePath;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public string? ImagePath { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; }

    public static Product Create(
        string name,
        string? description,
        decimal price,
        string? imagePath,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(price));

        if (categoryId == Guid.Empty)
             throw new ArgumentException("Category ID is required", nameof(categoryId));

        var product = new Product(
            name,
            description,
            price,
            imagePath,
            categoryId);

        return product;
    }

    public void UpdateDetails(string name, string? description, string? imagePath, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));
            
        if (categoryId == Guid.Empty)
             throw new ArgumentException("Category ID is required", nameof(categoryId));

        Name = name;
        Description = description;
        ImagePath = imagePath;
        CategoryId = categoryId;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative", nameof(newPrice));
            
        Price = newPrice;
    }
}
