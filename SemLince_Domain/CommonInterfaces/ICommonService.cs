using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemLince_Domain.CommonInterfaces
{
    /*
    //////////
    ///
    SIN USO ACTUALMENTE PERO PODRIA SERVIR COMO UN SERVICE GENERICO
    DEL CUAL SE PUEDEN DERIVAR LOS DEMAS
    ///
    //////////
 */
    public interface ICommonService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(int id,TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
