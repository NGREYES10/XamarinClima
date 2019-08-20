using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinClima.Models;

namespace XamarinClima.ViewModels
{
    public class MainPageViewModel
    {
        public WeatherData Data { get; set; }
        public ICommand SearchCommand { get; set; }

        public MainPageViewModel()
        {
            SearchCommand = new Command(async (searchTerm) =>
            {
                await GetData("https://api.darksky.net/forecast/362c425c81aca7a87cd0e0f47a7a556c/19.6863397,-99.0185679");
            });
        }

        public async Task GetData(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync(client.BaseAddress);

            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WeatherData>(jsonResult);
            Data = result;

        }
    }   
}
