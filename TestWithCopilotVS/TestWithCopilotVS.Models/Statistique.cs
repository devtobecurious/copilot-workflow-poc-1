using System.Collections.Generic;

namespace TestWithCopilotVS.Models
{
    /// <summary>
    /// Représente une statistique de jeu.
    /// </summary>
    public class Statistique
    {
        /// <summary>
        /// Identifiant unique de la statistique.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de succès dans la session.
        /// </summary>
        public int NbSuccess { get; set; }

        /// <summary>
        /// Liste des amis présents lors de la session.
        /// </summary>
        public List<string> AmisPresents { get; set; } = new();

        /// <summary>
        /// Nom du gagnant de la session.
        /// </summary>
        public string Gagnant { get; set; } = string.Empty;

        /// <summary>
        /// Mois de la session.
        /// </summary>
        public int Mois { get; set; }

        /// <summary>
        /// Année de la session.
        /// </summary>
        public int Annee { get; set; }
    }
}