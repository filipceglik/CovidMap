using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace CovidMap
{
    public class ApiService
    {
        private readonly RestClient _restService = new RestClient("https://covid19-api.com/country/");
        public IEnumerable<CountrySummary> GetLatestReportByCountryCode()
        {
            var request = new RestRequest("code", Method.GET)
                .AddParameter("format","json")
                .AddParameter("code","it")
                .AddHeader("x-rapidapi-host", "covid-19-data.p.rapidapi.com")
                .AddHeader("x-rapidapi-key", "no api key for you dear friend");
            var response = _restService.Execute<IEnumerable<CovidData>>(request);

            if (!response.IsSuccessful)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IEnumerable<CountrySummary>>(response.Content);
        }
    }
    public class CovidData
    {
        public IEnumerable<CountrySummary> Daily { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        
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
    }
}