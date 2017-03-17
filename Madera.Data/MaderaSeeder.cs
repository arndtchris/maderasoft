using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Madera.Model;

namespace Madera.Data
{
    public class MaderaSeeder : DropCreateDatabaseIfModelChanges<MaderaEntities>
    {
        protected override void Seed(MaderaEntities context)
        {
            /*
             * Le Seeder ne doit pas être utilisé pour alimenter des objets complexes (combinant plusieurs objets) 
             * comme un module qui combine plusieurs composant.
             * 
             * Par contre le Seeder est adapté à la création de gammes, services, composants...
             */
            //SeedAdresses().ForEach(d => context.Adresses.Add(d));
            SeedServices().ForEach(e => context.Services.Add(e));
            SeedDroits().ForEach(f => context.Droits.Add(f));
            SeedTEmployes().ForEach(h => context.TEmployes.Add(h));
            SeedGammes().ForEach(i => context.Gamme.Add(i));
            SeedTModule().ForEach(j => context.TModule.Add(j));
            SeedPersonne().ForEach(j => context.Personnes.Add(j));
            //SeedModules().ForEach(j => context.Module.Add(j));

            context.Commit();
        }

        /*private static List<Adresse> SeedAdresses()
        {
            return new List<Adresse>
            {
                new Adresse {
                    codePostal = "54000",
                    ville = "Nancy",
                    pays = "France",
                    numRue = "2bis",
                    nomRue = "Jean Jacques"
                },
                new Adresse {
                    codePostal = "54000",
                    ville = "Nancy",
                    pays = "France",
                    numRue = "14",
                    nomRue = "Cours Léopold"
                },
                new Adresse {
                    codePostal = "67000",
                    ville = "Strasbourg",
                    pays = "France",
                    numRue = "12",
                    nomRue = "rue des tulipes"
                }
            };
        }*/

        /*private static List<Module> SeedModules()
        {
            List<Module> lModule = new List<Module>();
            lModule.Add(new Module { id = 1, coupePrincipe = "string", libe = "mod1", prix = 3, typeModule = SeedTModule().First() });
            return lModule;
        }*/

        private static List<Gamme> SeedGammes()
        {
            return new List<Gamme>
            {
                new Gamme
                {
                    id = 1,
                    libe = "Basic",
                    pourcentageGamme = 10
                },
                new Gamme
                {
                    id = 2,
                    libe = "Timber",
                    pourcentageGamme = 20
                },
                new Gamme
                {
                    id = 3,
                    libe = "Prestige",
                    pourcentageGamme = 30
                },

            };
        }



        private static List<Service> SeedServices()
        {
            return new List<Service>
            {
                new Service
                {
                    libe = "Comptabilité"
                },
                new Service
                {
                    libe = "Gestion des stocks"
                },
                new Service
                {
                    libe = "Service commercial"
                },
                new Service
                {
                    libe = "Ressources humaines"
                },
                new Service
                {
                    libe = "Recherche & développement"
                }
            };
        }

        private static List<TEmploye> SeedTEmployes()
        {
            return new List<TEmploye>
            {
                new TEmploye
                {
                    libe = "Manager"
                },
                new TEmploye
                {
                    libe = "Directeur"
                },
                new TEmploye
                {
                    libe = "DSI"
                }

            };
        }

        private static List<Personne> SeedPersonne()
        {
            //Création d'adresses pour les fournisseurs
            //Adresse Les portes du paradis
            Adresse adresse1 = new Adresse();
            adresse1.id = 100;
            adresse1.nomRue = "rue des petits champs";
            adresse1.numRue = "21";
            adresse1.pays = "France";
            adresse1.ville = "Nancy";
            adresse1.codePostal = "54000";

            //Création d'adresses pour les fournisseurs
            //Adresse Placards de rêve
            Adresse adresse2 = new Adresse();
            adresse2.id = 101;
            adresse2.nomRue = "Zone industrielle du Beau bois";
            adresse2.numRue = "32";
            adresse2.pays = "France";
            adresse2.ville = "Ludres";
            adresse2.codePostal = "54710";

            //Création d'adresses pour les fournisseurs
            //Adresse Top du bois
            Adresse adresse3 = new Adresse();
            adresse3.id = 102;
            adresse3.nomRue = "rue des arbres";
            adresse3.numRue = "46";
            adresse3.pays = "France";
            adresse3.ville = "Nancy";
            adresse3.codePostal = "54000";

            //Création de users pour les fournisseurs
            //User Portes du paradis
            Utilisateur user1 = new Utilisateur();
            user1.id = 100;
            user1.login = "lesportesduparadis";
            user1.password = "123456";
            user1.isActive = true;
            user1.isDeleted = false;
            user1.dConnexion = DateTime.Now;
            user1.dCreation = DateTime.Now;
            user1.isFirstConnexion = false;

            //User Placards de rêve
            Utilisateur user2 = new Utilisateur();
            user2.id = 101;
            user2.login = "placardsdereve";
            user2.password = "123456";
            user2.isActive = true;
            user2.isDeleted = false;
            user2.dConnexion = DateTime.Now;
            user2.dCreation = DateTime.Now;
            user2.isFirstConnexion = false;

            //User top du bois
            Utilisateur user3 = new Utilisateur();
            user3.id = 102;
            user3.login = "letopdubois";
            user3.password = "123456";
            user3.isActive = true;
            user3.isDeleted = false;
            user3.dConnexion = DateTime.Now;
            user3.dCreation = DateTime.Now;
            user3.isFirstConnexion = false;

            return new List<Personne>
            {
                new Personne
                {
                    id = 100,
                    civ = "",
                    nom = "Les portes du paradis",
                    prenom = "",
                    isFournisseur = true,
                    isClient = false,
                    isDeleted = false,
                    email = "lesportesduparadis@contact.fr",
                    tel1 = "0393227470",
                    tel2 = "",
                    adresse = adresse1,
                    utilisateur = user1   
                },
                new Personne
                {
                    id = 101,
                    civ = "",
                    nom = "Placards de rêve",
                    prenom = "",
                    isFournisseur = true,
                    isClient = false,
                    isDeleted = false,
                    email = "placardsdereve@contact.fr",
                    tel1 = "0372727470",
                    tel2 = "",
                    adresse = adresse2,
                    utilisateur = user2
                },
                new Personne
                {
                    id = 102,
                    civ = "",
                    nom = "Top du bois",
                    prenom = "",
                    isFournisseur = true,
                    isClient = false,
                    isDeleted = false,
                    email = "topdubois@contact.fr",
                    tel1 = "0326123436",
                    tel2 = "",
                    adresse = adresse3,
                    utilisateur = user3
                }

            };
        }

        private static List<TModule> SeedTModule()
        {
            return new List<TModule>
            {
                new TModule
                {
                    libe = "Basic"
                },
                new TModule
                {
                    libe = "Timber"
                },
                new TModule
                {
                    libe = "Prestige"
                }

            };
        }

        private static List<Droit> SeedDroits()
        {
            return new List<Droit>
            {
                new Droit
                {
                    libe = "Administrateur",
                    create = true,
                    update = true,
                    read = true,
                    delete = true,
                },
                new Droit
                {
                    libe = "Utilisateur",
                    create = true,
                    update = true,
                    read = true,
                    delete = false
                },
                new Droit
                {
                    libe = "Observateur",
                    create = false,
                    update = false,
                    read = true,
                    delete = false
                }
            };
        }
    }
}