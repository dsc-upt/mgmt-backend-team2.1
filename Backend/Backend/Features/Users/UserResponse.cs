namespace Backend.Features.Users;

public class UserResponse
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public List<string> Roles { get; set; }
    
    public string Id { get; set; }
}