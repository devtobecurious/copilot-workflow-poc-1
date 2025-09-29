namespace TestWithCopilotVS.Models;

/// <summary>
/// Énumération des classifications ESRB (Entertainment Software Rating Board).
/// </summary>
public enum EsrbRating
{
    /// <summary>
    /// Pas encore classé.
    /// </summary>
    NotRated = 0,

    /// <summary>
    /// EC - Early Childhood (Première enfance) - Contenu adapté aux enfants de 3 ans et plus.
    /// </summary>
    EarlyChildhood = 1,

    /// <summary>
    /// E - Everyone (Tout public) - Contenu généralement adapté à tous les âges.
    /// </summary>
    Everyone = 2,

    /// <summary>
    /// E10+ - Everyone 10+ (Tout public 10+) - Contenu généralement adapté aux 10 ans et plus.
    /// </summary>
    Everyone10Plus = 3,

    /// <summary>
    /// T - Teen (Adolescent) - Contenu généralement adapté aux 13 ans et plus.
    /// </summary>
    Teen = 4,

    /// <summary>
    /// M - Mature 17+ (Mature 17+) - Contenu généralement adapté aux 17 ans et plus.
    /// </summary>
    Mature = 5,

    /// <summary>
    /// AO - Adults Only 18+ (Adultes seulement 18+) - Contenu adapté uniquement aux adultes de 18 ans et plus.
    /// </summary>
    AdultsOnly = 6,

    /// <summary>
    /// RP - Rating Pending (Classification en attente) - Non encore assigné par l'ESRB.
    /// </summary>
    RatingPending = 7
}