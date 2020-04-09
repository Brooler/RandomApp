using System.Net.Http;
using System.Threading.Tasks;
using RandomApp.Client.Interfaces;

namespace RandomApp.Client.Services
{
    public class DataService : IDataService
    {
        public async Task<int> GetRandomNumber()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:44370/api/random")) //TODO: Move address to configuration files...
                {
                    return int.Parse(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
