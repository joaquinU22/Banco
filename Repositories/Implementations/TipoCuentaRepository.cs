using Banco.Entities;
using Banco.Repositories.Contracts;
using Banco.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Repositories.Implementations
{
    public class TipoCuentaRepository : ITipoCuentaRepository
    {
        public List<TipoCuenta> GetAll()
        {
            DataTable dt = DataHelper
                .GetInstance()
                .ExecuteSPQuery("Obtener_tipos_cuentas", null);
            var tiposDeCuenta = new List<TipoCuenta>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var tipoCuenta = new TipoCuenta
                    {
                        Id = (int)row["Id"],
                        Nombre = row["Nombre"].ToString()
                    };
                    tiposDeCuenta.Add(tipoCuenta);
                }
            }

            return tiposDeCuenta;
        }
    }
}
