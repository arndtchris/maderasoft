--- Couche Data ---

La couche “Database” est la couche la plus basse au sein de cette architecture. C’est à ce niveau que se trouve la base de données  SqlServer où sont stockées les données de l’application.
Au sein de la solution MaderaSoft nous utilisons une approche « code first » pour créer et gérer la structure de notre base de données. 
Pour réaliser cela les classes métiers que nous créons peuvent être surchargées par des métas donnés interprétées par « Entity Framework ». 

Dans un souci de réutilisabilité de code, nous avons préféré séparer cette surcharge (nécessaire à la gestion de la base de données) de nos classes métiers. 
C’est donc dans la couche Database, dans le dossier Configuration, que nous retrouvons les propriétés de nos tables en base de données.

--- Couche Repository ---

Pour réduire l’interdépendance entre les couches Data et Models nous avons fait le choix de mettre en œuvre le patron de développement « Repository ». 
Comme expliqué précédemment ce dernier permet de charger nos plain object en respectant les métadonnées utilisées par Entity Framework qui réalise les transactions 
SQLs adéquates avec la base de données. 
De manière générale ce découplage entre les classes métiers et leur mapping avec la base de données assure de bonnes dispositions pour l’évolution du logiciel. 
En effet si l’équipe informatique est amenée à changer d’ORM (Object-Relational Mapping) seule fichiers de mapping de la couche Data seront à modifier, pour le reste de l’application 
la transition sera transparente.

La couche Repositories sert d’interface entre la base de données et la couche applicative c’est à cet endroit de l’application que nous retrouvons les 4 opérations de bases que l’on 
peut réaliser avec des données: Lecture, Ecriture, Modification et Suppression. A ce niveau le code source est donc dépourvu de toute logique métier.
 Une logique métier est un comportement répondant à un besoin spécifique (calcul de TVA, gestion des droits utilisateur, autorisation de connexion…) cependant 
 d’autres traitements hors métier peuvent être réalisé : actualiser une date de mise à jour, crypter un champ ou encore initialiser des valeurs par défaut…

