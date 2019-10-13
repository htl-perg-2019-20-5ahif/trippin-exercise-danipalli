using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;

namespace TrippinUserCreator
{
    public class TrippinUsers
    {
        [JsonPropertyName("value")]
        public List<TrippinUser> Users { get; set; }
    }

    public class TrippinUser
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("Emails")]
        public List<string> Emails { get; set; }

        [JsonPropertyName("AddressInfo")]
        public List<AddressInfo> AddressInfo { get; set; }
    }

    public class AddressInfo
    {
        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("City")]
        public City City { get; set; }
    }

    public class City
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("CountryRegion")]
        public string CountryRegion { get; set; }

        [JsonPropertyName("Region")]
        public string Region { get; set; }
    }

    public class TrippinJsonFileUsers
    {
        [JsonPropertyName("value")]
        public List<TrippinJsonFileUser> Users { get; set; }
    }

    public class TrippinJsonFileUser
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("Email")]
        public string Emails { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("CityName")]
        public string CityName { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }
    }
}
