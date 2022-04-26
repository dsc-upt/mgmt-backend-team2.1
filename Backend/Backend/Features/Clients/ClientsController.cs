using Backend.Database;
using Backend.Features.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [HttpGet]
    public async Task<ActionResult<ClientRequest>> Get()
    {
        return Ok(
            await _dbContext.Clients.Select(
                client => new ClientResponse
                {
                    Id = client.Id,
                    Name = client.Name,
                    ContactPerson = client.ContactPerson,
                    Email = client.Email,
                    Phone = client.Phone,
                }
            ).ToListAsync()
        );
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientRequest>> GetById([FromRoute]string id)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(entity => entity.Id == id);
        if (client == null)
        {
            return NotFound("Id not found");
        } 
        
        return Ok(
            new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                ContactPerson = client.ContactPerson,
                Email = client.Email,
                Phone = client.Phone,
            }
        );
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteById([FromRoute] string id)
    {
        
        var client = await _dbContext.Clients.FirstOrDefaultAsync(entity => entity.Id == id);
        if (client == null)
        {
            return NotFound("User not found");
        }
        
        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync();

        return Ok(client);

    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ClientRequest>> UpdateById([FromRoute] string id, [FromBody]ClientRequest clientRequest)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(entity => entity.Id == id);
        if (client == null) 
        {
            return NotFound("Id not found");
        }
    
        client.Name = clientRequest.Name;
        client.ContactPerson = clientRequest.ContactPerson;
        client.Email = client.Email;
        client.Phone = clientRequest.Phone;
        
        await _dbContext.SaveChangesAsync();
        
        return Ok(client);

    }
    
}