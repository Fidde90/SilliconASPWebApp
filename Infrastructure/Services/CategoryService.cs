using Infrastructure.Dtos;
using Infrastructure.Factories;
using Infrastructure.Models.Forms;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Services
{
    public class CategoryService(HttpClient client, IConfiguration configuration)
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = "https://localhost:7295/api/Category";

        public async Task<string> CreateCategoryAsync(CategoryFormModel model) 
        {
            try
            {
                var category = CategoryMapper.ToCreateCategoryDto(model);             
                var json = JsonConvert.SerializeObject(category);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_url}?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {                  
                    return "created";
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_url}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(json);
                    return data!;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var response = await _client.DeleteAsync($"{_url}/{categoryId}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {                  
                    return true;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
    }
}
