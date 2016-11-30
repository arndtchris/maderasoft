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
            SeedEmployes().ForEach(c => context.Employes.Add(c));
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

        private static List<Employe> SeedEmployes()
        {
            return new List<Employe>
            {
                new Employe
                {
                    isDeleted = false,
                    isClient = false,
                    isFournisseur = false,
                    nom = "Arndt",
                    prenom = "Chris"
                },
                new Employe
                {
                    isDeleted = false,
                    isClient = false,
                    isFournisseur = false,
                    nom = "Berthemin",
                    prenom = "Thomas"
                },
                new Employe
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