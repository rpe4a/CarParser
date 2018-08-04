using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Parser.Models
{
    [JsonObject]
    internal class Car
    {
        public int Id { get; set; }

        [JsonProperty("dt")] public DateTime DateOfAdded { get; set; }

        public string Source { get; set; }

        public Company Company { get; set; }

        public string Mileage => $"{Run} {Run_ed}";

        public string Year { get; set; }

        public int Price { get; set; }

        [JsonProperty("phone_find")] public int PhoneFind { get; set; }

        [JsonProperty("pts_owner")] public int PtsOwner { get; set; }

        public string Region { get; set; }

        public string Info { get; set; }

        public string Address { get; set; }

        public string Url { get; set; }

        public string Model { get; set; }

        public string Marka { get; set; }

        public string Color { get; set; }

        public string Body { get; set; }

        public string Transmission { get; set; }

        [JsonProperty("engine")] public string EngineType { get; set; }

        [JsonProperty("enginevol")] public string EngineVolume { get; set; }

        public string Condition { get; set; }

        public string Drive { get; set; }

        [JsonProperty("run")] public string Run { get; set; }

        [JsonProperty("run_ed")] public string Run_ed { get; set; }

        public string Modification { get; set; }

        public string Phone { get; set; }

        [JsonProperty("viewed")] public string ViewedCount { get; set; }

        [JsonProperty("fio")] public string FIOOwner { get; set; }

        public string VIN { get; set; }

        [JsonProperty("e_mail")] public string Email { get; set; }

        public IEnumerable<string> Photos => Photo?.Split(',');

        public string Wheel { get; set; }

        public string Photo { get; set; }

        public string Complect { get; set; }

        [JsonProperty("average_price")] public int AveragePrice { get; set; }

        [JsonProperty("difference_price")] public int DifferencePrice { get; set; }

        public string Fast { get; set; }

        public string Protected { get; set; }

        public Status CarStatus()
        {
            return PtsOwner <= 2 &&
                   DifferencePrice < 0 &&
                   Company == Company.FL &&
                   PhoneFind <= 1 &&
                   Price >= 200000 && Price <= 400000
                ? Status.Good
                : Status.Bad;
        }

        public string Website()
        {
            var site = string.Empty;

            if (string.IsNullOrWhiteSpace(Source))
                return site;

            var mileage = Regex.Match(Mileage ?? "", @"\d+").Value;

            switch (Source)
            {
                case "autoru":
                {
                    site =
                        $"https://auto.ru/perm/cars/all/?year_from={Year}&year_to={Year}&price_from={Price}&price_to={Price}&km_age_from={mileage}&km_age_to={mileage}&sort=cr_date-desc";

                    break;
                }
                case "avitoru":
                {
                    site = $"https://www.avito.ru/perm/avtomobili?pmax={Price}&pmin={Price}&radius=200&s=104&s_trg=3";
                    break;
                }
                case "amru":
                {
                    site =
                        $"https://am.ru/perm/search/?kladdr[0]=2442&kladdr[1]=52&price[min]={Price}&price[max]={Price}&mileage[min]={mileage}&mileage[max]={mileage}&years[min]={Year}&years[max]={Year}";
                    break;
                }
            }

            return site;
        }
    }

    internal enum Company
    {
        FL,
        UL
    }

    internal enum Status
    {
        Good,
        Bad
    }
}