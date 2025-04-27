using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    //////////
    ///
    SIN USO ACTUALMENTE PERO PODRIA SERVIR COMO UN REPOSITORY GENERICO
    DEL CUAL SE PUEDEN DERIVAR LOS DEMAS
    ///
    //////////
 */
namespace SemLince_Domain.CommonInterfaces
{
    public interface ICommonRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(int id, TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
