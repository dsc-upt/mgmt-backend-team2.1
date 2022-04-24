using System.ComponentModel.DataAnnotations;
using Backend.Features.Users;

namespace Backend.Features.Teams;

public class TeamRequest
{
    [Required] 
    //public User TeamLead { get; set; } //team leader-ul e deja in database, numai il cauti dupa id si il bagi intr-un team
    public string TeamLeadId { get; set; }

    [Required] 
    public string Name { get; set; }

    [Required]
    public string GitHubLink { get; set; }
}