using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemLince_Domain.CommonInterfaces;
using SemLince_Domain.Entities;

namespace SemLince_Application.IRepositories
{
    public interface IEventRepository : ICommonRepository<Event>
    {
    }
}
