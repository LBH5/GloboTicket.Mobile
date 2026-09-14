using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Services;


public interface ICategoryService
{
    Task<List<CategoryModel>> GetAllCategoriesAsync();
}