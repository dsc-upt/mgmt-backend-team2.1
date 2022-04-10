using System.ComponentModel.DataAnnotations;
using Backend.Features.Users;

namespace Backend.Features.Teams;

public class TeamRequest
{
    [Required] 
    public User TeamLead { get; set; }

    [Required] 
    public string Name { get; set; }

    [Required]
    public string GitHubLink { get; set; }
}