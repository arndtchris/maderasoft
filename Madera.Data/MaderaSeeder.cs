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
            SeedAdresses().ForEach(c => context.Adresses.Add(c));
            SeedServices().ForEach(c => context.Services.Add(c));
            SeedDroits().ForEach(c => context.Droits.Add(c));
            SeedPersonnes().ForEach(c => context.Personnes.Add(c));
            SeedTEmployes().ForEach(c => context.TEmployes.Add(c));

            context.Commit();
        }

        private static List<Adresse> SeedAdresses()
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
                }
            };
        }
        private static List<Utilisateur> SeedUtilisateurs()
        {
            return new List<Utilisateur>
            {
                new Utilisateur {
                    id = 1,
                    login = "mickaelcimbaluria",
                    password = "123456",
                    dCreation = DateTime.Now,
                    dConnexion = DateTime.Now,
                    isActive = true,
                    isFirstConnexion = false,
                    isDeleted = false
                },
                new Utilisateur {
                    id = 2,
                    login = "thomasberthemin",
                    password = "123456",
                    dCreation = DateTime.Now,
                    dConnexion = DateTime.Now,
                    isActive = true,
                    isFirstConnexion = false,
                    isDeleted = false,
                },
                new Utilisateur {
                    id = 3,
                    login = "chrisarndt",
                    password = "123456",
                    dCreation = DateTime.Now,
                    dConnexion = DateTime.Now,
                    isActive = true,
                    isFirstConnexion = false,
                    isDeleted = false,
                },
            };

        }

        private static List<Personne> SeedPersonnes()
        {
            return new List<Personne>
            {
                new Personne {
                    id = 1,
                    civ =  "Monsieur",
                    nom = "Cimbaluria",
                    prenom = "Mickaël",
                    email = "mickael.cimbaluria@madera.fr",
                    tel1 = "0383123456",
                    tel2 = "",
                    isFournisseur = false,
                    isClient = false,
                    isDeleted = false               
                },
                new Personne {
                    id = 1,
                    civ =  "Monsieur",
                    nom = "Berthemin",
                    prenom = "Thomas",
                    email = "thomas.berthemin@madera.fr",
                    tel1 = "0383123456",
                    tel2 = "",
                    isFournisseur = false,
                    isClient = false,
                    isDeleted = false
                },new Personne {
                    id = 1,
                    civ =  "Monsieur",
                    nom = "Cimbaluria",
                    prenom = "Mickaël",
                    email = "chris.arndt@madera.fr",
                    tel1 = "0383123456",
                    tel2 = "",
                    isFournisseur = false,
                    isClient = false,
                    isDeleted = false
                }
            };

        }


        private static List<DevisFacture> SeedDevisFacture()
        {
            return new List<DevisFacture>
            {
                new DevisFacture
                {
                    id = 1,
                    isSigned = true,
                    isDeleted = false,
                },
                new DevisFacture
                {
                    id = 2,
                    isSigned = true,
                    isDeleted = false,
                },
                new DevisFacture
                {
                    id = 3,
                    isSigned = false,
                    isDeleted = false,
                }
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
                }
            };
        }

        private static List<Personne> SeedPersonnes()
        {
            return new List<Personne>
            {
                new Personne
                {
                    isDeleted = false,
                    isClient = false,
                    isFournisseur = false,
                    nom = "Arndt",
                    prenom = "Chris"
                },
                new Personne
                {
                    isDeleted = false,
                    isClient = false,
                    isFournisseur = false,
                    nom = "Berthemin",
                    prenom = "Thomas"
                },
                new Personne
                {
                    isDeleted = false,
                    isClient = false,
                    isFournisseur = false,
                    nom = "Cimbaluria",
                    prenom = "Mickaêl"
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