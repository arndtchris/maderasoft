using Madera.Data.Infrastructure;
using Madera.Data.Repositories;
using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Service
{
    public class DevisFactureService : IDevisFactureService
    {
        private readonly IDevisFactureRepository _devisRepository;
        private readonly IApplicationTraceService _applicationTraceService;
        private readonly IUnitOfWork _unitOfWork;


        public DevisFactureService(IDevisFactureRepository devisRepository, IUnitOfWork unitOfWork, IApplicationTraceService applicationTraceService)
        {
            this._devisRepository = devisRepository;
            this._applicationTraceService = applicationTraceService;
            this._unitOfWork = unitOfWork;
        }


        public IEnumerable<DevisFacture> DonneTous()
        {
            return _devisRepository.GetAll();
        }

        public DevisFacture Get(int id)
        {
            return _devisRepository.GetById(id);
        }

        public void Update(DevisFacture devis)
        {
            _devisRepository.Update(devis);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Modification.ToString(),
                description = String.Format("Mise à jour du devis devis_id = {0}", devis.id),
            });
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _devisRepository.Delete(x => x.id == id);
            //ToDo : réaliser une suppression complète ou logique en fonction des droits de l'utilisateur en session
            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Suppression.ToString(),
                description = String.Format("Supression de le devis devis_id = {0}", id),
            });
        }

        public void Create(DevisFacture item)
        {

            _devisRepository.Insert(item);

            _applicationTraceService.create(new ApplicationTrace
            {
                utilisateur = "",
                action = Parametres.Action.Creation.ToString(),
                description = String.Format("Ajout d'un élément devis facture devisFacture_id={0}", item.id),
            });
        }
    }

    //Définition des méthodes qui seront accessibles depuis la couche de présentation
    public interface IDevisFactureService : IService<DevisFacture>
    {
    }
}
