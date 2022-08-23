using System;
using System.Threading.Tasks;
using NMS.Models;

namespace NMS.Core.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> CreateCategory(Category category);
    }
}