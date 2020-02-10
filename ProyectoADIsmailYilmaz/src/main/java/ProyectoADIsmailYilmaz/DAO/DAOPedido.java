package ProyectoADIsmailYilmaz.DAO;

import java.math.BigDecimal;
import java.sql.Timestamp;
import java.util.Date;
import java.util.List;

import ProyectoADIsmailYilmaz.Articulo;
import ProyectoADIsmailYilmaz.Cliente;
import ProyectoADIsmailYilmaz.Main;
import ProyectoADIsmailYilmaz.Pedido;
import ProyectoADIsmailYilmaz.PedidoLinea;

public class DAOPedido
{
	public static void MenuListarPedido(boolean especifico)
	{
		if (especifico)
		{
			System.out.print("Escribe la ID del pedido: ");
			long id = Main.sc.nextLong();
			
			Pedido pedido = DAOManager.Get(Pedido.class, id);
			
			if (pedido == null)
			{
				System.out.println("No se ha encontrado un pedido con ID " + id);
				return;
			}
			
			System.out.println(pedido.toString());
			
			List<PedidoLinea> lineas = pedido.getLineasPedido();
			for (int i = 0; i < lineas.size(); ++i)
			{
				System.out.println(lineas.get(i).toString());
			}
		}
		else
		{
			List<Pedido> pedidos = DAOManager.GetAll(Pedido.class);
			
			if (pedidos.isEmpty())
			{
				System.out.println("No se han encontrado artículos");
				return;
			}
			
			for (int i = 0; i < pedidos.size(); ++i)
			{
				System.out.println(pedidos.get(i).toString());
			}
		}
	}
	
	public static void MenuInsertarPedido()
	{
		System.out.print("Escribe la ID del cliente que ha realizado el pedido: ");
		
		long id = Main.sc.nextLong();
		
		Cliente cliente = DAOManager.Get(Cliente.class, id);
		
		if (cliente == null)
		{
			System.out.println("No se ha encontrado un cliente con ID: " + id);
			return;
		}
		
		Pedido pedido = new Pedido(new Timestamp(new Date().getTime()), cliente);
		
		System.out.print("Cuantas lineas quieres insertar? ");
		int lineas = Main.sc.nextInt();
		while (lineas <= 0)
		{
			System.out.println("Debes insertar al menos 1 línea");
			lineas = Main.sc.nextInt();
		}
		
		PedidoLinea lineaPedido;
		for (int i = 0; i < lineas; ++i)
		{
			System.out.print("Escribe la ID del articulo " + (i+1) + " de la linea: ");
			long idArt = Main.sc.nextLong();
			
			System.out.print("Escribe las unidades: ");
			BigDecimal unidades = Main.sc.nextBigDecimal();
			
			Articulo articulo = DAOManager.Get(Articulo.class, idArt);
			if (articulo == null)
			{
				System.out.println("Articulo no encontrado");
				i--;
				continue;
			}
			
			lineaPedido = new PedidoLinea(pedido, articulo, unidades);
			pedido.addLineaPedido(lineaPedido);
		}
		
		DAOManager.Guardar(pedido);
	}
	
	public static void MenuModificarPedido()
	{
		System.out.print("Escribe la ID del pedido: ");
		
		long id = Main.sc.nextLong();
		Pedido pedido = DAOManager.Get(Pedido.class, id);
		
		if (pedido == null)
		{
			System.out.println("Pedido no encontrado con la ID " + id);
			return;
		}
		
		System.out.print("Escribe el nuevo ID de cliente: ");
		long idCliente = Main.sc.nextLong();
		
		if (idCliente != -1)
		{
			Cliente cliente = DAOManager.Get(Cliente.class, idCliente);
			
			if (cliente == null)
			{
				System.out.println("No se ha encontrado un cliente con ID: " + idCliente);
				return;
			}
			
			pedido.setCliente(cliente);
		}
				
		DAOManager.Actualizar(pedido);
	}
	
	public static void MenuEliminarPedido()
	{
		System.out.print("Escribe la ID del pedido: ");
		
		long id = Main.sc.nextLong();
		Pedido pedido = DAOManager.Get(Pedido.class, id);
		
		if (pedido == null)
		{
			System.out.println("Pedido no encontrado con la ID " + id);
			return;
		}
		
		DAOManager.Borrar(pedido);
	}
}
