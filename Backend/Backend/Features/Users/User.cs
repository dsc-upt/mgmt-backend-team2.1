using System.ComponentModel.DataAnnotations;
using Backend.Base.Entities;

namespace Backend.Features.Users;

public class User:Entity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [EmailAddress] 
    public string Email { get; set; }
    
    public List<string> Roles { get; set; }
}