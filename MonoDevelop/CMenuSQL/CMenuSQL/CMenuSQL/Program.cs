using System;
using System.Data;
using MySql.Data.MySqlClient;
using CMenu;

namespace CMenuSQL
{
    class MainClass
    {
        private static IDbConnection conn;

        public static void Main(string[] args)
        {
            conn = new MySqlConnection("server=localhost;database=dbtest;user=normaluser;password=normalpass");
            try
            {
                conn.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("No se ha podido establecer una conexión a la base de datos");
                Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }

            ShowMainMenu();

            conn.Close();
        }

        public static void ShowMainMenu()
        {
            Menu menu = new Menu("Menu Principal");
            menu.AddOption("Salir")
                .AddOption("Nuevo")
                .AddOption("Editar")
                .AddOption("Borrar")
                .AddOption("Consultar")
                .AddOption("Listar");

            menu.Show();
            DoAction(menu.GetOption());
        }

        public static void DoAction(int action)
        {
            Menu menu;
            switch (action)
            {
                case 0:
                    return;

                case 1:
                    menu = new Menu("Datos Nuevos");
                    AnadirDatos();
                    break;

                case 2:
                    EditarDatos();
                    break;

                case 3:
                    BorrarDatos();
                    break;

                case 4:
                    menu = new Menu("Consultar Datos");
                    menu.AddOption("Por id")
                        .AddOption("Por nombre")
                        .AddOption("Por categoria");

                    menu.Show();
                    ConsultarDatos(menu.GetOption());
                    break;

                case 5:
                    ListarDatos();
                    break;

                default:
                    return;
            }

            ShowMainMenu();
        }

        public static void AnadirDatos()
        {
            Console.Write("Introduce el nombre del producto: ");
            string nombre = Console.ReadLine();

            int id_cat = -1;
            do
            {
                Console.Write("Introduce la categoría: ");
            } while (!int.TryParse(Console.ReadLine(), out id_cat));

            int precio = -1;
            do
            {
                Console.Write("Introduce el precio: ");
            } while (!int.TryParse(Console.ReadLine(), out precio));


            IDbCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO productos(nombre, id_categoria, precio) VALUES (@nombre, @id_cat, @precio)";
            command.Parameters.Add(new MySqlParameter("nombre", nombre));
            command.Parameters.Add(new MySqlParameter("id_cat", id_cat));
            command.Parameters.Add(new MySqlParameter("precio", precio));

            command.ExecuteNonQuery();
        }

        public static void EditarDatos()
        {
            int id = -1;
            do
            {
                Console.Write("Introduce la ID del producto: ");
            } while (!int.TryParse(Console.ReadLine(), out id));

            IDbCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM productos WHERE id_prod = @id_prod";
            command.Parameters.Add(new MySqlParameter("id_prod", id));

            IDataReader dataReader = command.ExecuteReader();
            if (!dataReader.Read())
            {
                Console.WriteLine("No existe ninguna fila con ID: " + id);
                Console.Read();
                ShowMainMenu();
                return;
            }


        }

        public static void BorrarDatos()
        {
            int id = -1;
            do
            {
                Console.Write("Introduce la ID del producto: ");
            } while (!int.TryParse(Console.ReadLine(), out id));

            IDbCommand command = conn.CreateCommand();
            command.CommandText = "DELETE FROM productos WHERE id_prod = @id_prod";
            command.Parameters.Add(new MySqlParameter("id_prod", id));

            int affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                Console.WriteLine("No existe ninguna fila con la ID: " + id);
            }
            else
            {
                Console.WriteLine("Se han eliminado " + affectedRows + " fila(s)");
            }

            Console.Read();
        }

        public static void ConsultarDatos(int opt)
        {
            IDbCommand command = conn.CreateCommand();
            switch (opt)
            {
                // Por id
                case 0:
                    int id = -1;
                    do
                    {
                        Console.Write("Introduce una ID: ");
                    } while (!int.TryParse(Console.ReadLine(), out id));

                    command.CommandText = "SELECT * FROM productos WHERE id_prod = @id_prod";
                    command.Parameters.Add(new MySqlParameter("id_prod", id));
                    break;

                // Por nombre
                case 1:
                    Console.Write("Introduce un nombre o parte de él: ");
                    string nombre = Console.ReadLine();

                    command.CommandText = "SELECT * FROM productos WHERE nombre LIKE @nombre";
                    command.Parameters.Add(new MySqlParameter("nombre", "%" + nombre + "%"));
                    break;

                // Por categoria
                case 2:
                    int id_cat = -1;
                    do
                    {
                        Console.Write("Introduce la ID de la categoría: ");
                    } while (!int.TryParse(Console.ReadLine(), out id_cat));

                    command.CommandText = "SELECT * FROM productos WHERE id_categoria = @id_cat";
                    command.Parameters.Add(new MySqlParameter("id_cat", id_cat));
                    break;

                default:
                    return;
            }

            IDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("ID: {0}", dataReader["id_prod"]);
                Console.WriteLine("Nombre: {0}", dataReader["nombre"]);
                Console.WriteLine("Categoria: {0}", dataReader["id_categoria"]);
                Console.WriteLine("Precio: {0}", dataReader["precio"]);
                Console.WriteLine("-------------------");
            }
            dataReader.Close();

            Console.Read();
        }

        public static void ListarDatos()
        {
            IDbCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM productos";

            IDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("ID: {0}", dataReader["id_prod"]);
                Console.WriteLine("Nombre: {0}", dataReader["nombre"]);
                Console.WriteLine("Categoria: {0}", dataReader["id_categoria"]);
                Console.WriteLine("Precio: {0}", dataReader["precio"]);
                Console.WriteLine("-------------------");
            }
            dataReader.Close();

            Console.Read();
        }
    }
}
