using CleanArchitecture.Application.Common.ApplicationServices.FileStorage;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Domain.AggregatesModels.Categories;
using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.Common;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileStorageService _fileStorageService;
    public CreateProductCommandHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound);
        }

        string? imagePath = request.Image is not null ?
            await _fileStorageService.UploadAsync<Product>(
                request.Image,
                FileType.Image, 
                cancellationToken)
            : null;

        var product = Product.Create(
            request.Name,
            request.Description,
            request.Price,
            imagePath,
            request.CategoryId);

        var result = await _productRepository.AddAsync(product);

        return result.Id;
    }
}
