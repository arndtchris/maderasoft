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