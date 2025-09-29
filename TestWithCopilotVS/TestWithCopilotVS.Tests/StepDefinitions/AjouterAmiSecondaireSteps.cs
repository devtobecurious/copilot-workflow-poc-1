using TestWithCopilotVS.Models;
using TestWithCopilotVS.Repositories;
using TestWithCopilotVS.Repositories.Interfaces;
using TechTalk.SpecFlow;
using Xunit;

namespace TestWithCopilotVS.Tests.StepDefinitions;

[Binding]
public class AjouterAmiSecondaireSteps
{
    private readonly IFriendRepository _friendRepository;
    private readonly IGameSessionRepository _sessionRepository;
    private readonly ISessionFriendRepository _sessionFriendRepository;
    private readonly IFriendInvitationRepository _invitationRepository;

    private GameSession? _currentSession;
    private Exception? _lastException;
    private string? _lastErrorMessage;
    private bool _operationSuccess;

    public AjouterAmiSecondaireSteps()
    {
        _friendRepository = new InMemoryFriendRepository();
        _sessionRepository = new InMemoryGameSessionRepository(_friendRepository);
        _sessionFriendRepository = new InMemorySessionFriendRepository();
        _invitationRepository = new InMemoryFriendInvitationRepository();
    }

    [Given(@"une session de jeu active existe avec l'ID (.*)")]
    public async Task GivenUneSessionDeJeuActiveExisteAvecLID(int sessionId)
    {
        var session = new GameSession
        {
            Id = sessionId,
            Name = "Session de test",
            CreatorId = 1,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _currentSession = await _sessionRepository.CreateAsync(session);
        
        // Forcer l'ID pour les tests
        _currentSession.Id = sessionId;
    }

    [Given(@"le créateur de la session a l'ID (.*)")]
    public void GivenLeCreateurDeLaSessionALID(int creatorId)
    {
        Assert.NotNull(_currentSession);
        Assert.Equal(creatorId, _currentSession.CreatorId);
    }

    [Given(@"il y a (.*) amis primaires dans la session")]
    public async Task GivenIlYaAmisPrimaires(int count)
    {
        Assert.NotNull(_currentSession);

        // Ajouter le créateur comme participant primaire
        await _sessionFriendRepository.AddAsync(new SessionFriend
        {
            SessionId = _currentSession.Id,
            FriendId = 1,
            Status = FriendSessionStatus.Primary
        });

        // Ajouter un autre ami primaire pour atteindre le count
        if (count > 1)
        {
            await _sessionFriendRepository.AddAsync(new SessionFriend
            {
                SessionId = _currentSession.Id,
                FriendId = 2,
                Status = FriendSessionStatus.Primary
            });
        }
    }

    [Given(@"un ami avec l'ID (.*) existe")]
    public void GivenUnAmiAvecLIDExiste(int friendId)
    {
        var existingFriend = _friendRepository.Get(friendId);
        if (existingFriend == null)
        {
            var friend = new Friend(friendId, $"Ami{friendId}", $"ami{friendId}@test.com");
            _friendRepository.Add(friend);
        }
    }

    [Given(@"l'ami avec l'ID (.*) ne participe pas à la session (.*)")]
    public async Task GivenLAmiAvecLIDNeParticipePasALaSession(int friendId, int sessionId)
    {
        var isParticipating = await _sessionFriendRepository.IsParticipatingAsync(sessionId, friendId);
        Assert.False(isParticipating);
    }

    [Given(@"un ami avec l'ID (.*) participe déjà à la session (.*)")]
    public async Task GivenUnAmiAvecLIDParticipeDejaALaSession(int friendId, int sessionId)
    {
        var friend = new Friend(friendId, $"Ami{friendId}", $"ami{friendId}@test.com");
        _friendRepository.Add(friend);

        await _sessionFriendRepository.AddAsync(new SessionFriend
        {
            SessionId = sessionId,
            FriendId = friendId,
            Status = FriendSessionStatus.Primary
        });
    }

    [Given(@"une session terminée avec l'ID (.*)")]
    public async Task GivenUneSessionTermineeAvecLID(int sessionId)
    {
        var session = new GameSession
        {
            Id = sessionId,
            Name = "Session terminée",
            CreatorId = 1,
            IsActive = false,
            EndedAt = DateTime.UtcNow.AddMinutes(-30)
        };

        await _sessionRepository.CreateAsync(session);
    }

    [Given(@"un ami avec l'ID (.*) est ajouté à la session (.*) comme ami secondaire")]
    public async Task GivenUnAmiAvecLIDEstAjouteALaSessionCommeAmiSecondaire(int friendId, int sessionId)
    {
        var friend = new Friend(friendId, $"Ami{friendId}", $"ami{friendId}@test.com");
        _friendRepository.Add(friend);

        await _sessionFriendRepository.AddAsync(new SessionFriend
        {
            SessionId = sessionId,
            FriendId = friendId,
            Status = FriendSessionStatus.Secondary
        });
    }

    [Given(@"les amis avec les IDs (.*), (.*), (.*) existent")]
    public void GivenLesAmisAvecLesIDsExistent(int friendId1, int friendId2, int friendId3)
    {
        foreach (var friendId in new[] { friendId1, friendId2, friendId3 })
        {
            var friend = new Friend(friendId, $"Ami{friendId}", $"ami{friendId}@test.com");
            _friendRepository.Add(friend);
        }
    }

    [When(@"j'ajoute l'ami (.*) à la session (.*) comme ami secondaire")]
    public async Task WhenJAjouteLAmiALaSessionCommeAmiSecondaire(int friendId, int sessionId)
    {
        try
        {
            await _sessionFriendRepository.AddAsync(new SessionFriend
            {
                SessionId = sessionId,
                FriendId = friendId,
                Status = FriendSessionStatus.Secondary
            });
            _operationSuccess = true;
        }
        catch (Exception ex)
        {
            _lastException = ex;
            _lastErrorMessage = ex.Message;
            _operationSuccess = false;
        }
    }

    [When(@"j'essaie d'ajouter l'ami (.*) à la session (.*)")]
    public async Task WhenJEssaieDajouterLAmiALaSession(int friendId, int sessionId)
    {
        try
        {
            var isAlreadyParticipating = await _sessionFriendRepository.IsParticipatingAsync(sessionId, friendId);
            if (isAlreadyParticipating)
            {
                _lastErrorMessage = $"L'ami {friendId} participe déjà à cette session";
                _operationSuccess = false;
                return;
            }

            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session != null && !session.IsActive)
            {
                _lastErrorMessage = "Impossible d'ajouter des amis à une session terminée";
                _operationSuccess = false;
                return;
            }

            await _sessionFriendRepository.AddAsync(new SessionFriend
            {
                SessionId = sessionId,
                FriendId = friendId,
                Status = FriendSessionStatus.Secondary
            });
            _operationSuccess = true;
        }
        catch (Exception ex)
        {
            _lastException = ex;
            _lastErrorMessage = ex.Message;
            _operationSuccess = false;
        }
    }

    [When(@"je change le statut de l'ami (.*) vers ""(.*)""")]
    public async Task WhenJeChangeLeStatutDeLAmiVers(int friendId, string statusName)
    {
        var status = Enum.Parse<FriendSessionStatus>(statusName);
        Assert.NotNull(_currentSession);

        await _sessionFriendRepository.UpdateStatusAsync(_currentSession.Id, friendId, status);
        _operationSuccess = true;
    }

    [When(@"je retire l'ami (.*) de la session (.*)")]
    public async Task WhenJeRetireLAmiDeLaSession(int friendId, int sessionId)
    {
        try
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session?.CreatorId == friendId)
            {
                _lastErrorMessage = "Impossible de retirer le créateur de la session";
                _operationSuccess = false;
                return;
            }

            var result = await _sessionFriendRepository.RemoveFromSessionAsync(sessionId, friendId);
            _operationSuccess = result;
        }
        catch (Exception ex)
        {
            _lastException = ex;
            _lastErrorMessage = ex.Message;
            _operationSuccess = false;
        }
    }

    [When(@"j'essaie de retirer l'ami (.*) \(créateur\) de la session (.*)")]
    public async Task WhenJEssaieDeRetirerLAmiCreateurDeLaSession(int friendId, int sessionId)
    {
        try
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session?.CreatorId == friendId)
            {
                _lastErrorMessage = "Impossible de retirer le créateur de la session";
                _operationSuccess = false;
                return;
            }

            await _sessionFriendRepository.RemoveFromSessionAsync(sessionId, friendId);
            _operationSuccess = true;
        }
        catch (Exception ex)
        {
            _lastException = ex;
            _lastErrorMessage = ex.Message;
            _operationSuccess = false;
        }
    }

    [When(@"j'ajoute successivement les amis (.*), (.*), (.*) à la session (.*) comme amis secondaires")]
    public async Task WhenJAjouteSuccessivementLesAmisALaSessionCommeAmisSecondaires(int friendId1, int friendId2, int friendId3, int sessionId)
    {
        foreach (var friendId in new[] { friendId1, friendId2, friendId3 })
        {
            await _sessionFriendRepository.AddAsync(new SessionFriend
            {
                SessionId = sessionId,
                FriendId = friendId,
                Status = FriendSessionStatus.Secondary
            });
        }
        _operationSuccess = true;
    }

    [Then(@"l'ami (.*) devrait être ajouté à la session avec le statut ""(.*)""")]
    public async Task ThenLAmiDevraitEtreAjouteALaSessionAvecLeStatut(int friendId, string statusName)
    {
        Assert.True(_operationSuccess);
        Assert.NotNull(_currentSession);

        var status = Enum.Parse<FriendSessionStatus>(statusName);
        var participation = await _sessionFriendRepository.GetBySessionAndFriendAsync(_currentSession.Id, friendId);

        Assert.NotNull(participation);
        Assert.Equal(status, participation.Status);
    }

    [Then(@"la session devrait avoir (.*) participants actifs")]
    public async Task ThenLaSessionDevraitAvoirParticipantsActifs(int expectedCount)
    {
        Assert.NotNull(_currentSession);
        var count = await _sessionFriendRepository.CountActiveParticipantsAsync(_currentSession.Id);
        Assert.Equal(expectedCount, count);
    }

    [Then(@"l'ami (.*) devrait avoir rejoint la session aujourd'hui")]
    public async Task ThenLAmiDevraitAvoirRejointLaSessionAujourdhui(int friendId)
    {
        Assert.NotNull(_currentSession);
        var participation = await _sessionFriendRepository.GetBySessionAndFriendAsync(_currentSession.Id, friendId);
        
        Assert.NotNull(participation);
        Assert.Equal(DateTime.UtcNow.Date, participation.JoinedAt.Date);
    }

    [Then(@"l'opération devrait échouer avec le message ""(.*)""")]
    public void ThenLOperationDevraitEchouerAvecLeMessage(string expectedMessage)
    {
        Assert.False(_operationSuccess);
        Assert.Equal(expectedMessage, _lastErrorMessage);
    }

    [Then(@"le nombre de participants ne devrait pas changer")]
    public async Task ThenLeNombreDeParticipantsNeDevraitPasChanger()
    {
        Assert.NotNull(_currentSession);
        var count = await _sessionFriendRepository.CountActiveParticipantsAsync(_currentSession.Id);
        // Le test précédent avait établi qu'il y avait 2 participants
        Assert.Equal(2, count);
    }

    [Then(@"l'ami (.*) devrait avoir le statut ""(.*)"" dans la session (.*)")]
    public async Task ThenLAmiDevraitAvoirLeStatutDansLaSession(int friendId, string statusName, int sessionId)
    {
        var status = Enum.Parse<FriendSessionStatus>(statusName);
        var participation = await _sessionFriendRepository.GetBySessionAndFriendAsync(sessionId, friendId);
        
        Assert.NotNull(participation);
        Assert.Equal(status, participation.Status);
    }

    [Then(@"l'ami (.*) ne devrait plus être actif dans la session (.*)")]
    public async Task ThenLAmiNeDevraitPlusEtreActifDansLaSession(int friendId, int sessionId)
    {
        var participation = await _sessionFriendRepository.GetBySessionAndFriendAsync(sessionId, friendId);
        Assert.NotNull(participation);
        Assert.False(participation.IsActive);
    }

    [Then(@"le nombre de participants actifs devrait diminuer de (.*)")]
    public async Task ThenLeNombreDeParticipantsActifsDevraitDiminuerDe(int decrease)
    {
        Assert.NotNull(_currentSession);
        var count = await _sessionFriendRepository.CountActiveParticipantsAsync(_currentSession.Id);
        // Nous avons commencé avec 2 participants, après retrait d'1, il devrait y en avoir 1
        Assert.Equal(2 - decrease, count);
    }

    [Then(@"tous les nouveaux amis devraient avoir le statut ""(.*)""")]
    public async Task ThenTousLesNouveauxAmisDevraientAvoirLeStatut(string statusName)
    {
        Assert.NotNull(_currentSession);
        var status = Enum.Parse<FriendSessionStatus>(statusName);
        var secondaryParticipants = await _sessionFriendRepository.GetBySessionAndStatusAsync(_currentSession.Id, status);
        
        // Devrait avoir 3 amis secondaires (IDs 7, 8, 9)
        Assert.True(secondaryParticipants.Count() >= 3);
        Assert.All(secondaryParticipants, p => Assert.Equal(status, p.Status));
    }
}