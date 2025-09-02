using System;
using Common.Models;

namespace Common.Services.Interfaces;

public interface IDamageService
{
    int CalculateDamage(Character character, string damageType, int incomingDamage);
}
