using System.ComponentModel.DataAnnotations;
namespace monchotradebackend.models.dtos; 
public class ProfileImageDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public IFormFile? ImageFile {get;set;}
}
