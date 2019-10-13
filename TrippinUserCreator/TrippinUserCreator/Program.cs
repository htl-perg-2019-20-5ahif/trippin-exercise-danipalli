using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace TrippinUserCreator
{
    class Program
    {

        private static HttpClient HttpClient
            = new HttpClient() { BaseAddress = new Uri("https://services.odata.org/TripPinRESTierService/(S(uegqu1dhjggaqd5dk31p3wyb))/") };

        public static async Task<TrippinUsers> GetTrippinUsers()
        {
            var trippinUserResponse = await HttpClient.GetAsync("People");
            trippinUserResponse.EnsureSuccessStatusCode();
            var responseBody = await trippinUserResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TrippinUsers>(responseBody);
        }

        public static async Task PostTrippinUser(TrippinUser trippinUser)
        {
            var content = new StringContent(JsonSerializer.Serialize(trippinUser), Encoding.UTF8, "application/json");
            var trippinResponse = await HttpClient.PostAsync("People", content);
            trippinResponse.EnsureSuccessStatusCode();
        }

        public static async Task<string> ReadTextFile(string filename)
        {
            string text;
            try
            {
                // Read the dictionary
                text = await File.ReadAllTextAsync(filename);
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine("Json file not found!\n" + ex.ToString());
                throw;
            }

            return text;
        }

        public static void AddMissingUsers(TrippinUsers trippinUsers, TrippinJsonFileUsers jsonFileUsers)
        {
            foreach (TrippinUser trippinUser in trippinUsers.Users)
            {
                jsonFileUsers.Users.RemoveAll(user => user.UserName == trippinUser.UserName);
            }
            Console.WriteLine("Add " + jsonFileUsers.Users.Count + " Users");

            jsonFileUsers.Users.ForEach(async user => {
                await PostTrippinUser(new TrippinUser
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Emails = new List<string> { user.Emails },
                    AddressInfo = new List<AddressInfo> {
                        new AddressInfo
                        {
                            Address = user.Address,
                            City = new City
                            {
                                Name = user.CityName,
                                CountryRegion = user.Country,
                                Region = user.Country
                            }
                        }
                    }

                });
            });
        }

        static async Task Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Please specify exactly one json file.");
                return;
            }

            // Read users from Web API
            var trippinUsers = await GetTrippinUsers();

            // Read users from json file
            var jsonText = await ReadTextFile(args[0]);
            var jsonFileUsers = JsonSerializer.Deserialize<TrippinJsonFileUsers>(jsonText);

            AddMissingUsers(trippinUsers, jsonFileUsers);

            return;
        }

    }
}
