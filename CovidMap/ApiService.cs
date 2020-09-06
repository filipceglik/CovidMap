using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MahApps.Metro.Converters;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using RestSharp;

namespace CovidMap
{
    public class ApiService
    {
        private readonly RestClient _restService = new RestClient("https://covid19-api.com/country/");
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        private readonly string apiKey = "why even bother validating the key";
        public IEnumerable<CountrySummary> GetLatestReportByCountryCode(string countryCode)
        {
            var request = new RestRequest("code", Method.GET)
                .AddParameter("format","json")
                .AddParameter("code",countryCode)
                .AddHeader("x-rapidapi-host", "covid-19-data.p.rapidapi.com")
                .AddHeader("x-rapidapi-key", apiKey);
            var response = _restService.Execute<IEnumerable<CovidData>>(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IEnumerable<CountrySummary>>(response.Content, _settings);
        }
        
        public IEnumerable<CountrySummary> GetLatestReportAllCountries()
        {
            var request = new RestRequest("all", Method.GET)
                .AddParameter("format","json")
                .AddHeader("x-rapidapi-host", "covid-19-data.p.rapidapi.com")
                .AddHeader("x-rapidapi-key", apiKey);
            var response = _restService.Execute<IEnumerable<CovidData>>(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IEnumerable<CountrySummary>>(response.Content, _settings);
        }

        public CovidData getData(IEnumerable<CountrySummary> countrySummaries)
        {
            CovidData tmp = new CovidData();
            tmp.Daily = countrySummaries;
            return tmp;
        }
    }
    public class CovidData
    {
        public IEnumerable<CountrySummary> Daily { get; set; }
    }
    
    public class CountrySummary
    {
        public int confirmed { get; set; }
        public int recovered { get; set; }
        public int critical { get; set; }
        public int deaths { get; set; }
        public string country { get; set; }
        public string code { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime lastChange { get; set; }
        public DateTime lastUpdate { get; set; }

        public Location gps
        {
            get { return new Location(latitude,longitude); }
            set { gps = value; }
        }
    }
}