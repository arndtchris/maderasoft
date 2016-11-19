using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Model
{
    /*
     * Cette table permet de tracer toutes les actions réalisées au sein de l'application
     * Si un utilisateur se connecte
     * Si un nouvel élément est créé/supprimé/modifié
     * 
     * utilisateur : Nom + Prénom + id
     * date : date du jour
     * action : connection/déconnection/création/modification/suppression logique/suppression
     * description : identifier l'objet ex : type objet + id
     * 
     * Cette table va être essentielement renseignée depuis la couche service de l'application.
     * De cette façon chaque appel de cette couche sera traçé.
     * 
     */


    public class ApplicationTrace
    {
        public int id { get; set; }
        public string utilisateur { get; set; }
        public DateTime date { get; set; }
        public string action { get; set; }
        public string description { get; set; }

        public ApplicationTrace()
        {

        }
    }
}