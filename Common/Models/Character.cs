using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int HitPoints { get; set; }
    public int CurrentHitPoints { get; set; }
    public int TempHitPoints { get; set; }
    public List<CharacterClass> Classes { get; set; }
    public Stats Stats { get; set; }
    public List<Item> Items { get; set; }
    public List<CharDefense> Defenses { get; set; }
}

public class CharacterClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int HitDiceValue { get; set; }
    public int ClassLevel { get; set; }
    // Foreign Key
    public int CharacterId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Character Character { get; set; }
}

public class Stats
{
    public int Id { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }
    // Foreign Key
    public int CharacterId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Character Character { get; set; }
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Modifier Modifier { get; set; }
    // Foreign Key
    public int CharacterId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Character Character { get; set; }
}

public class Modifier
{
    public int Id { get; set; }
    public string AffectedObject { get; set; }
    public string AffectedValue { get; set; }
    public int Value { get; set; }
    // FK
    public int ItemId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Item Item { get; set; }
}

public class CharDefense
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string? Defense { get; set; }
    // FK
    public int CharacterId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Character Character { get; set; }
}
