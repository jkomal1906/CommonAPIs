using CommonAPIs.DTOs;
using CommonAPIs.Models;
using CommonAPIs.Repository;
using CommonAPIs.Services;

namespace CommonAPIs.IServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var product = MapToEntity(productDto);
            product.CreatedDate = DateTime.UtcNow;
            product.UpdatedDate = DateTime.UtcNow;

            var created = await _repository.AddAsync(product);
            return MapToDto(created);
        }

        public async Task<ProductDto?> UpdateAsync(int id, ProductDto productDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = productDto.Name;
            existing.Description = productDto.Description;
            existing.Category = productDto.Category;
            existing.Brand = productDto.Brand;
            existing.Price = productDto.Price;
            existing.DiscountPrice = productDto.DiscountPrice;
            existing.Stock = productDto.Stock;
            existing.SKU = productDto.SKU;
            existing.Barcode = productDto.Barcode;
            existing.Weight = productDto.Weight;
            existing.Height = productDto.Height;
            existing.Width = productDto.Width;
            existing.Length = productDto.Length;
            existing.Color = productDto.Color;
            existing.Material = productDto.Material;
            existing.ManufactureDate = productDto.ManufactureDate;
            existing.ExpiryDate = productDto.ExpiryDate;
            existing.IsActive = productDto.IsActive;
            existing.UpdatedDate = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(existing);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        // Mapping helpers
        private static ProductDto MapToDto(Product p) =>
            new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                Brand = p.Brand,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                Stock = p.Stock,
                SKU = p.SKU,
                Barcode = p.Barcode,
                Weight = p.Weight,
                Height = p.Height,
                Width = p.Width,
                Length = p.Length,
                Color = p.Color,
                Material = p.Material,
                ManufactureDate = p.ManufactureDate,
                ExpiryDate = p.ExpiryDate,
                IsActive = p.IsActive
            };

        private static Product MapToEntity(ProductDto dto) =>
            new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                Brand = dto.Brand,
                Price = dto.Price,
                DiscountPrice = dto.DiscountPrice,
                Stock = dto.Stock,
                SKU = dto.SKU,
                Barcode = dto.Barcode,
                Weight = dto.Weight,
                Height = dto.Height,
                Width = dto.Width,
                Length = dto.Length,
                Color = dto.Color,
                Material = dto.Material,
                ManufactureDate = dto.ManufactureDate,
                ExpiryDate = dto.ExpiryDate,
                IsActive = dto.IsActive
            };
    }
}
