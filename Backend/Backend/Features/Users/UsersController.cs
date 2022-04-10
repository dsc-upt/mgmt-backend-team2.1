using Backend.Database;
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

    [HttpDelete]
    public async Task<ActionResult<UserRequest>> DeleteById([FromRoute] string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return 
       
    }
    
}