# GitHub Copilot - Instructions du Projet

## Vue d'ensemble du projet

Ce projet est une API .NET 9 qui gère des sessions de jeu entre amis. Il utilise ASP.NET Core avec des endpoints qui utilise la Minimal API comme architecture et Swagger ne doit pas être utilisé pour la documentation API : utilisation de Scalar.

## Guidelines pour l'utilisation de GitHub Copilot

### 1. Standards de code .NET

- **Conventions de nommage** : Suivez les conventions C# standard (PascalCase pour les classes et méthodes, camelCase pour les variables locales)
- **Documentation XML** : Ajoutez des commentaires XML pour toutes les méthodes publiques
- **Async/await** : Utilisez les patterns asynchrones pour les opérations I/O
- **Injection de dépendances** : Respectez le pattern DI d'ASP.NET Core

### 2. Architecture du projet

**RÈGLE ESSENTIELLE : Architecture en couches avec projets séparés**

- **Projet API** (`TestWithCopilotVS`) : Contient uniquement les endpoints, la configuration et Program.cs
- **Projet Models** (`TestWithCopilotVS.Models`) : Contient toutes les entités/modèles (ex: `Friend.cs`, `Statistique.cs`)
- **Projet Repositories** (`TestWithCopilotVS.Repositories`) : Contient tous les repositories et interfaces (ex: `IFriendRepository.cs`, `FriendRepository.cs`)
- **Projet Tests** (`TestWithCopilotVS.Tests`) : Contient tous les tests unitaires et BDD

**Détails par couche :**
- **Endpoints** : Utilisez les minimal APIs d'ASP.NET Core dans le projet principal
- **Modèles** : Une seule définition par entité, dans le projet `.Models` uniquement
- **Repositories** : Pattern Repository avec interfaces dans le projet `.Repositories`
- **Extensions** : Méthodes d'extension pour les services dans le projet principal (ex: `CustomCorsExtensions.cs`)

**Références entre projets :**
- API → Models + Repositories
- Repositories → Models  
- Tests → Models + Repositories (+ API si nécessaire pour les tests d'intégration)

### 3. Bonnes pratiques avec Copilot

- **Validation** : Toujours valider les suggestions de Copilot avant de les accepter, attendre un GOO pour accepter
- **Tests** : Demandez à Copilot de générer des tests unitaires pour le nouveau code, en mode TDD
- **Architecture** : Respectez IMPÉRATIVEMENT la séparation en couches - ne jamais dupliquer les modèles
- **Sécurité** : Vérifiez que les suggestions respectent les bonnes pratiques de sécurité
- **Performance** : Optimisez les requêtes et utilisez les bonnes pratiques .NET

**Règles strictes d'architecture :**
- ❌ **INTERDIT** : Dupliquer des classes/modèles dans plusieurs projets
- ❌ **INTERDIT** : Mettre des repositories dans le projet API principal
- ✅ **OBLIGATOIRE** : Une seule source de vérité par entité dans `.Models`
- ✅ **OBLIGATOIRE** : Tous les repositories dans `.Repositories`
- ✅ **OBLIGATOIRE** : Respecter les dépendances entre couches

### 4. Prompts efficaces

Exemples de prompts à utiliser avec Copilot Chat :

```
// Pour créer un endpoint (dans le projet API)
"Crée un endpoint GET pour récupérer tous les amis avec pagination"

// Pour créer un modèle (dans le projet .Models)
"Crée une nouvelle entité Product dans le projet TestWithCopilotVS.Models"

// Pour créer un repository (dans le projet .Repositories)
"Crée IProductRepository et ProductRepository dans le projet TestWithCopilotVS.Repositories"

// Pour ajouter de la validation
"Ajoute la validation des données d'entrée avec des annotations dans le modèle"

// Pour les tests
"Génère des tests unitaires pour la classe ProductRepository dans le projet .Tests"

// Pour la documentation
"Ajoute des commentaires XML pour cette méthode"
```

**Prompts pour respecter l'architecture :**
```
// Vérification avant création
"Vérifie que cette classe n'existe pas déjà dans un autre projet"

// Migration de code mal placé
"Déplace cette classe vers le bon projet selon l'architecture en couches"
```


### 5. Processus complet de développement d'une feature

**Workflow GitHub Issues → Branche → Code → Tests → Pull Request**

#### Étape 1 : Création de l'issue GitHub
1. **Demander à l'utilisateur** :
   - L'intitulé de la feature (titre clair et descriptif)
   - La description détaillée de la fonctionnalité souhaitée
   
2. **Créer l'issue** avec :
   - Titre : `[FEATURE] {intitulé de la feature}`
   - Description complète des besoins fonctionnels
   - Labels appropriés (`enhancement`, `feature`)
   - Assignation si nécessaire

#### Étape 2 : Création de la branche dédiée
```bash
# Créer et basculer sur une nouvelle branche
git checkout -b feat/issue-{numéro-issue}-{nom-feature-court}
```

**Convention de nommage des branches :**
- `feat/issue-{numéro}-{description-courte}`
- Exemple : `feat/issue-42-user-authentication`

#### Étape 3 : Développement de la fonctionnalité
**Ordre de développement obligatoire :**

1. **Modèles** (dans `TestWithCopilotVS.Models`) :
   - Créer/modifier les entités nécessaires
   - Ajouter les annotations de validation

2. **Interfaces Repository** (dans `TestWithCopilotVS.Repositories/Interfaces`) :
   - Définir les contrats des repositories

3. **Implémentations Repository** (dans `TestWithCopilotVS.Repositories`) :
   - Implémenter les repositories avec la logique métier

4. **Endpoints** (dans `TestWithCopilotVS`) :
   - Créer les endpoints Minimal API
   - Configurer l'injection de dépendances

5. **Extensions** (si nécessaire) :
   - Ajouter les extensions de services

#### Étape 4 : Tests unitaires (mode TDD)
**Implémentation obligatoire des tests dans `TestWithCopilotVS.Tests` :**

1. **Tests des modèles** :
   - Validation des propriétés
   - Tests des annotations

2. **Tests des repositories** :
   - Tests unitaires complets
   - Mocking des dépendances

3. **Tests des endpoints** :
   - Tests d'intégration
   - Tests des codes de retour HTTP

4. **Tests BDD** (si applicable) :
   - Scénarios SpecFlow dans `Features/`
   - Step definitions dans `StepDefinitions/`

#### Étape 5 : Commits et Push
**Structure des commits :**
```bash
# Commit initial du modèle
git add .
git commit -m "feat(models): add {EntityName} model for issue #{numéro}"

# Commit des repositories
git commit -m "feat(repositories): implement {EntityName}Repository for issue #{numéro}"

# Commit des endpoints
git commit -m "feat(api): add {feature} endpoints for issue #{numéro}"

# Commit des tests
git commit -m "test: add unit tests for {feature} - issue #{numéro}"

# Push vers GitHub
git push origin feat/issue-{numéro}-{nom-feature-court}
```

#### Étape 6 : Création de la Pull Request
**Éléments obligatoires de la PR :**

1. **Titre** : `[FEATURE] {Titre de la feature} - Close #{numéro-issue}`

2. **Description** :
   ```markdown
   ## 🚀 Feature Description
   [Description de la fonctionnalité]
   
   ## 📋 Changes Made
   - [ ] Modèles ajoutés/modifiés
   - [ ] Repositories implémentés  
   - [ ] Endpoints créés
   - [ ] Tests unitaires ajoutés
   - [ ] Documentation mise à jour
   
   ## 🔗 Related Issue
   Closes #{numéro-issue}
   
   ## 🧪 Testing
   - [ ] Tests unitaires passent
   - [ ] Tests d'intégration passent
   - [ ] Tests manuels effectués
   
   ## 📸 Screenshots (si applicable)
   [Screenshots de l'API ou des résultats]
   ```

3. **Configuration** :
   - Lier à l'issue avec `Closes #{numéro-issue}`
   - Assigner des reviewers
   - Ajouter des labels appropriés
   - Demander une review avant merge

**Règles de merge :**
- ✅ Tous les tests doivent passer
- ✅ Code review approuvé
- ✅ Pas de conflits avec `main`
- ✅ Architecture respectée (couches séparées)

### 6. Gestion des branches et structure des commits

- **Avant chaque nouvelle feature**, suivez le processus complet décrit ci-dessus
- Utilisez des messages de commit clairs en utilisant le conventional commit messages (Conventional Commits) :
	- `chore:` pour les tâches de maintenance et de construction d'architecture globale
	- `feat:` pour les nouvelles fonctionnalités
	- `fix:` pour les corrections de bugs
	- `docs:` pour la documentation
	- `refactor:` pour le refactoring
	- `test:` pour les tests

### 7. Révision du code

- **Toujours réviser** le code généré par Copilot
- **Tester** les fonctionnalités avant de commiter
- **Documenter** les choix d'implémentation non évidents
- **Optimiser** si nécessaire

## Ressources du projet

- **Framework** : .NET 9 / ASP.NET Core
- **Documentation API** : Scalar (pas Swagger)
- **Architecture** : Clean Architecture avec couches séparées
- **Pattern** : Repository pattern, Minimal APIs
- **Configuration** : appsettings.json pour les environnements

## Structure des projets

```
TestWithCopilotVS/                          # 🚀 Projet API principal
├── Program.cs                              # Point d'entrée et configuration
├── Endpoints/                              # Endpoints groupés par fonctionnalité
├── Extensions/                             # Extensions de services
└── appsettings.json                        # Configuration

TestWithCopilotVS.Models/                   # 📋 Modèles/Entités
├── Friend.cs                               # ✅ Seule définition de Friend
├── Statistique.cs                          # ✅ Seule définition de Statistique
└── [Autres entités...]

TestWithCopilotVS.Repositories/             # 🗃️ Couche d'accès aux données
├── Interfaces/                             # Interfaces des repositories
│   ├── IFriendRepository.cs
│   └── IStatistiqueRepository.cs
├── FriendRepository.cs                     # Implémentations
├── StatistiqueRepository.cs
└── [Autres repositories...]

TestWithCopilotVS.Tests/                    # 🧪 Tous les tests
├── UnitTests/                              # Tests unitaires
├── BddTests/                               # Tests BDD/SpecFlow
│   ├── Features/
│   └── StepDefinitions/
└── IntegrationTests/                       # Tests d'intégration
```

---

*Dernière mise à jour : Septembre 2025*
