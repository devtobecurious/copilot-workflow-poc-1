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

- **Endpoints** : Utilisez les minimal APIs d'ASP.NET Core
- **Modèles** : Créez des classes séparées pour les entités (ex: `Friend.cs`)
- **Repositories** : Implémentez le pattern Repository pour l'accès aux données
- **Extensions** : Utilisez des méthodes d'extension pour configurer les services (ex: `CustomCorsExtensions.cs`)

### 3. Bonnes pratiques avec Copilot

- **Validation** : Toujours valider les suggestions de Copilot avant de les accepter, attendre un GOO pour accepter
- **Tests** : Demandez à Copilot de générer des tests unitaires pour le nouveau code, en mode TDD
- **Sécurité** : Vérifiez que les suggestions respectent les bonnes pratiques de sécurité
- **Performance** : Optimisez les requêtes et utilisez les bonnes pratiques .NET

### 4. Prompts efficaces

Exemples de prompts à utiliser avec Copilot Chat :

```
// Pour créer un endpoint
"Crée un endpoint GET pour récupérer tous les amis avec pagination"

// Pour ajouter de la validation
"Ajoute la validation des données d'entrée avec des annotations"

// Pour les tests
"Génère des tests unitaires pour la classe FriendRepository"

// Pour la documentation
"Ajoute des commentaires XML pour cette méthode"
```

### 5. Structure des commits

Utilisez des messages de commit clairs en utilisant le conventional commit messages (Conventional Commits) :
- `chore:` pour les tâches de maintenance et de construction d'architecture globale
- `feat:` pour les nouvelles fonctionnalités
- `fix:` pour les corrections de bugs
- `docs:` pour la documentation
- `refactor:` pour le refactoring
- `test:` pour les tests

### 6. Révision du code

- **Toujours réviser** le code généré par Copilot
- **Tester** les fonctionnalités avant de commiter
- **Documenter** les choix d'implémentation non évidents
- **Optimiser** si nécessaire

## Ressources du projet

- **Framework** : .NET 9 / ASP.NET Core
- **Documentation API** : Swagger/OpenAPI
- **Pattern** : Repository pattern, Minimal APIs
- **Configuration** : appsettings.json pour les environnements

---

*Dernière mise à jour : Septembre 2025*
