using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Madera.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Ajoute une donnée en bdd
        void Insert(T entity);
        // Met à jour une entrée en bdd
        void Update(T entity);
        // Supprime une entrée en bdd
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        // Récupère une entrée en fonction de son id
        T GetById(int id);
        // Récupère une entrée en fonction d'une requête
        T Get(Expression<Func<T, bool>> where);
        // Récupère toutes les entrées d'une table
        IEnumerable<T> GetAll();
        // Recupère une liste d'entrées en fonction d'une requête
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
