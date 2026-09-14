using System.Net.Http.Json;
using System.Text.Json;
using GloboTicket.Admin.Mobile.Model;

namespace GloboTicket.Admin.Mobile.Repositories;



public class CategoryRepository(IHttpClientFactory httpClient) : ICategoryRepository
{

    public Task<bool> AddCategoryAsync(CategoryModel categoryModel)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        // TODO: Implement the logic to add a category
        return Task.FromResult(false);
    }

    public async Task<List<CategoryModel>> GetAllCategoriesAsync()
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        try
        {
            var categories = await client.GetFromJsonAsync<List<CategoryModel>>("categories",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            return await Task.FromResult(categories ?? []);
        }
        catch (Exception)
        {
            return await Task.FromResult(new List<CategoryModel>());
        }
    }

    public async Task<CategoryModel?> GetCategoryAsync(Guid id)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        try
        {
            return await client.GetFromJsonAsync<CategoryModel>($"categories/{id}",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public Task<bool> UpdateCategoryAsync(CategoryModel categoryModel)
    {
        using var client = httpClient.CreateClient("GloboTicketAdminAPIClient");
        // TODO: Implement the logic to update a category
        return Task.FromResult(false);
    }
}
