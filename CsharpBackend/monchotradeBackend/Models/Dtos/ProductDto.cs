using System.ComponentModel.DataAnnotations;
namespace monchotradebackend.models.dtos; 
/*
    id: 1,
    title: 'Vintage Camera',
    imageUrl: '/path/to/camera.jpg',
    offeredBy: 'Alice',
    description: 'Classic film camera in excellent condition'
*/
public class ProductDto{
    public int Id {get;set;}
    public string Title {get;set;} = string.Empty;
    public string ImageUrl {get;set;} = string.Empty;
    public string OfferedBy {get; set;} = string.Empty; 
    public string Description{get;set;} = string.Empty;

    public string Category {get;set;} = string.Empty; 

}