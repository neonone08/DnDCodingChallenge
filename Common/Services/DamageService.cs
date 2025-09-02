using System;
using Common.Models;
using Common.Services.Interfaces;

namespace Common.Services;

public class DamageService: IDamageService
{
    public int CalculateDamage(Character character, string damageType, int incomingDamage)
    {
         if (!DamageTypes.ValidTypes.Contains(damageType))
        {
            throw new ArgumentException($"Invalid damage type: {damageType}. Allowed types are: {string.Join(", ", DamageTypes.ValidTypes)}");
        }

        var defense = character.Defenses?.FirstOrDefault(d =>
            d.Type.Equals(damageType, StringComparison.OrdinalIgnoreCase));

        if (defense != null)
        {
            if (defense.Defense.Equals("immunity", StringComparison.OrdinalIgnoreCase))
            {
                return 0; // immune
            }
            else if (defense.Defense.Equals("resistance", StringComparison.OrdinalIgnoreCase))
            {
                return incomingDamage / 2; // resistance halves damage
            }
        }

        return incomingDamage;
    }
}
