using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheWeather.Models;
using Xamarin.Forms;

namespace TheWeather.ViewModels
{
    public class WeatherPageViewModel : INotifyPropertyChanged
    {
        private WeatherData data;
        #region[ PropertyChanged ]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion[ PropertyChanged ]
        public WeatherData Data
        {
            get => data; set
            {
                data = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; set; }

        public WeatherPageViewModel()
        {
            SearchCommand = new Command(async (searchTerm) =>
            {
                await GetData("https://api.weatherbit.io/v2.0/current?lat=31.72024&lon=-106.46084&key=71f4377a76e24db1ad0cadbeff478b2c");
            });
        }


        private async Task GetData(string url)
        {
            var cliente = new HttpClient();
            var response = await cliente.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeatherData>(jsonResult);
            Data = result;
        }
    }
}
