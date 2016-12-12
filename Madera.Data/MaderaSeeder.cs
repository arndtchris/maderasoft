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
            SeedAdresses().ForEach(d => context.Adresses.Add(d));
            SeedServices().ForEach(e => context.Services.Add(e));
            SeedDroits().ForEach(f => context.Droits.Add(f));
            //SeedDevisFacture().ForEach(g => context.DevisFacture.Add(g));
            //SeedEmployes().ForEach(c => context.Employes.Add(c));
            SeedTEmployes().ForEach(h => context.TEmployes.Add(h));
            SeedGammes().ForEach(i => context.Gamme.Add(i));

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
                },
                new Adresse {
                    codePostal = "67000",
                    ville = "Strasbourg",
                    pays = "France",
                    numRue = "12",
                    nomRue = "rue des tulipes"
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

        private static List<Gamme> SeedGammes()
        {
            return new List<Gamme>
            {
                new Gamme
                {
                    id = 1,
                    libe = "Essentiel",
                    pourcentageGamme = 10
                },
                new Gamme
                {
                    id = 2,
                    libe = "Intermédiaire",
                    pourcentageGamme = 20
                },
                new Gamme
                {
                    id = 3,
                    libe = "Luxe",
                    pourcentageGamme = 30
                },

            };
        }

        //private static List<Personne> SeedPersonnes()
        //{

        //    return new List<Personne>
        //    {
        //        new Personne {
        //            id = 1,
        //            civ =  "Monsieur",
        //            nom = "Cimbaluria",
        //            prenom = "Mickaël",
        //            email = "mickael.cimbaluria@madera.fr",
        //            tel1 = "0383123456",
        //            tel2 = "",
        //            isFournisseur = false,
        //            isClient = true,
        //            isDeleted = false               
        //        },
        //        new Personne {
        //            id = 2,
        //            civ =  "Monsieur",
        //            nom = "Berthemin",
        //            prenom = "Thomas",
        //            email = "thomas.berthemin@madera.fr",
        //            tel1 = "0383123456",
        //            tel2 = "",
        //            isFournisseur = false,
        //            isClient = false,
        //            isDeleted = false
        //        },new Personne {
        //            id = 3,
        //            civ =  "Monsieur",
        //            nom = "Arndt",
        //            prenom = "Chris",
        //            email = "chris.arndt@madera.fr",
        //            tel1 = "0383123456",
        //            tel2 = "",
        //            isFournisseur = false,
        //            isClient = false,
        //            isDeleted = false
        //        }
        //    };

        //}


        //private static List<DevisFacture> SeedDevisFacture()
        //{
        //    Employe emp = new Employe(1, false, SeedTEmployes()[0], null, SeedPersonnes()[1]);
        //    Projet p = new Projet(1, "Projet trop cool", 100, 120, false, true, SeedPersonnes()[0] , SeedAdresses()[0], emp);


        //    return new List<DevisFacture>
        //    {
        //        new DevisFacture
        //        {
        //            id = 1,
        //            isSigned = true,
        //            isDeleted = false,
        //            projet = p,
        //            referent = emp

        //        }
        //    };
        //}

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

        //private static List<Employe> SeedEmployes()
        //{
        //    return new List<Employe>
        //    {
        //        new Employes
        //        {
        //            id = 1,
        //            isDeleted = false
        //        },
        //        new Employes
        //        {
        //            id = 2,
        //            isDeleted = false
        //        },
        //        new Employes
        //        {
        //            id = 3,
        //            isDeleted = false
        //        }
        //    };
        //}

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