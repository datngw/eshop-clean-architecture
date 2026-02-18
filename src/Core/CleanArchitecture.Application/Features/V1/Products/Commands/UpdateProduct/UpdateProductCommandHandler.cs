using CleanArchitecture.Application.Common.ApplicationServices.FileStorage;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Domain.AggregatesModels.Categories;
using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateProductCommandHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _fileStorageService = fileStorageService ?? throw new ArgumentException(nameof(fileStorageService));
    }

    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.GetByIdAsync(request.Id);
        if(product == null)
            return Result.Failure<Guid>(ProductErrors.NotFound);

        if (request.CategoryId.HasValue && request.CategoryId.Value != Guid.Empty && request.CategoryId != product.CategoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value, cancellationToken);
            if (category is null)
                return Result.Failure<Guid>(CategoryErrors.NotFound);
        }

        string? imagePath = request.Image is not null ?
            await _fileStorageService.UploadAsync<Product>(
                request.Image,
                FileType.Image, 
                cancellationToken)
            : product.ImagePath;

        // Use new Domain Methods
        // If price is updated
        if (request.Price.HasValue)
        {
            product.UpdatePrice(request.Price.Value);
        }

        // Update details (Name, Description, Image, Category)
        // Since we don't have separate methods for each field yet other than UpdateDetails which takes all,
        // we might need to pass current values if request values are null.
        // Product.UpdateDetails(string name, string? description, string? imagePath, Guid categoryId)
        
        string name = request.Name ?? product.Name;
        string? description = request.Description ?? product.Description;
        Guid categoryId = request.CategoryId ?? product.CategoryId;

        product.UpdateDetails(name, description, imagePath, categoryId);

        await _productRepository.UpdateAsync(product, cancellationToken);

        return product.Id;
    }
}
