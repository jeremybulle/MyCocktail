using System.Diagnostics.CodeAnalysis;

namespace MyCocktail.Api.Dto
{
    [ExcludeFromCodeCoverage]
    public class MeasureDto
    {
        public string Id { get; set; }
        public string Quantity { get; set; }
        public IngredientDto Ingedient { get; set; }
    }
}
