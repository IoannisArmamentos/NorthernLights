using System.Collections.Generic;

namespace NorthernLights.Models
{
    public class Weather
    {
        public double id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Current
    {
        public double dt { get; set; }
        public double sunrise { get; set; }
        public double sunset { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double dew_podouble { get; set; }
        public double uvi { get; set; }
        public double clouds { get; set; }
        public double visibility { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg { get; set; }
        public double wind_gust { get; set; }
        public List<Weather> weather { get; set; }
    }

    public class Minutely
    {
        public double dt { get; set; }
        public double precipitation { get; set; }
    }

    public class Rain
    {
        public double _1h { get; set; }
    }

    public class Hourly
    {
        public double dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double dew_podouble { get; set; }
        public double uvi { get; set; }
        public double clouds { get; set; }
        public double visibility { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg { get; set; }
        public double wind_gust { get; set; }
        public List<Weather> weather { get; set; }
        public double pop { get; set; }
        public Rain rain { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class FeelsLike
    {
        public double day { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class Daily
    {
        public double dt { get; set; }
        //public string dt_DateTime { get; set; }
        public double sunrise { get; set; }
        //public string sunriseTime { get; set; }
        public double sunset { get; set; }
        //public string sunsetTime { get; set; }
        public double moonrise { get; set; }
        public double moonset { get; set; }
        public double moon_phase { get; set; }
        public Temp temp { get; set; }
        public FeelsLike feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double dew_podouble { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg { get; set; }
        public double wind_gust { get; set; }
        public List<Weather> weather { get; set; }
        public double clouds { get; set; }
        public double pop { get; set; }
        public double rain { get; set; }
        public double uvi { get; set; }
    }

    public class Alert
    {
        public string sender_name { get; set; }
        public string @event { get; set; }
        public double start { get; set; }
        public string startDateTime { get; set; }
        public double end { get; set; }
        public string endDateTime { get; set; }
        public string description { get; set; }
        public List<string> tags { get; set; }
    }

    public class Root
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public double timezone_offset { get; set; }
        public Current current { get; set; }
        public List<Minutely> minutely { get; set; }
        public List<Hourly> hourly { get; set; }
        public List<Daily> daily { get; set; }
        public List<Alert> alerts { get; set; }
    }

}
