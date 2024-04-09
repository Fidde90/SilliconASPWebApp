using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Buffers;
using System.Diagnostics;
using System.Net;

namespace Infrastructure.Services
{
    public class CourseService(HttpClient client, IConfiguration configuration)
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = "https://localhost:7295/api/Courses";

        [HttpGet]
        public async Task<IEnumerable<CourseDto>> GetCoursesAsync(string category = "", string searchValue = "")
        {
            try
            {
                var response = await _client.GetAsync($"{_url}?category={Uri.UnescapeDataString(category)}?searchValue={Uri.UnescapeDataString(searchValue)}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CourseResult>(json);
                    if (data!.Courses != null && data.Succeeded)
                    {
                        return data.Courses;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
