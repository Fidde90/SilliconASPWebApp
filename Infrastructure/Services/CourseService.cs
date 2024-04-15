using Infrastructure.Dtos;
using Infrastructure.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SilliconASPWebApp.Models.Components;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Services
{
    public class CourseService(HttpClient client, IConfiguration configuration)
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = "https://localhost:7295/api/Courses";

        [HttpGet]
        public async Task<CourseResult> GetCoursesAsync(string category = "", string searchValue = "", int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _client.GetAsync($"{_url}?key={_configuration["ApiKey:Secret"]}&category={Uri.UnescapeDataString(category)}&searchValue={Uri.UnescapeDataString(searchValue)}&" +
                    $"pageNumber={Uri.UnescapeDataString(pageNumber.ToString())}&pageSize={Uri.UnescapeDataString(pageSize.ToString())}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CourseResult>(json);
                    if (data!.Courses != null && data.Succeeded)
                        return data;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<CourseResult> GetCoursesAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_url}?key={_configuration["ApiKey:Secret"]}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CourseResult>(json);
                    if (data!.Courses != null && data.Succeeded)
                        return data;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<UpdateCourseDto> UpdateCourseAsync(CourseCardModel dto)
        {
            try
            {
                var newDto = CourseMapping.ToUpdateCourseDto(dto);
                var json = JsonConvert.SerializeObject(newDto);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"{_url}?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    return newDto;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
