using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCocktail.Api.Dto
{
    public class MeasureDto
    {
        public string Id { get; set; }
        public string Quantity { get; set; }
        public IngredientDto Ingedient { get; set; }
    }
}
