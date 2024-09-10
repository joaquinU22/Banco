// Crear instancia del repositorio
using Banco.Entities;
using Banco.Repositories.Contracts;
using Banco.Repositories.Implementations;
using Banco.Services;

BancoManager serviceManager = new BancoManager();




//// Prueba 3: Obtener todos los clientes
//var clientes = serviceManager.GetAllClientes();
//foreach (var cliente in clientes)
//{
//    Console.WriteLine($"ID: {cliente.Id}, Nombre: {cliente.Nombre}, Apellido: {cliente.Apellido}, DNI: {cliente.Dni}");
//}

//// Crear una nueva cuenta
//var nuevaCuenta = new Cuenta
//{
//    Cbu = "405817171",
//    Saldo = 1500.50m,
//    TipoCuenta = tiposDeCuenta[0],
//    UltimoMovimiento = DateTime.Now,
//    ClienteId = clientes[0].Id,
//};
//bool cuentaCreada = serviceManager.AddCuenta(nuevaCuenta);
//Console.WriteLine("Cuenta creada: " + cuentaCreada);

//// Obtener todas las cuentas
//var cuentas = serviceManager.GetAllCuentas();
//foreach (var cuenta in cuentas)
//{
//    Console.WriteLine($"CBU: {cuenta.Cbu}, Saldo: {cuenta.Saldo}, Tipo: {cuenta.TipoCuenta.Nombre}");
//}

// Prueba 1: Obtener todos los tipos de cuentas
Console.WriteLine("Tipos de Cuentas Disponibles:");
var tiposDeCuenta = serviceManager.GetAllTiposCuentas();

foreach (var tipo in tiposDeCuenta)
{
    Console.WriteLine($"ID: {tipo.Id}, Nombre: {tipo.Nombre}");
}
var nuevoCliente = new Cliente
{
    Apellido = "Prueba test",
    Nombre = "Prueba test",
    Dni = "12313435"                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
};
var tipoCuenta = new TipoCuenta
{

    Id = 1,
    Nombre = "CREDITO"
};
var cuentaNueva = new Cuenta()
{
    Cbu = "405817171",
    Saldo = 1500.50m,
    TipoCuenta = tipoCuenta,
    UltimoMovimiento = DateTime.Now,
};
nuevoCliente.Cuentas.Add(cuentaNueva);

var seGuardoCliente = serviceManager.Add(nuevoCliente);


Console.WriteLine("\nPruebas completadas. Presiona cualquier tecla para salir.");
Console.ReadKey();