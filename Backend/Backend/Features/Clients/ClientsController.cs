using Backend.Database;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.Clients;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{

    private readonly AppDbContext _dbContext;

    public ClientsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<ClientResponse>> Add(ClientRequest clientRequest)
    {
        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = clientRequest.Name,
            ContactPerson = clientRequest.ContactPerson,
            Email = clientRequest.Email,
            Phone = clientRequest.Phone,
        };

        client.ContactPerson.Id = Guid.NewGuid().ToString();
        client.ContactPerson.Created = DateTime.UtcNow;
        client.ContactPerson.Updated = DateTime.UtcNow;

        await _dbContext.AddAsync(client);
        await _dbContext.SaveChangesAsync();
        
        return Ok(new ClientResponse
        {
            Id = client.Id,
            Name = client.Name,
            ContactPerson = client.ContactPerson,
            Email = client.Email,
            Phone = client.Phone,
        });
    }
    
}