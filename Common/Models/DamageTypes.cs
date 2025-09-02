using System;

namespace Common.Models;

public static class DamageTypes
{
    public static readonly HashSet<string> ValidTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "Bludgeoning",
        "Piercing",
        "Slashing",
        "Fire",
        "Cold",
        "Acid",
        "Thunder",
        "Lightning",
        "Poison",
        "Radiant",
        "Necrotic",
        "Psychic",
        "Force"
    };
}
