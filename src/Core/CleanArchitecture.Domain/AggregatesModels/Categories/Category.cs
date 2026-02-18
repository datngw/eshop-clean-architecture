using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Categories;

public sealed class Category : BaseEntityRoot
{
    private Category(string name)
    {
        Name = name;
    }

    public string Name { get; private set; } = string.Empty;

    public static Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Category category = new Category(name);
        return category;
    }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));
            
        Name = name;
    }
}
