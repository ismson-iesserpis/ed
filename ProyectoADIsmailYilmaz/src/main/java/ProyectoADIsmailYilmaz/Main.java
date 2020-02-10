package ProyectoADIsmailYilmaz;

import java.util.Scanner;

import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

import ProyectoADIsmailYilmaz.DAO.DAOArticulo;
import ProyectoADIsmailYilmaz.DAO.DAOCategoria;
import ProyectoADIsmailYilmaz.DAO.DAOCliente;
import ProyectoADIsmailYilmaz.DAO.DAOManager;
import ProyectoADIsmailYilmaz.DAO.DAOPedido;
import ProyectoADIsmailYilmaz.DAO.DAOPedidoLinea;

public class Main {
	public static Scanner sc;
	
	public static void main(String[] args)
	{
		EntityManagerFactory entityManagerFactory = Persistence.createEntityManagerFactory("proyectoadismailyilmaz");
		DAOManager.em = entityManagerFactory.createEntityManager();
		sc = new Scanner(System.in);
		
		MenuPrincipal();
		
		sc.close();
		DAOManager.em.close();			
		entityManagerFactory.close();
	}
	
	private static void MenuPrincipal()
	{
		System.out.println("Menu principal");
		System.out.println("1.- Listar");
		System.out.println("2.- Añadir");
		System.out.println("3.- Modificar");
		System.out.println("4.- Eliminar");
		System.out.println("5.- Salir");
		
		int option = ElegirOpcion(1, 5);
		switch (option)
		{
			// Listar
			case 1:
				MenuListar();
				break;
				
			// Añadir
			case 2:
				MenuAnadir();
				break;
				
			// Modificar
			case 3:
				MenuModificar();
				break;
				
			// Eliminar
			case 4:
				MenuEliminar();
				break;
				
			// Salir
			case 5:
				return;
				
			default:
				return;
		}
		
		MenuPrincipal();
	}
	
	public static void MenuListar()
	{
		System.out.println("Listar datos de");
		
		int tablas = ListaTablas();
		int option = ElegirOpcion(1, tablas);
		
		System.out.println("Listar todos o una ID?");
		System.out.println("1.- Todos");
		System.out.println("2.- Una ID");
		int option2 = ElegirOpcion(1, 2);
		boolean especifico = option2 == 1 ? false : true;
		
		switch (option)
		{
			case 1:
				DAOArticulo.MenuListarArticulo(especifico);
				break;
				
			case 2:
				DAOCategoria.MenuListarCategoria(especifico);
				break;
				
			case 3:
				DAOCliente.MenuListarCliente(especifico);
				break;
				
			case 4:
				DAOPedido.MenuListarPedido(especifico);
				break;
				
			case 5:
				DAOPedidoLinea.MenuListarPedidoLinea(especifico);
				break;
				
			default:
				break;
		}
	}
	
	public static void MenuAnadir()
	{
		System.out.println("Añadir datos a");
		int tablas = ListaTablas();
		
		int option = ElegirOpcion(1, tablas);
		switch (option)
		{
			case 1:
				DAOArticulo.MenuInsertarArticulo();
				break;
				
			case 2:
				DAOCategoria.MenuInsertarCategoria();
				break;
				
			case 3:
				DAOCliente.MenuInsertarCliente();
				break;
				
			case 4:
				DAOPedido.MenuInsertarPedido();
				break;
				
			case 5:
				DAOPedidoLinea.MenuInsertarPedidoLinea();
				break;
				
			default:
				break;
		}
	}
	
	public static void MenuModificar()
	{
		System.out.println("Modificar datos de");
		int tablas = ListaTablas();
		
		int option = ElegirOpcion(1, tablas);
		
		System.out.println("!!! Deja en blanco (o '-1' en números) para por defecto !!!");
		switch (option)
		{
			case 1:
				DAOArticulo.MenuModificarArticulo();
				break;
			
			case 2:
				DAOCategoria.MenuModificarCategoria();
				break;
			
			case 3:
				DAOCliente.MenuModificarCliente();
				break;
				
			case 4:
				DAOPedido.MenuModificarPedido();
				break;
				
			case 5:
				DAOPedidoLinea.MenuModificarPedidoLinea();
				break;
			
			default:
				break;
		}
	}
	
	public static void MenuEliminar()
	{
		System.out.println("Eliminar datos de");
		int tablas = ListaTablas();
		
		int option = ElegirOpcion(1, tablas);
		switch (option)
		{
			case 1:
				DAOArticulo.MenuEliminarArticulo();
				break;
			
			case 2:
				DAOCategoria.MenuEliminarCategoria();
				break;
			
			case 3:
				DAOCliente.MenuEliminarCliente();
				break;
				
			case 4:
				DAOPedido.MenuEliminarPedido();
				break;
				
			case 5:
				DAOPedidoLinea.MenuEliminarPedidoLinea();
				break;
			
			default:
				break;
		}
	}
	
	private static int ListaTablas()
	{
		System.out.println("1.- Articulo");
		System.out.println("2.- Categoria");
		System.out.println("3.- Cliente");
		System.out.println("4.- Pedido");
		System.out.println("5.- PedidoLinea");
		
		return 5;
	}
	
	private static int ElegirOpcion(int min, int max)
	{
		System.out.print("Elige una opción: ");
		
		int option = sc.nextInt();
		while (option < min || option > max)
		{
			System.out.println("Opcion incorrecta");
			System.out.print("Elige una opción: ");
			option = sc.nextInt();
		}
		
		return option;
	}
}
