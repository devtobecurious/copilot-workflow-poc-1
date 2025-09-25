using TestWithCopilotVS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomCors(builder.Configuration);
// Dépôt des amis en mémoire (Singleton)
builder.Services.AddSingleton<IFriendRepository, InMemoryFriendRepository>();
// Dépôt des statistiques en mémoire (Singleton)
builder.Services.AddSingleton<StatistiqueRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CustomCorsExtensions.CorsPolicyName);

// Enregistrement des endpoints via méthode d'extension

app.MapGameSessionEndpoints();
app.MapFriendEndpoints();
app.MapStatistiqueEndpoints();

app.Run();

// Méthode d'extension pour la configuration CORS

