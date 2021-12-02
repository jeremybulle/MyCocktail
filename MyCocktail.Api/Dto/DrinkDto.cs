using System.Collections.Generic;

namespace MyCocktail.Api.Dto
{
    public class DrinkDto
    {
        public string Id { get; set; }
        public string IdSource { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public string DateModified { get; set; }
        public string UrlPicture { get; set; }
        public string IdOwner { get; set; }
        public CategoryDto Category { get; set; }
        public GlassDto Glass { get; set; }
        public AlcoholicDto Alcoholic { get; set; }
        public IEnumerable<MeasureDto> Measures { get; set; }
    }
}
