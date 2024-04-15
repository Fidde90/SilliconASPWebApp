using Infrastructure.Dtos;
using Infrastructure.Models.Forms;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Services
{
    public class ContactService(HttpClient client, IConfiguration configuration)
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;
        private readonly string _url = $"https://localhost:7295/api/Contact";

        public async Task<bool> SeandMessageAsync(ContactFormModel form)
        {
            try
            {
                var newMessage = new ContactDto
                {
                    FullName = form.FullName,
                    Email = form.Email,
                    Service = form.Service!,
                    Message = form.Message
                };

                if (newMessage != null)
                {
                    var json = JsonConvert.SerializeObject(newMessage);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{_url}?key={_configuration["ApiKey:Secret"]}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return false;
        }
    }
}
