using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Entities
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string Cbu { get; set; }
        public decimal Saldo { get; set; }
        public DateTime UltimoMovimiento { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public int ClienteId { get; set; } // FK
    }

}
