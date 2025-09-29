using TestWithCopilotVS;
using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories.Interfaces;
using TestWithCopilotVS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomCors(builder.Configuration);

// Dépôts en mémoire (Singleton) - Existants
builder.Services.AddSingleton<IFriendRepository, InMemoryFriendRepository>();
builder.Services.AddSingleton<IStatistiqueRepository, InMemoryStatistiqueRepository>();

// Nouveaux dépôts pour les sessions et amis secondaires - Existants
builder.Services.AddSingleton<IGameSessionRepository, InMemoryGameSessionRepository>();
builder.Services.AddSingleton<ISessionFriendRepository, InMemorySessionFriendRepository>();
builder.Services.AddSingleton<IFriendInvitationRepository, InMemoryFriendInvitationRepository>();

// Nouveaux repositories RPG
builder.Services.AddSingleton<IRpgPlayerRepository, InMemoryRpgPlayerRepository>();
builder.Services.AddSingleton<IRpgCharacterRepository, InMemoryRpgCharacterRepository>();
builder.Services.AddSingleton<IRpgSessionRepository, InMemoryRpgSessionRepository>();

// Repository RPG Campaign avec factory pour éviter la dépendance circulaire
builder.Services.AddSingleton<Func<IRpgPlayerRepository>>(provider => 
    () => provider.GetRequiredService<IRpgPlayerRepository>());
builder.Services.AddSingleton<IRpgCampaignRepository, InMemoryRpgCampaignRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CustomCorsExtensions.CorsPolicyName);

// Enregistrement des endpoints via méthode d'extension - Existants
app.MapFriendEndpoints();
app.MapStatistiqueEndpoints();

// Nouveaux endpoints pour les sessions et amis secondaires - Existants
app.MapSessionEndpoints();
app.MapSessionFriendEndpoints();

// Nouveaux endpoints RPG
app.MapRpgCampaignEndpoints();
app.MapRpgCharacterEndpoints();

app.Run();

// Méthode d'extension pour la configuration CORS

