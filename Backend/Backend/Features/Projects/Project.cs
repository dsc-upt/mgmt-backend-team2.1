using Backend.Base.Entities;
using Backend.Features.Users;

namespace Backend.Features.Projects;

public class Project : Entity
{
    public string Name { get; set; }
    
    public User Manager { get; set; }
    
    public string Status { get; set; }
    
    //
}