using Backend.Features.Users;

namespace Backend.Features.Clients;

public class ClientResponse
{
    public string Name { get; set; }
    
    public UserResponse ContactPerson { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Id { get; set; }
}