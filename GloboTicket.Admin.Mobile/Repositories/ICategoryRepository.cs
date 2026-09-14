using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Repositories;


public interface ICategoryRepository
{
    Task<List<CategoryModel>> GetAllCategoriesAsync();
    Task<CategoryModel?> GetCategoryAsync(Guid id);
    Task<bool> AddCategoryAsync(CategoryModel categoryModel);
    Task<bool> UpdateCategoryAsync(CategoryModel categoryModel);
}