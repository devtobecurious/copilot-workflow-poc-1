Feature: Ajout d'amis secondaires à une session
    En tant que créateur d'une session de jeu
    Je veux pouvoir ajouter des amis secondaires à ma session en cours
    Afin d'agrandir le groupe de participants pendant le jeu

Background:
    Given une session de jeu active existe avec l'ID 1
    And le créateur de la session a l'ID 1
    And il y a 2 amis primaires dans la session

Scenario: Ajouter un ami secondaire avec succès
    Given un ami avec l'ID 3 existe
    And l'ami avec l'ID 3 ne participe pas à la session 1
    When j'ajoute l'ami 3 à la session 1 comme ami secondaire
    Then l'ami 3 devrait être ajouté à la session avec le statut "Secondary"
    And la session devrait avoir 3 participants actifs
    And l'ami 3 devrait avoir rejoint la session aujourd'hui

Scenario: Empêcher l'ajout d'un ami déjà présent
    Given un ami avec l'ID 2 participe déjà à la session 1
    When j'essaie d'ajouter l'ami 2 à la session 1
    Then l'opération devrait échouer avec le message "L'ami 2 participe déjà à cette session"
    And le nombre de participants ne devrait pas changer

Scenario: Empêcher l'ajout d'un ami à une session terminée
    Given une session terminée avec l'ID 2
    And un ami avec l'ID 4 existe
    When j'essaie d'ajouter l'ami 4 à la session 2
    Then l'opération devrait échouer avec le message "Impossible d'ajouter des amis à une session terminée"

Scenario: Modifier le statut d'un ami secondaire
    Given un ami avec l'ID 5 est ajouté à la session 1 comme ami secondaire
    When je change le statut de l'ami 5 vers "Observer"
    Then l'ami 5 devrait avoir le statut "Observer" dans la session 1

Scenario: Retirer un ami secondaire de la session
    Given un ami avec l'ID 6 est ajouté à la session 1 comme ami secondaire
    When je retire l'ami 6 de la session 1
    Then l'ami 6 ne devrait plus être actif dans la session 1
    And le nombre de participants actifs devrait diminuer de 1

Scenario: Empêcher la suppression du créateur
    When j'essaie de retirer l'ami 1 (créateur) de la session 1
    Then l'opération devrait échouer avec le message "Impossible de retirer le créateur de la session"

Scenario: Ajouter plusieurs amis secondaires
    Given les amis avec les IDs 7, 8, 9 existent
    When j'ajoute successivement les amis 7, 8, 9 à la session 1 comme amis secondaires
    Then la session devrait avoir 5 participants actifs
    And tous les nouveaux amis devraient avoir le statut "Secondary"