using Banco.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Repositories.Contracts
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();
        bool Add(Cliente cliente);
    }
}
