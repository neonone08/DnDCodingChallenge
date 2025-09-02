using Common.Models;
using Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombatController : ControllerBase
    {
        private readonly ILogger<CombatController> _logger;
        private readonly AppDbContext _context;
        private readonly IDamageService _damageService;
        public CombatController(ILogger<CombatController> logger, AppDbContext context, IDamageService damageService)
        {
            _logger = logger;
            _context = context;
            _damageService = damageService;
        }

        [HttpPost("deal-damage")]
        public async Task<IActionResult> DealDamage(DealDamageRequestDTO request)
        {
            var character = await _context.Characters
                .Include(c => c.Classes)
                .Include(c => c.Defenses)
                .Include(c => c.Stats)
                .FirstOrDefaultAsync(c => c.Name == request.Name);

            if (character == null) return NotFound();

            try
            {
                int damage = _damageService.CalculateDamage(character, request.DamageType, request.Amount);
                // Apply damage to temporary hit points first
                if (character.TempHitPoints > 0)
                {
                    if (damage <= character.TempHitPoints)
                    {
                        character.TempHitPoints -= damage;
                        damage = 0;
                    }
                    else
                    {
                        damage -= character.TempHitPoints;
                        character.TempHitPoints = 0;
                    }
                }
                // Apply remaining damage to actual hit points
                character.CurrentHitPoints = Math.Max(0, character.CurrentHitPoints - damage);

                await _context.SaveChangesAsync();
                return Ok(character);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("heal")]
        public async Task<IActionResult> Heal(HealRequestDTO request)
        {
            var character = await _context.Characters.Include(c => c.Stats).Include(c => c.Classes).FirstOrDefaultAsync(c => c.Name == request.Name);
            if (character == null) return NotFound();

            character.CurrentHitPoints = (character.CurrentHitPoints + request.Amount) > character.HitPoints ? character.HitPoints : character.CurrentHitPoints + request.Amount;

            await _context.SaveChangesAsync();
            return Ok(character);
        }

        [HttpPost("temp-hp")]
        public async Task<IActionResult> AddTempHp(AddTempHpRequestDTO request)
        {
            var character = await _context.Characters.Include(c => c.Stats).Include(c => c.Classes).FirstOrDefaultAsync(c => c.Name == request.Name);
            if (character == null) return NotFound();

            // not additive, always take the higher value
            character.TempHitPoints = Math.Max(character.TempHitPoints, request.Amount);

            await _context.SaveChangesAsync();
            return Ok(character);
        }
    }
}
