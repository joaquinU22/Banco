using Banco.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Repositories.Contracts
{
    public interface ICuentaRepository
    {
        bool Add(Cuenta cuenta);
        List<Cuenta> Get();
        bool Update(Cuenta cuenta);
        bool Delete(string cbu);
    }

}
