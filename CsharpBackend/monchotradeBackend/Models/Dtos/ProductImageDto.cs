using System.ComponentModel.DataAnnotations;
namespace monchotradebackend.models.dtos; 
public class ProductImageDto
{
    [Required]
    public int ProducId { get; set; }
    [Required]
    public IFormFile? ImageFile {get;set;}
}
