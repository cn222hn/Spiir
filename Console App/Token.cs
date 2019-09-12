using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Console_App.Model;
using Newtonsoft.Json;

namespace Console_App
{
    /*
     * Token class meant to handle the process automatically
     * However, it is not possible in the current state as the html received back
     * Do not contain the necessary information to log in by code and get the
     * Access code for the tokens.
     */
    class Token
    {
        public async Task<AccessToken> GetToken(HttpClient client, string clientId, string clientSecret)
        {
            var url = await Initialize(client);
            var code = await GetCode(client, url);

            var model = new AccessToken
            {
                Code = code
            };

            var content =
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var request = await client.PostAsync("https://api.nordicapigateway.com/v1/authentication/tokens", content);
            var response = await request.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AccessToken>(response);
        }

        private async Task<string> Initialize(HttpClient client)
        {
            //Prepare Request Body
            var model = new User
            {
                UserHash = "test-user-id",
                RedirectUrl = "https://developer.nordicapigateway.com/demo/redirect"
            };

            var content =
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            //Request Token
            var request = await client.PostAsync(" https://api.nordicapigateway.com/v1/authentication/initialize",
                content);

            return await request.Content.ReadAsStringAsync();
        }

        private async Task<string> GetCode(HttpClient client, string url)
        {
            var auth = JsonConvert.DeserializeObject<AccessToken>(url);
            var code = await client.GetAsync(auth.AuthUrl);

            return await code.Content.ReadAsStringAsync();
        }
    }
}