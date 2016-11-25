--- Couche Presentation ---

Le dernier niveau de notre architecture est la couche Presentation. 
C’est elle qui va intercepter et traiter toutes les actions d’un utilisateur, pour construire des Interface Homme Machine répondant à ces attentes.
Pour réaliser nous appliquons la norme de développement MVC. Les contrôleurs interceptent les requêtes http, appel les différences services pour charger des données 
dans un ViewModel (voir la section Model dans Convention de nommage) et en affiche le contenu dans une Vue répondant à la demande initiale de l’utilisateur.


