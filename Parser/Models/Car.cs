using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Parser.Models
{
    [JsonObject]
    internal class Car
    {
        public int Id { get; set; }

        [JsonProperty("dt")] public DateTime DateOfAdded { get; set; }

        [JsonProperty("source")] public string Website { get; set; }

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

        [JsonProperty("run")]
        public string Run { get; set; }

        [JsonProperty("run_ed")]
        public string Run_ed { get; set; }

        public string Modification { get; set; }

        public string Phone { get; set; }

        [JsonProperty("viewed")] public string ViewedCount { get; set; }

        [JsonProperty("fio")] public string FIOOwner { get; set; }

        public string VIN { get; set; }

        [JsonProperty("e_mail")] public string Email { get; set; }

        public IEnumerable<string> Photos => Photo?.Split(new[] {','});

        public string Wheel { get; set; }

        public string Photo { get; set; }

        public string Complect { get; set; }

        [JsonProperty("average_price")] public int AveragePrice { get; set; }

        [JsonProperty("difference_price")] public int DifferencePrice { get; set; }

        public string Fast { get; set; }

        public string Protected { get; set; }

        public Status CarStatus() => PtsOwner <= 2 &&
                                DifferencePrice < 0 &&
                                Company == Company.FL &&
                                PhoneFind <= 1 &&
                                Price >= 200000 && Price <= 400000
                                ? Status.Good : Status.Bad;

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(DateOfAdded)}: {DateOfAdded}, {nameof(Region)}: {Region}, {nameof(Url)}: {Url}, {nameof(Model)}: {Model}, {nameof(Marka)}: {Marka}, {nameof(Color)}: {Color}, {nameof(Body)}: {Body}, {nameof(Transmission)}: {Transmission}, {nameof(EngineType)}: {EngineType}, {nameof(EngineVolume)}: {EngineVolume}, {nameof(Condition)}: {Condition}, {nameof(Drive)}: {Drive}, {nameof(Mileage)}: {Mileage}, {nameof(Run)}: {Run}, {nameof(Run_ed)}: {Run_ed}, {nameof(Year)}: {Year}, {nameof(Modification)}: {Modification}, {nameof(Phone)}: {Phone}, {nameof(ViewedCount)}: {ViewedCount}, {nameof(PtsOwner)}: {PtsOwner}, {nameof(FIOOwner)}: {FIOOwner}, {nameof(Info)}: {Info}, {nameof(Address)}: {Address}, {nameof(VIN)}: {VIN}, {nameof(Email)}: {Email}, {nameof(Company)}: {Company}, {nameof(Photos)}: {Photos}, {nameof(Wheel)}: {Wheel}, {nameof(Photo)}: {Photo}, {nameof(Complect)}: {Complect}, {nameof(Price)}: {Price}, {nameof(PhoneFind)}: {PhoneFind}, {nameof(AveragePrice)}: {AveragePrice}, {nameof(DifferencePrice)}: {DifferencePrice}, {nameof(Website)}: {Website}, {nameof(Fast)}: {Fast}, {nameof(Protected)}: {Protected}";
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