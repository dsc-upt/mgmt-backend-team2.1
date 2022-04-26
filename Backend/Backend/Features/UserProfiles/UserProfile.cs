using System.ComponentModel.DataAnnotations;
using Backend.Base.Entities;
using Backend.Features.Users;
using Backend.Features.Teams;

namespace Backend.Features.UserProfiles;

public class UserProfile : Entity
{
    //user
    //public User User {get; set;}
    //teams public
    //public Team Team { get; set; }

    [Required] 
    public string FacebookLink { get; set; }
    
    [Required] [Phone]
    public string Phone { get; set; }
    
    [Required] 
    public DateOnly Birthday { get; set; }
    
    [Required]
    public string Picture { get; set; } //???
}