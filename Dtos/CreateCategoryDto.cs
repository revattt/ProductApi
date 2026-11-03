using System.ComponentModel.DataAnnotations;

namespace ProductApi.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
    }
}
