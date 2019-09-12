using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://mysafeinfo.com/api/data?list=englishmonarchs&format=json";
            var clientId = "spiir-3f467ffe-0e0e-4c9c-9a6d-793209d7b4dd";
            var clientSecret = "f6c56ba248451e9574745a3b94b448a0a9f60f59557243ee39c1f884ce947e24";
            
            var monarchs = new Monarchs(url, clientId, clientSecret);
            monarchs.GetMonarchs();
            
            Console.WriteLine("Press ESC to exit the application");
            do {
                while (! Console.KeyAvailable) {
                    // just for displaying purposes
                }       
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}