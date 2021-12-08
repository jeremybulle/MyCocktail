using MyCocktail.Domain.EntitySource;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCocktail.Infrastucture.Repositories
{
    public class CocktailDbSourceRepository
    {
        private readonly HttpClient _client = new HttpClient();

        public CocktailDbSourceRepository()
        {
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
