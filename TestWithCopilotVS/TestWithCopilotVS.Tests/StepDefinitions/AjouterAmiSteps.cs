using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;
using TestWithCopilotVS.Models;

namespace TestWithCopilotVS.Tests.StepDefinitions
{
    [Binding]
    public class AjouterAmiSteps
    {
        private Statistique _statistique;
        private Exception _exception;

        [Given(@"une nouvelle statistique de jeu")]
        public void GivenUneNouvelleStatistiqueDeJeu()
        {
            _statistique = new Statistique();
        }

        [Given(@"que la liste des amis présents est vide")]
        [Given(@"la liste des amis présents est vide")]
        public void GivenQueLaListeDesAmisPresentsEstVide()
        {
            _statistique.AmisPresents.Clear();
        }

        [Given(@"que je souhaite ajouter un ami")]
        [Given(@"je souhaite ajouter un ami")]
        public void GivenQueJeSouhaiteAjouterUnAmi()
        {
            // Étape préparatoire - rien à faire
        }

        [Given(@"que je souhaite ajouter plusieurs amis")]
        [Given(@"je souhaite ajouter plusieurs amis")]
        public void GivenQueJeSouhaiteAjouterPlusieursAmis()
        {
            // Étape préparatoire - rien à faire
        }

        [Given(@"que je souhaite ajouter un ami avec un nom vide")]
        [Given(@"je souhaite ajouter un ami avec un nom vide")]
        public void GivenQueJeSouhaiteAjouterUnAmiAvecUnNomVide()
        {
            // Étape préparatoire - rien à faire
        }

        [Given(@"que la liste des amis présents contient déjà ""(.*)""")]
        [Given(@"la liste des amis présents contient déjà ""(.*)""")]
        public void GivenQueLaListeDesAmisPresentsContientDeja(string nomAmi)
        {
            _statistique.AmisPresents.Add(nomAmi);
        }

        [Given(@"que je souhaite ajouter un ami avec des caractères spéciaux")]
        [Given(@"je souhaite ajouter un ami avec des caractères spéciaux")]
        public void GivenQueJeSouhaiteAjouterUnAmiAvecDesCaracteresSpeciaux()
        {
            // Étape préparatoire - rien à faire
        }

        [Given(@"que je souhaite ajouter un ami avec des espaces")]
        [Given(@"je souhaite ajouter un ami avec des espaces")]
        public void GivenQueJeSouhaiteAjouterUnAmiAvecDesEspaces()
        {
            // Étape préparatoire - rien à faire
        }

        [When(@"j'ajoute l'ami ""(.*)"" à la liste des amis présents")]
        public void WhenJAjouteLAmiALaListeDesAmisPresents(string nomAmi)
        {
            try
            {
                _statistique.AmisPresents.Add(nomAmi);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"la liste des amis présents doit contenir ""(.*)""")]
        public void ThenLaListeDesAmisPresentsDitContenir(string nomAmi)
        {
            Assert.Contains(nomAmi, _statistique.AmisPresents);
        }

        [Then(@"la liste des amis présents doit avoir (\d+) élément")]
        [Then(@"la liste des amis présents doit avoir (\d+) éléments")]
        public void ThenLaListeDesAmisPresentsDitAvoir(int nombreElements)
        {
            Assert.Equal(nombreElements, _statistique.AmisPresents.Count);
        }

        [Then(@"la liste des amis présents doit contenir ""(.*)"" deux fois")]
        public void ThenLaListeDesAmisPresentsDitContenirDeuxFois(string nomAmi)
        {
            var count = _statistique.AmisPresents.Count(ami => ami == nomAmi);
            Assert.Equal(2, count);
        }
    }
}