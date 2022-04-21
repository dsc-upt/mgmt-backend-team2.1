using Backend.Database;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Add(UserRequest userRequest)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            Email = userRequest.Email,
            Roles = userRequest.Roles,
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return Ok(new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }

    [HttpGet]
    public async Task<ActionResult<UserRequest>> Get()
    {
        return Ok(
            await _dbContext.Users.Select(
                user => new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = user.Roles,
                }
            ).ToListAsync()
        );
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserRequest>> GetById([FromRoute]string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("Id not found");
        } 
        
        return Ok(
            new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.Roles,
            }
        );
    }

    
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteById([FromRoute] string id)
    {
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return Ok(user);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserRequest>> UpdateById([FromRoute] string id, [FromBody]UserRequest userRequest)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null) 
        {
            return NotFound("Id not found");
        }
    
        user.FirstName = userRequest.FirstName;
        user.LastName = userRequest.LastName;
        user.Email = user.Email;
        user.Roles = userRequest.Roles;
        
        await _dbContext.SaveChangesAsync();
        return Ok(user);

    }
    
    [HttpPost("role/{id}")]
    public async Task<ActionResult<UserRequest>> AddRole([FromRoute] string id, [FromBody]string role)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("Id not found");
        }
        
        user.Roles.Add(role);
        user.Updated=DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        
        return Ok(new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }
    
    [HttpDelete("remove role/{id}")]
    public async Task<ActionResult<UserRequest>> RemoveRole([FromRoute] string id, [FromBody]string role)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("Id not found");
        }

        var role1 = user.Roles.Find(element => element == role);
        if (role1 == null)
        {
            return NotFound("Role not found");
        }
        
        user.Roles.Remove(role);
        user.Updated=DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        
        return Ok(new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = user.Roles,
        });
    }
}
