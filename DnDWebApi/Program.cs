using Common.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register EF Core InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CharacterDb"));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Register services
builder.Services.AddTransient<Common.Services.Interfaces.IDamageService, Common.Services.DamageService>();
var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Load sample JSON and seed
    var json = File.ReadAllText("briv.json");
    var jsonOptions = new System.Text.Json.JsonSerializerOptions
    {
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
    };
    var character = System.Text.Json.JsonSerializer.Deserialize<Character>(json, options: jsonOptions);
    //initialize current hit points
    character!.CurrentHitPoints = character.HitPoints;
    db.Characters.Add(character!);
    db.SaveChanges();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
