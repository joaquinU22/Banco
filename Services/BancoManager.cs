using Banco.Entities;
using Banco.Repositories.Contracts;
using Banco.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Services
{
    public class BancoManager
    {
        IClienteRepository clienteRepository;
        ITipoCuentaRepository tipoCuentaRepository;
        ICuentaRepository cuentaRepository;
        public BancoManager()
        {
            clienteRepository = new ClienteRepository();
            tipoCuentaRepository = new TipoCuentaRepository();
            cuentaRepository = new CuentaRepository();
        }

        public List<Cliente> GetAllClientes()
        {
            return clienteRepository.GetAll();
        }
        public bool Add(Cliente cliente)
        {
            return clienteRepository.Add(cliente);
        }
        public List<TipoCuenta> GetAllTiposCuentas()
        {
            return tipoCuentaRepository.GetAll();
        }
        public bool AddCuenta(Cuenta cuenta)
        {
            return cuentaRepository.Add(cuenta);
        }
        public List<Cuenta> GetAllCuentas()
        {
            return cuentaRepository.Get();
        }
    }
}
