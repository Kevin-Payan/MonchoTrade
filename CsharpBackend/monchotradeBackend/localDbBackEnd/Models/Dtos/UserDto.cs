using Microsoft.Net.Http.Headers;

namespace monchotradebackend.models.dtos; 


public class UserMyProfileResponseDto{
    public string Name {get;set;} = string.Empty; 
    public string LastName {get;set;} = string.Empty; 
    public string? SecondLastName {get; set;} = string.Empty;
    public string Email {get;set;} = string.Empty;
    public string PhoneNumber {get;set;} = string.Empty;
    public string Country {get;set;} = string.Empty;
    public string ProfileImageUrl {get;set;} = string.Empty; 
}



public class UserLoginDto{
    public string Email {get; set;} = string.Empty; 
    public string Password {get; set;} = string.Empty;
}
