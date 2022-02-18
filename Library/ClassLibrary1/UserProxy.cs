
using System.Net.Http;
using System.Threading.Tasks;
using Avanade.Library.Entities;
using System.Text.Json;
using System.Text;

namespace Avanade.Library.Proxy
{
    public static class UserProxy
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string HttpBasePath = "http://localhost:85/api/user";
        private static readonly string MimeType = "application/json";

        public static async Task<IUser> GetUser(User content)
        {
            string json = JsonSerializer.Serialize<User>(content);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, MimeType);
            HttpResponseMessage streamTask = await client.PostAsync(HttpBasePath + "/GetUser", stringContent);
            var usersResult = await JsonSerializer.DeserializeAsync<User>(await streamTask.Content.ReadAsStreamAsync());
            return usersResult;
        }

        
    }
}
