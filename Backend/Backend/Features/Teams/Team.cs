using System.ComponentModel.DataAnnotations;
using Backend.Base.Entities;
using Backend.Features.Users;

namespace Backend.Features.Teams;

public class Team : Entity
{
    public User TeamLead { get; set; }
    
    public string Name { get; set; }
    
    public string GitHubLink { get; set; }
    
}