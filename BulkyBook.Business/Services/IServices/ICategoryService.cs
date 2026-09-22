using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.Models;

namespace BulkyBook.Business.Services.IServices
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task CreateCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task DeleteCategoryAsync(int id);

        Task<bool> CategoryExistsAsync(string categoryName, int? categoryId = null);
    }
}
