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
    public class ClienteRepository : IClienteRepository
    {
        public List<Cliente> GetAll()
        {
            var clientes = new List<Cliente>();
            DataTable table = DataHelper
                .GetInstance()
                .ExecuteSPQuery("OBTENER_CLIENTES", null);

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    Cliente cliente = new Cliente
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString(),
                        Dni = row["Dni"].ToString()
                    };
                    clientes.Add(cliente);
                }
            }

            return clientes;
        }

        public bool Add(Cliente cliente)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("CREAR_CLIENTE", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                //parámetro de entrada:
                cmd.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
                cmd.Parameters.AddWithValue("@APELLIDO", cliente.Apellido);
                cmd.Parameters.AddWithValue("@DNI", cliente.Dni);
                //parámetro de salida:
                SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int idCliente = (int)param.Value;
                if(cliente.Cuentas.Count == 0)
                {
                    t.Rollback();
                }
                foreach (var cuenta in cliente.Cuentas)
                {
                    var cmdCuenta = new SqlCommand("CREAR_CUENTA", cnn, t);
                    cmdCuenta.CommandType = CommandType.StoredProcedure;
                    cmdCuenta.Parameters.AddWithValue("@CBU", cuenta.Cbu);
                    cmdCuenta.Parameters.AddWithValue("@SALDO", cuenta.Saldo);
                    cmdCuenta.Parameters.AddWithValue("@tipo_cuenta_id", cuenta.TipoCuenta.Id );
                    cmdCuenta.Parameters.AddWithValue("@ULTIMO_MOVIMIENTO", cuenta.UltimoMovimiento);
                    cmdCuenta.Parameters.AddWithValue("@CLIENTE_ID", idCliente);
                    cmdCuenta.ExecuteNonQuery();
                }

                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
    }
}
