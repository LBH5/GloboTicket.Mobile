using GloboTicket.Admin.Mobile.Model;
using GloboTicket.Admin.Mobile.Repositories;

namespace GloboTicket.Admin.Mobile.Services;


public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{

    public async Task<List<CategoryModel>> GetAllCategoriesAsync()
    {
        return await categoryRepository.GetAllCategoriesAsync();
    }
}