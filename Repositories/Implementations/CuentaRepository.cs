using Banco.Entities;
using Banco.Repositories.Contracts;
using Banco.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Repositories.Implementations
{
    public class CuentaRepository : ICuentaRepository
    {
        public bool Add(Cuenta cuenta)
        {
            var parametros = new List<ParameterSQL>
                {
                    new ParameterSQL("@CBU", cuenta.Cbu),
                    new ParameterSQL("@Saldo", cuenta.Saldo),
                    new ParameterSQL("@Tipo_Cuenta_Id", cuenta.TipoCuenta.Id),
                    new ParameterSQL("@Ultimo_Movimiento", cuenta.UltimoMovimiento),
                    new ParameterSQL("@Cliente_Id", cuenta.ClienteId)
                };

            int filasAfectadas = DataHelper
                .GetInstance()
                .ExecuteSPDML("CREAR_CUENTA", parametros);

            return filasAfectadas > 0;
        }

        public List<Cuenta> Get()
        {
            var cuentas = new List<Cuenta>();
            DataTable table = DataHelper
                .GetInstance()
                .ExecuteSPQuery("OBTENER_CUENTAS", null);

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var cuenta = new Cuenta
                    {
                        Cbu = row["CBU"].ToString(),
                        Saldo = Convert.ToDecimal(row["Saldo"]),
                        UltimoMovimiento = Convert.ToDateTime(row["ULTIMO_MOVIMIENTO"]),
                        TipoCuenta = new TipoCuenta
                        {
                            Id = Convert.ToInt32(row["TIPO_CUENTA_ID"]),
                            Nombre = row["NOMBRE_CUENTA"].ToString()
                        },
                        ClienteId = Convert.ToInt32(row["CLIENTE_ID"])
                    };
                    cuentas.Add(cuenta);
                }
            }

            return cuentas;
        }

        public bool Update(Cuenta cuenta)
        {
            var parametros = new List<ParameterSQL>
                {
                    new ParameterSQL("@ID",cuenta.Id),
                    new ParameterSQL("@CBU", cuenta.Cbu),
                    new ParameterSQL("@Saldo", cuenta.Saldo),
                    new ParameterSQL("@Tipo_Cuenta_Id", cuenta.TipoCuenta.Id),
                    new ParameterSQL("@Ultimo_Movimiento", cuenta.UltimoMovimiento),
                };

            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("ACTUALIZAR_CUENTA", parametros);
            return filasAfectadas > 0;
        }

        public bool Delete(string cbu)
        {
            //CREAR EL SP
            var parametros = new List<ParameterSQL>
                {
                    new ParameterSQL("@CBU", cbu)
                };

            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("ELIMINAR_CUENTA", parametros);
            return filasAfectadas > 0;
        }
    }
}

