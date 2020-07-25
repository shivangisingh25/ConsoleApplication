using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication
{
    class Program
    {
        private string GetOrderbyid(int id)
        {
            string name = "";
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(name);
                client.BaseAddress = new Uri("http://20.189.74.207/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = client.PostAsync("api/Order/value/" + id, content).Result;

                if (response.IsSuccessStatusCode)
                    name = response.Content.ReadAsStringAsync().Result;
                else
                    return response.ToString();
            }
            return name;
        }
        private string Getmenu(string token)
        {
            string name = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://52.229.228.53/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer" + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = client.GetAsync("api/MenuItem/GetName").Result;

                if (response.IsSuccessStatusCode)
                    name = response.Content.ReadAsStringAsync().Result;
                else
                    name = null;
            }
            return name;
        }
        static string GetToken(string url)
        {
            User user = new User
            {
                Username = "Shivangi",
                Password = "1234"
            };
            var json = JsonConvert.SerializeObject(user);
            //User obj = JsonConvert.DeserializeObject<User>(json);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic detail = JObject.Parse(name);
                return detail.token;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Menu Items");
            Program menu = new Program();
            string token = GetToken("http://52.229.228.53/api/Token/TokenGenerate");
            Console.WriteLine(menu.Getmenu(token));
            Console.WriteLine("Select your Item: ");
            int val = Convert.ToInt32(Console.ReadLine());
            Program order = new Program();
            Console.WriteLine("You cart details: ");
            Console.WriteLine(order.GetOrderbyid(val));
        }
    }
}

