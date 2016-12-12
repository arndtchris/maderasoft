namespace MaderaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonneCiv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresse",
                c => new
                    {
                        AdresseID = c.Int(nullable: false, identity: true),
                        numRue = c.String(maxLength: 10, unicode: false),
                        nomRue = c.String(maxLength: 150, unicode: false),
                        codePostal = c.String(maxLength: 5, unicode: false),
                        ville = c.String(maxLength: 80, unicode: false),
                        pays = c.String(maxLength: 80, unicode: false),
                    })
                .PrimaryKey(t => t.AdresseID);
            
            CreateTable(
                "dbo.AffectationService",
                c => new
                    {
                        AffectationServiceID = c.Int(nullable: false, identity: true),
                        isPrincipal = c.Boolean(nullable: false),
                        employe_id = c.Int(),
                        groupe_id = c.Int(),
                        service_id = c.Int(),
                    })
                .PrimaryKey(t => t.AffectationServiceID)
                .ForeignKey("dbo.Employe", t => t.employe_id)
                .ForeignKey("dbo.Droit", t => t.groupe_id)
                .ForeignKey("dbo.Service", t => t.service_id)
                .Index(t => t.employe_id)
                .Index(t => t.groupe_id)
                .Index(t => t.service_id);
            
            CreateTable(
                "dbo.Personne",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        civ = c.String(maxLength: 3, unicode: false),
                        nom = c.String(maxLength: 80, unicode: false),
                        prenom = c.String(maxLength: 80, unicode: false),
                        email = c.String(maxLength: 150, unicode: false),
                        tel1 = c.String(maxLength: 10, unicode: false),
                        tel2 = c.String(maxLength: 10, unicode: false),
                        isFournisseur = c.Boolean(nullable: false),
                        isClient = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        adresse_AdresseID = c.Int(),
                        employe_id = c.Int(),
                        utilisateur_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Adresse", t => t.adresse_AdresseID)
                .ForeignKey("dbo.Employe", t => t.employe_id)
                .ForeignKey("dbo.Utilisateur", t => t.utilisateur_id)
                .Index(t => t.adresse_AdresseID)
                .Index(t => t.employe_id)
                .Index(t => t.utilisateur_id);
            
            CreateTable(
                "dbo.TEmploye",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Utilisateur",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        login = c.String(maxLength: 20, unicode: false),
                        password = c.String(maxLength: 20, unicode: false),
                        dCreation = c.DateTime(nullable: false),
                        dConnexion = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isFirstConnexion = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Droit",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        create = c.Boolean(nullable: false),
                        update = c.Boolean(nullable: false),
                        read = c.Boolean(nullable: false),
                        delete = c.Boolean(nullable: false),
                        libe = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ApplicationTrace",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        utilisateur = c.String(maxLength: 150, unicode: false),
                        date = c.DateTime(nullable: false),
                        action = c.String(maxLength: 150, unicode: false),
                        description = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Composant",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 150, unicode: false),
                        isDeleted = c.Boolean(nullable: false),
                        prixHT = c.Double(nullable: false),
                        fournisseur_id = c.Int(),
                        gamme_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Utilisateur", t => t.fournisseur_id)
                .ForeignKey("dbo.Gamme", t => t.gamme_id)
                .Index(t => t.fournisseur_id)
                .Index(t => t.gamme_id);
            
            CreateTable(
                "dbo.Gamme",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 100, unicode: false),
                        pourcentageGamme = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Composition",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        qte = c.Int(nullable: false),
                        composant_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Composant", t => t.composant_id)
                .Index(t => t.composant_id);
            
            CreateTable(
                "dbo.DevisFacture",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        isSigned = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        projet_id = c.Int(),
                        referent_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Projet", t => t.projet_id)
                .ForeignKey("dbo.Employe", t => t.referent_id)
                .Index(t => t.projet_id)
                .Index(t => t.referent_id);
            
            CreateTable(
                "dbo.Projet",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 150, unicode: false),
                        prixHT = c.Single(nullable: false),
                        prixTotalTTC = c.Single(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        isPaid = c.Boolean(nullable: false),
                        adresse_AdresseID = c.Int(),
                        client_id = c.Int(),
                        referent_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Adresse", t => t.adresse_AdresseID)
                .ForeignKey("dbo.Personne", t => t.client_id)
                .ForeignKey("dbo.Employe", t => t.referent_id)
                .Index(t => t.adresse_AdresseID)
                .Index(t => t.client_id)
                .Index(t => t.referent_id);
            
            CreateTable(
                "dbo.Etab",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 150, unicode: false),
                        adresse_AdresseID = c.Int(),
                        type_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Adresse", t => t.adresse_AdresseID)
                .ForeignKey("dbo.TEtab", t => t.type_id)
                .Index(t => t.adresse_AdresseID)
                .Index(t => t.type_id);
            
            CreateTable(
                "dbo.TEtab",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EtatAvancementProjet",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 150, unicode: false),
                        pourcentageADebloquer = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.HistoriqueProjet",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        avancementProjet_id = c.Int(),
                        projet_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EtatAvancementProjet", t => t.avancementProjet_id)
                .ForeignKey("dbo.Projet", t => t.projet_id)
                .Index(t => t.avancementProjet_id)
                .Index(t => t.projet_id);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 150, unicode: false),
                        coupePrincipe = c.String(maxLength: 8000, unicode: false),
                        typeModule_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TModule", t => t.typeModule_id)
                .Index(t => t.typeModule_id);
            
            CreateTable(
                "dbo.TModule",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        libe = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Taxe",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pourcentage = c.Double(nullable: false),
                        libe = c.String(maxLength: 50, unicode: false),
                        isReduction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Employe",
                c => new
                    {
                        id = c.Int(nullable: false),
                        typeEmploye_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Personne", t => t.id)
                .ForeignKey("dbo.TEmploye", t => t.typeEmploye_id)
                .Index(t => t.id)
                .Index(t => t.typeEmploye_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employe", "typeEmploye_id", "dbo.TEmploye");
            DropForeignKey("dbo.Employe", "id", "dbo.Personne");
            DropForeignKey("dbo.Module", "typeModule_id", "dbo.TModule");
            DropForeignKey("dbo.HistoriqueProjet", "projet_id", "dbo.Projet");
            DropForeignKey("dbo.HistoriqueProjet", "avancementProjet_id", "dbo.EtatAvancementProjet");
            DropForeignKey("dbo.Etab", "type_id", "dbo.TEtab");
            DropForeignKey("dbo.Etab", "adresse_AdresseID", "dbo.Adresse");
            DropForeignKey("dbo.DevisFacture", "referent_id", "dbo.Employe");
            DropForeignKey("dbo.DevisFacture", "projet_id", "dbo.Projet");
            DropForeignKey("dbo.Projet", "referent_id", "dbo.Employe");
            DropForeignKey("dbo.Projet", "client_id", "dbo.Personne");
            DropForeignKey("dbo.Personne", "utilisateur_id", "dbo.Utilisateur");
            DropForeignKey("dbo.Personne", "employe_id", "dbo.Employe");
            DropForeignKey("dbo.Personne", "adresse_AdresseID", "dbo.Adresse");
            DropForeignKey("dbo.Projet", "adresse_AdresseID", "dbo.Adresse");
            DropForeignKey("dbo.Composition", "composant_id", "dbo.Composant");
            DropForeignKey("dbo.Composant", "gamme_id", "dbo.Gamme");
            DropForeignKey("dbo.Composant", "fournisseur_id", "dbo.Utilisateur");
            DropForeignKey("dbo.AffectationService", "service_id", "dbo.Service");
            DropForeignKey("dbo.AffectationService", "groupe_id", "dbo.Droit");
            DropForeignKey("dbo.AffectationService", "employe_id", "dbo.Employe");
            DropIndex("dbo.Employe", new[] { "typeEmploye_id" });
            DropIndex("dbo.Employe", new[] { "id" });
            DropIndex("dbo.Module", new[] { "typeModule_id" });
            DropIndex("dbo.HistoriqueProjet", new[] { "projet_id" });
            DropIndex("dbo.HistoriqueProjet", new[] { "avancementProjet_id" });
            DropIndex("dbo.Etab", new[] { "type_id" });
            DropIndex("dbo.Etab", new[] { "adresse_AdresseID" });
            DropIndex("dbo.Projet", new[] { "referent_id" });
            DropIndex("dbo.Projet", new[] { "client_id" });
            DropIndex("dbo.Projet", new[] { "adresse_AdresseID" });
            DropIndex("dbo.DevisFacture", new[] { "referent_id" });
            DropIndex("dbo.DevisFacture", new[] { "projet_id" });
            DropIndex("dbo.Composition", new[] { "composant_id" });
            DropIndex("dbo.Composant", new[] { "gamme_id" });
            DropIndex("dbo.Composant", new[] { "fournisseur_id" });
            DropIndex("dbo.Personne", new[] { "utilisateur_id" });
            DropIndex("dbo.Personne", new[] { "employe_id" });
            DropIndex("dbo.Personne", new[] { "adresse_AdresseID" });
            DropIndex("dbo.AffectationService", new[] { "service_id" });
            DropIndex("dbo.AffectationService", new[] { "groupe_id" });
            DropIndex("dbo.AffectationService", new[] { "employe_id" });
            DropTable("dbo.Employe");
            DropTable("dbo.Taxe");
            DropTable("dbo.TModule");
            DropTable("dbo.Module");
            DropTable("dbo.HistoriqueProjet");
            DropTable("dbo.EtatAvancementProjet");
            DropTable("dbo.TEtab");
            DropTable("dbo.Etab");
            DropTable("dbo.Projet");
            DropTable("dbo.DevisFacture");
            DropTable("dbo.Composition");
            DropTable("dbo.Gamme");
            DropTable("dbo.Composant");
            DropTable("dbo.ApplicationTrace");
            DropTable("dbo.Service");
            DropTable("dbo.Droit");
            DropTable("dbo.Utilisateur");
            DropTable("dbo.TEmploye");
            DropTable("dbo.Personne");
            DropTable("dbo.AffectationService");
            DropTable("dbo.Adresse");
        }
    }
}
