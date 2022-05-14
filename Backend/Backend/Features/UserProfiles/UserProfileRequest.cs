using System.ComponentModel.DataAnnotations;
using Backend.Features.Teams;

namespace Backend.Features.UserProfiles;

public class UserProfileRequest
{
    [Required]
    public string UserId { set; get; }
    
    [Required]
    public List<Team> Teams { get; set; }
    
    [Required]
    public string FacebookLink { get; set; }
    
    [Required]
    [Phone]
    public string Phone { get; set; }
    
    [Required]
    public DateOnly Birthday { get; set; }
    
    [Required]
    public string Picture { get; set; }
}