using System.ComponentModel.DataAnnotations;
using Backend.Base.Entities;

namespace Backend.Features.UserProfiles;

public class UserProfile : Entity
{
    //user
    //teams
    [Required] 
    public string FacebookLink { get; set; }
    
    [Required] [Phone]
    public string Phone { get; set; }
    
    [Required] 
    public DateOnly Birthday { get; set; }
    
    [Required]
    public string Picture { get; set; } //???
}