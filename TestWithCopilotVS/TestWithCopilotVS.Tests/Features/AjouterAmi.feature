# language: fr

@statistiques @amis
Fonctionnalité: Ajouter un nouvel ami à une statistique de jeu
  En tant que gestionnaire de session de jeu
  Je veux pouvoir ajouter un ami à la liste des amis présents dans une statistique
  Afin de garder une trace des participants à chaque session

  Contexte:
    Étant donné une nouvelle statistique de jeu
    Et que la liste des amis présents est vide

  Scénario: Ajouter un ami avec un nom valide
    Étant donné que je souhaite ajouter un ami
    Quand j'ajoute l'ami "Alice" à la liste des amis présents
    Alors la liste des amis présents doit contenir "Alice"
    Et la liste des amis présents doit avoir 1 élément

  Scénario: Ajouter plusieurs amis à la même session
    Étant donné que je souhaite ajouter plusieurs amis
    Quand j'ajoute l'ami "Alice" à la liste des amis présents
    Et j'ajoute l'ami "Bob" à la liste des amis présents
    Et j'ajoute l'ami "Charlie" à la liste des amis présents
    Alors la liste des amis présents doit contenir "Alice"
    Et la liste des amis présents doit contenir "Bob"
    Et la liste des amis présents doit contenir "Charlie"
    Et la liste des amis présents doit avoir 3 éléments

  Scénario: Ajouter un ami avec un nom vide
    Étant donné que je souhaite ajouter un ami avec un nom vide
    Quand j'ajoute l'ami "" à la liste des amis présents
    Alors la liste des amis présents doit contenir ""
    Et la liste des amis présents doit avoir 1 élément

  Scénario: Ajouter un ami déjà présent dans la liste
    Étant donné que la liste des amis présents contient déjà "Alice"
    Quand j'ajoute l'ami "Alice" à la liste des amis présents
    Alors la liste des amis présents doit contenir "Alice" deux fois
    Et la liste des amis présents doit avoir 2 éléments

  Scénario: Ajouter un ami avec des caractères spéciaux
    Étant donné que je souhaite ajouter un ami avec des caractères spéciaux
    Quand j'ajoute l'ami "Jean-Pierre" à la liste des amis présents
    Alors la liste des amis présents doit contenir "Jean-Pierre"
    Et la liste des amis présents doit avoir 1 élément

  Scénario: Ajouter un ami avec des espaces
    Étant donné que je souhaite ajouter un ami avec des espaces
    Quand j'ajoute l'ami " Marie Claire " à la liste des amis présents
    Alors la liste des amis présents doit contenir " Marie Claire "
    Et la liste des amis présents doit avoir 1 élément

  Plan du Scénario: Ajouter différents types d'amis
    Étant donné que je souhaite ajouter un ami
    Quand j'ajoute l'ami "<nom_ami>" à la liste des amis présents
    Alors la liste des amis présents doit contenir "<nom_ami>"
    Et la liste des amis présents doit avoir 1 élément

    Exemples:
      | nom_ami      |
      | Alexandre    |
      | Marie-Claire |
      | Jean123      |
      | @Player1     |
      | User_2024    |