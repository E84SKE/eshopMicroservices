
var builder = WebApplication.CreateBuilder(args);

// services 

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    // fallback sicuro: se manca, prova comunque su catalogdb:5432 (docker)
    connectionString = "Host=catalogdb;Port=15432;Database=catalogdb;Username=postgres;Password=postgres";
}
Console.WriteLine($"[INFO] Using connection string: {connectionString}");




builder.Services.AddCarter();
builder.Services.AddMediatR(config =>

{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
// Configura Marten
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    // opzionale: crea automaticamente schema se mancano tabelle

}).UseLightweightSessions();

var app = builder.Build();




app.MapCarter();


app.Run();


