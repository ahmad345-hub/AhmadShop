
using AhmadShop.DAL.DTO.Requests;
using AhmadShop.DAL.DTO.Response;
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using Mapster;
using System.Linq.Expressions;

namespace AhmadShop.BLL
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepositry<Product> _productRepository;
        private readonly IFileService _fileService;

        public ProductService(
            IGenericRepositry<Product> productRepository,
            IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        // Create Product
        public async Task<ProductResponse> CreateAsync(
            ProductRequest request,
            CancellationToken cancellationToken = default)
        {
            var product = new Product
            {
                Price = request.Price ?? 0,
                Quantity = request.Quantity ?? 0,
                Rate = request.Rate ?? 0,
                Discount = request.Discount ?? 0,
                CategoryId = request.CategoryId ?? 0
            };

            // Upload image
            if (request.MainImage is not null)
            {
                var imageName = await _fileService.UploadAsync(
                    request.MainImage,
                    cancellationToken
                );

                product.MainImag = imageName ?? string.Empty;
            }

            // Add translations
            if (request.Translations is not null)
            {
                foreach (var translation in request.Translations)
                {
                    product.Translations.Add(
                        translation.Adapt<ProductTranslation>()
                    );
                }
            }

            await _productRepository.CreateAsync(
                product,
                cancellationToken
            );

            return product.Adapt<ProductResponse>();
        }

        // Get All Products
        public async Task<List<ProductResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAsync(
                includes: new List<Expression<Func<Product, object>>>
                {
                    p => p.Translations
                },
                cancellationToken: cancellationToken
            );

            return products.Adapt<List<ProductResponse>>();
        }

        // Get Product By Id
        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetOneAsync(
                p => p.Id == id,
                new List<Expression<Func<Product, object>>>
                {
                    p => p.Translations
                }
            );

            return product?.Adapt<ProductResponse>();
        }

        // Update Product
        public async Task<ProductResponse?> UpdateAsync(
            int id,
            ProductRequest request,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetOneAsync(
                p => p.Id == id,
                new List<Expression<Func<Product, object>>>
                {
                    p => p.Translations
                },
                tracked: true,
                cancellationToken: cancellationToken
            );

            if (product is null)
                return null;

            // Update only provided properties
            if (request.Price.HasValue)
                product.Price = request.Price.Value;

            if (request.Quantity.HasValue)
                product.Quantity = request.Quantity.Value;

            if (request.Rate.HasValue)
                product.Rate = request.Rate.Value;

            if (request.Discount.HasValue)
                product.Discount = request.Discount.Value;

            if (request.CategoryId.HasValue)
                product.CategoryId = request.CategoryId.Value;

            // Update image if a new image was uploaded
            if (request.MainImage is not null)
            {
                var newImageName = await _fileService.UploadAsync(
                    request.MainImage,
                    cancellationToken
                );

                if (newImageName is not null)
                {
                    var oldImage = product.MainImag;

                    product.MainImag = newImageName;

                    if (!string.IsNullOrWhiteSpace(oldImage))
                        _fileService.Delete(oldImage);
                }
            }

            // Update translations only if provided
            if (request.Translations is not null)
            {
                product.Translations.Clear();

                foreach (var translationRequest in request.Translations)
                {
                    product.Translations.Add(
                        translationRequest.Adapt<ProductTranslation>()
                    );
                }
            }

            await _productRepository.UpdateAsync(
                product,
                cancellationToken
            );

            return product.Adapt<ProductResponse>();
        }

        // Delete Product
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetOneAsync(
                p => p.Id == id,
                tracked: true
            );

            if (product is null)
                return false;

            if (!string.IsNullOrWhiteSpace(product.MainImag))
                _fileService.Delete(product.MainImag);

            await _productRepository.RemoveAsync(product);

            return true;
        }
    }
}
