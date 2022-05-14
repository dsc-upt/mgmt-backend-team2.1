using System.ComponentModel.DataAnnotations;
using Backend.Features.Users;

namespace Backend.Features.Clients;

public class ClientRequest
{
    [Required]
    public string Name { get; set; }
   
    [Required]
    public string ContactPersonId { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string Phone { get; set; }
}