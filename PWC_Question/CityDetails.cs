using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWC_Question
{
    public class CityDetails
    {
        public string Name { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }

    }
    public class CurrentWeather
    {
        public double temperature { get; set; }
        public double windspeed { get; set; }
        public double winddirection { get; set; }
        public int weathercode { get; set; }
        public int is_day { get; set; }
        public string time { get; set; }
    }

    public class Root
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public CurrentWeather current_weather { get; set; }
    }
    public class CityData
    {
        double lat;
        double lon;
        internal List<CityDetails> cl;
        internal List<Root> weather = new List<Root>();
        public CityData()
        {
            cl = new List<CityDetails>()

            {
            new CityDetails() {Name="Kolkata",latitude=22.5675,longitude=88.3700},
            new CityDetails() { Name = "Delhi", latitude = 28.6100, longitude = 77.2300 },
            new CityDetails() { Name = "Mumbai", latitude = 19.0761, longitude = 72.8775 },
            new CityDetails() { Name = "Bangalore", latitude = 12.9789, longitude = 77.5917 },
            new CityDetails() { Name = "Chennai", latitude = 13.0825, longitude = 80.2750 }

            };

        }


        public async void Data(string cityName)
        {

            foreach (var item in cl)
            {
                if (item.Name == cityName)
                {
                    lat = item.latitude;
                    lon = item.longitude;
                }
            }
            Root obj = new Root();

            obj = getData(lat, lon);
            Console.WriteLine("===================================\n");
            Console.WriteLine("Weather Details in " + cityName);
            Console.WriteLine("Temperature: " + obj.current_weather.temperature);
            Console.WriteLine("WindSpeed: " + obj.current_weather.windspeed);
            Console.WriteLine("Wind Direction: " + obj.current_weather.winddirection);

            Console.WriteLine("Weather Code: " + obj.current_weather.weathercode);
            Console.WriteLine("Current Time: " + obj.current_weather.time);


            Console.WriteLine("===================================\n");

        }




        public Root getData(double lat, double lon)
        {
            //Root obj = new Root();
            HttpClient client = new HttpClient();

            var ResponseTask = client.GetAsync($"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true");
            ResponseTask.Wait();
            if (ResponseTask.IsCompleted)
            {
                var result = ResponseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var messageTask = result.Content.ReadAsStringAsync();
                    messageTask.Wait();
                    Root json = JsonConvert.DeserializeObject<Root>(messageTask.Result);
                    return json;
                }
            }
            return null;

        }



    }
}
