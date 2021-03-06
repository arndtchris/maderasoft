﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Madera.Data.Configuration;
using Madera.Model;

namespace Madera.Data
{
    public class MaderaEntities : DbContext
    {
        public MaderaEntities() : base("madera") { }

        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<AffectationService> AffectationsServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Droit> Droits { get; set; }
        public DbSet<Personne> Personnes { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<TEmploye> TEmployes { get; set; }
        public DbSet<DevisFacture> DevisFacture { get; set; }
        public DbSet<Gamme> Gamme { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PositionModule> PositionModule { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Composant> Composant { get; set; }
        public DbSet<Etage> Etage { get; set; }
        public DbSet<TModule> TModule { get; set; }
        public virtual void Commit()
        {
            this.SaveChanges();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // On récupère l'ensemble des erreurs
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // On concatène la liste d'erreur en un seul String où les différentes erreurs sont séparées par un ;
                var fullErrorMessage = string.Join("; ", errorMessages);

                // On concatène le message d'erreur initial avec le détail
                var exceptionMessage = string.Concat(ex.Message, " Erreurs de validation : ", fullErrorMessage);

                // On lève l'exception
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdresseConfiguration());
            modelBuilder.Configurations.Add(new AffectationServiceConfiguration());
            modelBuilder.Configurations.Add(new ApplicationTraceConfiguration());
            modelBuilder.Configurations.Add(new ComposantConfiguration());
            modelBuilder.Configurations.Add(new CompositionConfiguration());
            modelBuilder.Configurations.Add(new DevisFactureConfiguration());
            modelBuilder.Configurations.Add(new DroitConfiguration());
            modelBuilder.Configurations.Add(new EmployeConfiguration());
            modelBuilder.Configurations.Add(new EtabConfiguration());
            modelBuilder.Configurations.Add(new EtatAvancementProjetConfiguration());
            modelBuilder.Configurations.Add(new GammeConfiguration());
            modelBuilder.Configurations.Add(new HistoriqueProjetConfiguration());
            modelBuilder.Configurations.Add(new ModuleConfiguration());
            modelBuilder.Configurations.Add(new PersonneConfiguration());
            modelBuilder.Configurations.Add(new ProjetConfiguration());
            modelBuilder.Configurations.Add(new ServiceConfiguration());
            modelBuilder.Configurations.Add(new TaxeConfiguration());
            modelBuilder.Configurations.Add(new TEmployeConfiguration());
            modelBuilder.Configurations.Add(new TEtabConfiguration());
            modelBuilder.Configurations.Add(new TModuleConfiguration());
            modelBuilder.Configurations.Add(new UtilisateurConfiguration());
            modelBuilder.Configurations.Add(new PlanConfiguration());
            modelBuilder.Configurations.Add(new PositionModuleConfiguration());
            modelBuilder.Configurations.Add(new EtageConfiguration());
        }
    }
}