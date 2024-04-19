using System.Diagnostics;

namespace Infrastructure.Services
{
    public class AdminService
    {
        public async Task<string> GetToken()
        {
            string url = "https://localhost:7295/token?key=NGYyMmY5ZTgtNjI4ZS00NjdmLTgxNmEtMTI2YjdjNjk4ZDA1";
            using var client = new HttpClient();

            try
            {
                var response = await client.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    return token;
                }
            }
            catch (Exception e) { Debug.WriteLine($"Error: {e.Message}"); }
            return null!;
        }
    }
}
