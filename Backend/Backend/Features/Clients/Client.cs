using System.ComponentModel.DataAnnotations;
using Backend.Base.Entities;
using Backend.Features.Users;

namespace Backend.Features.Clients;

public class Client : Entity
{
    public string Name { get; set; }
    
    public User ContactPerson { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
}