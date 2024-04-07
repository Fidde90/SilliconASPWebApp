using Infrastructure.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class CourseService(HttpClient client, IConfiguration configuration)
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = "https://localhost:7295/api/Courses";

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_url}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(json);
                    return data!;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
