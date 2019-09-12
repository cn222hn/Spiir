using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Console_App.Model;
using Newtonsoft.Json;

namespace Console_App
{
    public class Monarchs
    {
        private readonly string _url;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly Token _token;

        public Monarchs(string url, string clientId, string clientSecret)
        {
            _url = url;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _token = new Token();
        }

        public async void GetMonarchs()
        {
            using (var client = new HttpClient())
            {
                //Define Headers                                                                                       
                client.DefaultRequestHeaders.Accept.Clear();                                                           
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));      
                client.DefaultRequestHeaders.Add("X-Client-ID", _clientId);                                             
                client.DefaultRequestHeaders.Add("X-Client-Secret", _clientSecret);                                     
                client.DefaultRequestHeaders.Accept.Add(                                                               
                    new MediaTypeWithQualityHeaderValue("text/html")); // to accept the code format                    
                
                var token = "ygAAAAVDaXBoZXJ0ZXh0AJAAAAAAa1f8Cu6KKCL3zRA4+gs9lfVDAeZho7rzyR/V5nRfww996sZdNnFlksDG5oRyd8SC1I5Z+Min7QjEyi9emtwQGLtodfJiGP2XEjPs6CZR4wu2s/8rT0YFwFDZK86SHkW05ylKkvcEApI4vU6p4auxlg30KB/dW1rjjsDgETLO0D8fGlp+sCsDKPzJHZC4DVktBUl2ABAAAAAABrxGiW37koY10IaFDFBcYRBLZXlJZAAAAAAAAA==";

                var byteArray = Encoding.ASCII.GetBytes(token);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Convert.ToBase64String(byteArray));
                
                var response = await client.GetAsync(_url);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(content);
                }
            }
        }
    }
}