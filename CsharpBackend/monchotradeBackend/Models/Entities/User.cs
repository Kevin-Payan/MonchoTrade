using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace monchotradebackend.models.entities; 

public class User{
    public int Id{get; set;}
    public string Name{get;set;} = string.Empty; 
    public string LastName{get; set;} = string.Empty; 
   
    /*String no nullables necesitan tener string.Empty  
    El ? despues del tipo de dato hace que ese atributo sea
    nullable, osea no requerido necesariamente*/
    public string? SecondLastName {get;set;}
    public DateOnly BirthDate{get;set;}
    public string Email{get;set;} = string.Empty; 
    
    public string Country{get;set;} = string.Empty; 

    public string PhoneNumber{get;set;} = string.Empty; 

    public string PasswordHash {get; set;} = string.Empty;

}