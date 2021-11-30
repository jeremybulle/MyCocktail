using MyCocktail.Domain.EntitySource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Repositories
{
    public class CocktailDbSourceRepository
    {
        private HttpClient _client = new HttpClient();

        public CocktailDbSourceRepository()
        {
            //_client.BaseAddress = new Uri("https://thecocktaildb.com/api/json/v1/1/search.php");
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<DrinkSource>> GetCocktailSourcesByLetter(char letter)
        {
            var query = string.Concat("https://thecocktaildb.com/api/json/v1/1/search.php?f=", letter.ToString());

            var response = await _client.GetAsync(query);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;

                var cockatails = JsonConvert.DeserializeObject<DrinksSource>(responseBody);
                return cockatails.drinks;
            }
            System.Console.WriteLine("Error calling APi");
            return new List<DrinkSource>();
        }
    }
}
