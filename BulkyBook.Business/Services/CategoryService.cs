using BulkyBook.Business.Services.IServices;
using BulkyBook.Models;
using BulkyBook.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }
        
        public async Task CreateCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

     

        public async Task UpdateCategoryAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

      
       
        public async Task<bool> CategoryExistsAsync(string categoryName, int? categoryId = null)
        {
            if (categoryId.HasValue)
            {
                return await _dbContext.Categories.AnyAsync(c => c.Name.ToLower().Trim() == categoryName.ToLower().Trim() && c.Id != categoryId.Value);
            }
            else
            {
                return await _dbContext.Categories.AnyAsync(c => c.Name.ToLower().Trim() == categoryName.ToLower().Trim());
            }
        }

        
    }
}
