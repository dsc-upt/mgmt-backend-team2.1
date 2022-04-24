using Backend.Database;
using Backend.Features.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Teams;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    
    public TeamsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    [HttpPost]
    public async Task<ActionResult<TeamResponse>> Add(TeamRequest teamRequest)
    {
        var teamLead = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == teamRequest.TeamLeadId);
        if (teamLead == null)
        {
            return NotFound("Team lead id not found");
        }
        
        var team = new Team
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            TeamLead = teamLead,
            Name = teamRequest.Name,
            GitHubLink = teamRequest.GitHubLink,
            
        };

        //team.TeamLead.Id = Guid.NewGuid().ToString(); //exista deja user-ul
        //team.TeamLead.Created = DateTime.UtcNow; 
        team.TeamLead.Updated = DateTime.UtcNow;
        
        await _dbContext.Teams.AddAsync(team);
        await _dbContext.SaveChangesAsync();
        
        return Ok(new TeamResponse
        {
            Id = team.Id,
            Name = team.Name,
            GitHubLink = team.GitHubLink,
            TeamLead = new UserResponse
            {
                Id = team.TeamLead.Id,
                FirstName = team.TeamLead.FirstName,
                LastName = team.TeamLead.LastName,
                Email = team.TeamLead.Email,
                Roles = team.TeamLead.Roles,
            },
        });
    }
    
    
    [HttpGet]
    public async Task<ActionResult<TeamRequest>> Get() //TeamResponse
    {
        return Ok(
            await _dbContext.Teams.Select(
                team => new TeamResponse
                {
                    Id = team.Id,
                    //TeamLead = team.TeamLead,
                    Name = team.Name,
                    GitHubLink = team.GitHubLink,
                    TeamLead = new UserResponse
                    {
                        Id = team.TeamLead.Id,
                        FirstName = team.TeamLead.FirstName,
                        LastName = team.TeamLead.LastName,
                        Email = team.TeamLead.Email,
                        Roles = team.TeamLead.Roles,
                    },
                }
            ).ToListAsync()
        );
    }
    
    
    [HttpGet("{id}")]//EROARE
    public async Task<ActionResult<TeamRequest>> GetById([FromRoute] string id)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(entity => entity.Id == id);
        if (team == null)
        {
            return NotFound("Team not found");
        } 
        
        return Ok(
            new TeamResponse
            {
                Id = team.Id,
                //TeamLead = team.TeamLead,
                Name = team.Name,
                GitHubLink = team.GitHubLink,
                TeamLead = new UserResponse
                {
                    Id = team.TeamLead.Id,
                    FirstName = team.TeamLead.FirstName,
                    LastName = team.TeamLead.LastName,
                    Email = team.TeamLead.Email,
                    Roles = team.TeamLead.Roles,
                },
            }
        );
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Team>> DeleteById([FromRoute] string id)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(entity => entity.Id == id);
        if (team == null)
        {
            return NotFound("Team not found");
        }

        _dbContext.Teams.Remove(team);
        await _dbContext.SaveChangesAsync();

        return Ok(team);
    }

    
    [HttpPut("{id}")]
    public async Task<ActionResult<TeamRequest>> UpdateById([FromRoute] string id, [FromBody] TeamRequest teamRequest)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(entity => entity.Id == id);
        if (team == null)
        {
            return NotFound("Team not found");
        }
        
        var teamLead = await _dbContext.Users.FirstOrDefaultAsync(element => element.Id == teamRequest.TeamLeadId);
        if (teamLead == null)
        {
            return NotFound("Team leader not found");
        }

        team.Name = teamRequest.Name;
        team.GitHubLink = teamRequest.GitHubLink;
        team.TeamLead = teamLead;
        team.Updated = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();

        return Ok(
            new TeamResponse
            {
                Id = team.Id,
                //TeamLead = team.TeamLead,
                Name = team.Name,
                GitHubLink = team.GitHubLink,
                TeamLead = new UserResponse
                {
                    Id = team.TeamLead.Id,
                    FirstName = team.TeamLead.FirstName,
                    LastName = team.TeamLead.LastName,
                    Email = team.TeamLead.Email,
                    Roles = team.TeamLead.Roles,
                },
            }
        );
    }
    
    
    [HttpPut("change_TeamLead/{id}/{teamLeadId}")] //schimbi team leader-ul cu un user existent deja
    public async Task<ActionResult<TeamRequest>> ChangeById([FromRoute] string id, [FromRoute] string teamLeadId)
        //[FromBody] TeamRequest teamRequest)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(entity => entity.Id == id);
        if (team == null) 
        {
            return NotFound("Team not found");
        }

        var teamLead = await _dbContext.Users.FirstOrDefaultAsync(element => element.Id == teamLeadId);
        if (teamLead == null)
        {
            return NotFound("Team leader not found");
        }
        
        team.TeamLead = teamLead;
        
        /*
        team.TeamLead.FirstName = teamLead.FirstName;
        team.TeamLead.LastName = teamLead.LastName;
        team.TeamLead.Email = teamLead.Email;
        team.TeamLead.Roles = teamLead.Roles;
        team.TeamLead.Updated = DateTime.UtcNow;
        */
        
        await _dbContext.SaveChangesAsync();
        
        return Ok(
            new TeamResponse
            {
                Id = team.Id,
                //TeamLead = team.TeamLead,
                Name = team.Name,
                GitHubLink = team.GitHubLink,
                TeamLead = new UserResponse
                {
                    Id = team.TeamLead.Id,
                    FirstName = team.TeamLead.FirstName,
                    LastName = team.TeamLead.LastName,
                    Email = team.TeamLead.Email,
                    Roles = team.TeamLead.Roles,
                },
            }
        );

    }
    
}