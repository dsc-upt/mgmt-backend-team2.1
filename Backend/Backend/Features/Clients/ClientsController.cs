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
        var contactPerson =
            await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == clientRequest.ContactPersonId);
        if (contactPerson == null)
        {
            return NotFound("Contact person id not found");
        }
        
        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = clientRequest.Name,
            ContactPerson = contactPerson, //datele user-ului
            Email = clientRequest.Email,
            Phone = clientRequest.Phone,
        };

        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
        
        return Ok(new ClientResponse
        {
            Id = client.Id,
            Name = client.Name,
            ContactPerson = new UserResponse
            {
                FirstName = client.ContactPerson.FirstName,
                LastName = client.ContactPerson.LastName,
                Email = client.ContactPerson.Email,
                Roles = client.ContactPerson.Roles,
                Id = client.ContactPerson.Id,
                
            },
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
                    ContactPerson = new UserResponse
                    {
                        FirstName = client.ContactPerson.FirstName,
                        LastName = client.ContactPerson.LastName,
                        Email = client.ContactPerson.Email,
                        Roles = client.ContactPerson.Roles,
                        Id = client.ContactPerson.Id,
                    },
                    Email = client.Email,
                    Phone = client.Phone,
                }
            ).ToListAsync()
        );
    }
    
    
    [HttpGet("{id}")] //aceeasi eroare ca la teams
    public async Task<ActionResult<ClientRequest>> GetById([FromRoute] string id)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(entity => entity.Id == id);
        if (client == null)
        {
            return NotFound("Client id not found");
        } 
        
        return Ok(
            new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                ContactPerson = new UserResponse
                {
                    FirstName = client.ContactPerson.FirstName,
                    LastName = client.ContactPerson.LastName,
                    Email = client.ContactPerson.Email,
                    Roles = client.ContactPerson.Roles,
                    Id = client.ContactPerson.Id,
                },
                Email = client.Email,
                Phone = client.Phone,
            }
        );
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Client>> DeleteById([FromRoute] string id)
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
    public async Task<ActionResult<ClientRequest>> UpdateById([FromRoute] string id, [FromBody] ClientRequest clientRequest)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(entity => entity.Id == id);
        if (client == null) 
        {
            return NotFound("Client id not found");
        }

        var contactPerson =
            await _dbContext.Users.FirstOrDefaultAsync(element => element.Id == clientRequest.ContactPersonId);
        if (contactPerson == null)
        {
            return NotFound("Contact person id not found");
        }
        
        client.Name = clientRequest.Name;
        client.ContactPerson = contactPerson;
        client.Email = client.Email;
        client.Phone = clientRequest.Phone;
        
        await _dbContext.SaveChangesAsync();
        
        return Ok(client);

    }


    [HttpPost("{id}/{contactPersonId}")]//change contact person
    public async Task<ActionResult<ClientRequest>> ChangeById([FromRoute] string id, [FromRoute] string contactPersonId)
    {
        var client = await _dbContext.Clients.FirstOrDefaultAsync(entity => entity.Id == id);
        if (client == null)
        {
            return NotFound("Client id not found");
        }

        var contactPerson = await _dbContext.Users.FirstOrDefaultAsync(element => element.Id == contactPersonId);
        if (contactPerson == null)
        {
            return NotFound("Contact person id not found");
        }

        client.ContactPerson = contactPerson;
        //client.Updated = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        
        return Ok(
            new ClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                ContactPerson = new UserResponse
                {
                    FirstName = client.ContactPerson.FirstName,
                    LastName = client.ContactPerson.LastName,
                    Email = client.ContactPerson.Email,
                    Roles = client.ContactPerson.Roles,
                    Id = client.ContactPerson.Id,
                },
                Email = client.Email,
                Phone = client.Phone,
            }
        );
    }
}