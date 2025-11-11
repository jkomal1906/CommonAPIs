using CommonAPIs.DTOs;
using CommonAPIs.Models;
using CommonAPIs.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace CommonAPIs.IRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CommonAPIsDbContext _context;

        public ProductRepository(CommonAPIsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Set<Product>().AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Set<Product>().Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Set<Product>().FindAsync(id);
            if (product == null)
                return false;

            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
