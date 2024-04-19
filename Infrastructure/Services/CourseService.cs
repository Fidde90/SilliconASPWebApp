using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Services
{
    public class CourseService(HttpClient client, IConfiguration configuration)
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = "https://localhost:7295/api/Courses/";

        [HttpPost]
        public async Task<bool> CreateCourseAsync(CourseDto newCourse, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var url = $"{_url}?key={_configuration["ApiKey:Secret"]}";
                var json = JsonConvert.SerializeObject(newCourse);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }

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

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
        {
            try
            {
                var response = await _client.GetAsync($"https://localhost:7295/getAllAdmin?key={_configuration["ApiKey:Secret"]}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(json);
                    if (data!.Count() > 0)
                        return data!;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<CourseDto> GetOneCourseAsync(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{_url}{id}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CourseDto>(json);
                    if (data != null)
                        return data;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }

        public async Task<UpdateCourseDto> UpdateCourseAsync(UpdateCourseDto newDto, string token)
        {
            if (newDto != null)
            {
                try
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var json = JsonConvert.SerializeObject(newDto);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PutAsync($"{_url}?key={_configuration["ApiKey:Secret"]}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var newJson = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<UpdateCourseDto>(newJson);
                        if (data != null)
                            return data;
                    }
                }
                catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            }
            return null!;
        }

        public async Task<bool> DeleteCourseAsync(int id, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.DeleteAsync($"{_url}{id}?key={_configuration["ApiKey:Secret"]}");
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
