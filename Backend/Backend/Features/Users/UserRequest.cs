using System.ComponentModel.DataAnnotations;

namespace Backend.Features.Users;

public class UserRequest
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public List<string> Roles { get; set; }
}