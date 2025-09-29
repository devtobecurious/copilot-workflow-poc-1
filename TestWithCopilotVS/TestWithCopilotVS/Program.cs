using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;
using TestWithCopilotVS.Repositories;
using TestWithCopilotVS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomCors(builder.Configuration);

// Dépôts en mémoire (Singleton)
builder.Services.AddSingleton<IFriendRepository, InMemoryFriendRepository>();
builder.Services.AddSingleton<IStatistiqueRepository, InMemoryStatistiqueRepository>();

// Nouveaux dépôts pour les sessions et amis secondaires
builder.Services.AddSingleton<IGameSessionRepository, InMemoryGameSessionRepository>();
builder.Services.AddSingleton<ISessionFriendRepository, InMemorySessionFriendRepository>();
builder.Services.AddSingleton<IFriendInvitationRepository, InMemoryFriendInvitationRepository>();

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

// Nouveaux endpoints pour les sessions et amis secondaires
app.MapSessionEndpoints();
app.MapSessionFriendEndpoints();

app.Run();

// Méthode d'extension pour la configuration CORS

