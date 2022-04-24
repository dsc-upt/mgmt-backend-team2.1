using Backend.Features.Users;

namespace Backend.Features.Teams;

public class TeamResponse
{
    public UserResponse TeamLead { get; set; } // e de tipul "userresponse" ca il afisezi doar in controller (fara created si updated) 
    
    public string Name { get; set; }
    
    public string GitHubLink { get; set; }
    
    public string Id  {get; set; }
}