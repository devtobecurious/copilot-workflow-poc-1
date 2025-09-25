using System.Collections.Generic;
using System.Linq;

namespace TestWithCopilotVS.TestWithCopilotVS
{
    /// <summary>
    /// Repository en mémoire pour la gestion des statistiques de jeu.
    /// </summary>
    public class StatistiqueRepository
    {
        private readonly List<Statistique> _stats = new();
        private int _nextId = 1;

        /// <summary>
        /// Récupère toutes les statistiques.
        /// </summary>
        public IEnumerable<Statistique> GetAll() => _stats;

        /// <summary>
        /// Ajoute une nouvelle statistique.
        /// </summary>
        public Statistique Add(Statistique stat)
        {
            stat.Id = _nextId++;
            _stats.Add(stat);
            return stat;
        }

        /// <summary>
        /// Récupère une statistique par son identifiant.
        /// </summary>
        public Statistique? GetById(int id) => _stats.FirstOrDefault(s => s.Id == id);

        /// <summary>
        /// Supprime une statistique par son identifiant.
        /// </summary>
        public bool Delete(int id)
        {
            var stat = GetById(id);
            if (stat == null) return false;
            _stats.Remove(stat);
            return true;
        }
    }
}
