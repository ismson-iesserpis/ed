package ProyectoADIsmailYilmaz.DAO;

import java.util.List;

import ProyectoADIsmailYilmaz.Cliente;
import ProyectoADIsmailYilmaz.Main;

public class DAOCliente
{
	public static void MenuListarCliente(boolean especifico)
	{
		if (especifico)
		{
			System.out.print("Escribe la ID del cliente: ");
			long id = Main.sc.nextLong();
			
			Cliente cliente = DAOManager.Get(Cliente.class, id);
			
			if (cliente == null)
			{
				System.out.println("No se ha encontrado un artículo con ID " + id);
			}
			
			System.out.println(cliente.toString());
		}
		else
		{
			List<Cliente> clientes = DAOManager.GetAll(Cliente.class);
			
			if (clientes.isEmpty())
			{
				System.out.println("No se han encontrado artículos");
				return;
			}
			
			for (int i = 0; i < clientes.size(); ++i)
			{
				System.out.println(clientes.get(i).toString());
			}
		}
	}
	
	public static void MenuInsertarCliente()
	{
		System.out.print("Escribe el nombre del cliente: ");
		
		if (Main.sc.hasNextLine())
			Main.sc.nextLine();
		
		String nombre = Main.sc.nextLine();
		
		Cliente cliente = new Cliente(nombre);
		DAOManager.Guardar(cliente);
	}
	
	public static void MenuModificarCliente()
	{
		System.out.print("Escribe la ID del cliente: ");
		
		long id = Main.sc.nextLong();
		Cliente cliente = DAOManager.Get(Cliente.class, id);
		
		if (cliente == null)
		{
			System.out.println("No se ha encontrado un cliente con ID " + id);
			return;
		}
		
		System.out.print("Escribe el nuevo nombre del cliente: ");
		
		if (Main.sc.hasNextLine())
			Main.sc.nextLine();
		
		String nombre = Main.sc.nextLine();
		
		if (!nombre.isEmpty())
			cliente.setNombre(nombre);
		
		DAOManager.Actualizar(cliente);
	}
	
	public static void MenuEliminarCliente()
	{
		System.out.print("Escribe la ID del cliente: ");
		
		long id = Main.sc.nextLong();
		Cliente cliente = DAOManager.Get(Cliente.class, id);
		
		if (cliente == null)
		{
			System.out.println("No se ha encontrado un cliente con ID " + id);
			return;
		}
		
		DAOManager.Borrar(cliente);
	}
}
