using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madera.Service
{
    public interface IService<T> where T : class
    {
        /// <summary>
        /// Trouve un élément selon son identifiant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Ajoute un élément en base de données
        /// </summary>
        /// <param name="item"></param>
        void Create(T item);

        /// <summary>
        /// Met à jour un élément en base de données
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);

        /// <summary>
        /// Supprime un élément selon son identifiant
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Donne la liste de tous les éléments en base de données
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> DonneTous();

        /// <summary>
        /// Permet de fermer et rendre effective une série de requêtes sql
        /// </summary>
        void Save();

    }
}
