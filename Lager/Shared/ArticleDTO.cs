using System.ComponentModel.DataAnnotations;

namespace Lager.Shared
{
    public sealed record ArticleDTO(
        [property: Required] string Name, 
        [property: Required] string Number,
        string Provider,
        decimal Price,
        int PresentCount,
        int BoughtCount,
        [property: Required] int MinCount,
        [property: Required] ArticleOwnerDTO Owner,
        string Note,
        string? Base64ImageData);
}
