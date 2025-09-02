using Common.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterClass> Classes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Modifier> Modifiers { get; set; }
    public DbSet<CharDefense> Defenses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Character ↔ Stats (1:1)
        modelBuilder.Entity<Character>()
            .HasOne(c => c.Stats)
            .WithOne(s => s.Character)
            .HasForeignKey<Stats>(s => s.CharacterId);

        // Character ↔ Classes (1:many)
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Classes)
            .WithOne(cl => cl.Character)
            .HasForeignKey(cl => cl.CharacterId);

        // Character ↔ Items (1:many)
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Items)
            .WithOne(i => i.Character)
            .HasForeignKey(i => i.CharacterId);

        // Item ↔ Modifier (1:1)
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Modifier)
            .WithOne(m => m.Item)
            .HasForeignKey<Modifier>(m => m.ItemId);

        // Character ↔ Defenses (1:many)
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Defenses)
            .WithOne(d => d.Character)
            .HasForeignKey(d => d.CharacterId);
    }
}
