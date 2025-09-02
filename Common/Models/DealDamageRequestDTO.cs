using System;

namespace Common.Models;

public class DealDamageRequestDTO
{
    public string Name { get; set; }
    public string DamageType { get; set; }
    public int Amount { get; set; }
}
