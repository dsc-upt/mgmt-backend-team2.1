using System.ComponentModel.DataAnnotations;
using Backend.Base.Entities;
using Backend.Features.Users;
using Backend.Features.Teams;

namespace Backend.Features.UserProfiles;

public class UserProfile : Entity
{
    public User User {get; set;}
    
    public List<Team> Team { get; set; }
    
    public string FacebookLink { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    public DateOnly Birthday { get; set; }
    
    public string Picture { get; set; } //???
}