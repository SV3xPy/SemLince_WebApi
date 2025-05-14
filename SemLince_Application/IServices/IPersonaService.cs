using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemLince_Domain.CommonInterfaces;
using SemLince_Domain.Entities;

namespace SemLince_Application.IServices
{
    public interface IPersonaService : ICommonService<Persona>
    {
        Task<Persona> LoginAsync(string Email, string Password);
    }
}
