using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
         private readonly AppDbContext _db;

    // Inject AppDbContext through DI
    public CharactersController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/characters
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        var characters = await _db.Characters
            .Include(c => c.Classes)
            .Include(c => c.Items).ThenInclude(i => i.Modifier)
            .Include(c => c.Defenses)
            .ToListAsync();

        return Ok(characters);
    }

    // GET: api/characters/Briv
    [HttpGet("{name}")]
    public async Task<ActionResult<Character>> GetCharacter(string name)
    {
        var character = await _db.Characters
            .Include(c => c.Classes)
            .Include(c => c.Items).ThenInclude(i => i.Modifier)
            .Include(c => c.Defenses)
            .Include(c => c.Stats)
            .FirstOrDefaultAsync(c => c.Name == name);

        if (character == null)
            return NotFound();

        return Ok(character);
    }

    // POST: api/characters
    [HttpPost]
    public async Task<ActionResult<Character>> AddCharacter(Character character)
    {
        _db.Characters.Add(character);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCharacter), new { name = character.Name }, character);
    }
    }
}
