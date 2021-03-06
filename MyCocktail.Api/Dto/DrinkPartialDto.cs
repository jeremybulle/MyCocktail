using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Api.Dto
{
    [ExcludeFromCodeCoverage]
    public class DrinkPartialDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UrlPicture { get; set; }
        public string CategoryName { get; set; }
        public string GlassName { get; set; }
        public string AlcoholicName { get; set; }
    }
}
