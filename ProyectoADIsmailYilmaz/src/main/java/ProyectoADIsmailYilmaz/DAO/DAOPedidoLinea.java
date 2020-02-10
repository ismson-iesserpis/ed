package ProyectoADIsmailYilmaz.DAO;

import java.math.BigDecimal;
import java.util.List;

import ProyectoADIsmailYilmaz.Articulo;
import ProyectoADIsmailYilmaz.Main;
import ProyectoADIsmailYilmaz.Pedido;
import ProyectoADIsmailYilmaz.PedidoLinea;

public class DAOPedidoLinea
{
	public static void MenuListarPedidoLinea(boolean especifico)
	{
		if (especifico)
		{
			System.out.print("Escribe la ID de la línea: ");
			long id = Main.sc.nextLong();
			
			PedidoLinea linea = DAOManager.Get(PedidoLinea.class, id);
			
			if (linea == null)
			{
				System.out.println("No se ha encontrado una línea con ID " + id);
				return;
			}
			
			System.out.println(linea.toString());
		}
		else
		{
			List<PedidoLinea> lineas = DAOManager.GetAll(PedidoLinea.class);
			
			if (lineas.isEmpty())
			{
				System.out.println("No se han encontrado líneas");
				return;
			}
			
			for (int i = 0; i < lineas.size(); ++i)
			{
				System.out.println(lineas.get(i).toString());
			}
		}
	}
	
	public static void MenuInsertarPedidoLinea()
	{
		System.out.print("Escribe la ID del pedido al que pertenecerá la linea: ");
		
		long idPedido = Main.sc.nextLong();
		Pedido pedido = DAOManager.Get(Pedido.class, idPedido);
		
		if (pedido == null)
		{
			System.out.println("No se ha encontrado un pedido con la ID " + idPedido);
			return;
		}
		
		System.out.print("Escribe la ID del articulo de la linea: ");
		
		long idArticulo = Main.sc.nextLong();
		Articulo articulo = DAOManager.Get(Articulo.class, idArticulo);
		
		if (articulo == null)
		{
			System.out.println("No se ha encontrado un articulo con la ID " + idArticulo);
			return;
		}
		
		System.out.print("Introduce las unidades del articulo: ");
		BigDecimal unidades = Main.sc.nextBigDecimal();
		
		PedidoLinea lineaPedido = new PedidoLinea(pedido, articulo, unidades);
		pedido.addLineaPedido(lineaPedido);
		
		DAOManager.Guardar(pedido);
	}
	
	public static void MenuModificarPedidoLinea()
	{
		System.out.print("Escribe la ID de la linea del pedido: ");
		
		long id = Main.sc.nextLong();
		PedidoLinea lineaPedido = DAOManager.Get(PedidoLinea.class, id);
		
		if (lineaPedido == null)
		{
			System.out.println("Linea de pedido no encontrada con la ID " + id);
			return;
		}
		
		BigDecimal importeOriginal = lineaPedido.getImporte();
		Pedido pedidoOriginal = lineaPedido.getPedido();
		int indexOriginal = pedidoOriginal.getLineasPedido().indexOf(lineaPedido);

		System.out.print("Escribe la ID del nuevo articulo de la linea: ");
		
		long idArticulo = Main.sc.nextLong();
		
		if (idArticulo != -1)
		{
			Articulo articulo = DAOManager.Get(Articulo.class, idArticulo);
			
			if (articulo == null)
			{
				System.out.println("No se ha encontrado un articulo con la ID " + idArticulo);
				return;
			}
			
			lineaPedido.setArticulo(articulo);
		}
		
		System.out.print("Introduce las unidades del articulo: ");
		BigDecimal unidades = Main.sc.nextBigDecimal();
		
		if (unidades.compareTo(DAOManager.skipValue) != 0)
			lineaPedido.setUnidades(unidades);
		
		System.out.println(lineaPedido.getArticulo().getPrecio() + " * " + lineaPedido.getUnidades());
		lineaPedido.setImporte(lineaPedido.getArticulo().getPrecio().multiply(lineaPedido.getUnidades()));
		
		pedidoOriginal.updateLineaPedido(indexOriginal, lineaPedido, importeOriginal);
		DAOManager.Actualizar(pedidoOriginal);
	}
	
	public static void MenuEliminarPedidoLinea()
	{
		System.out.print("Escribe la ID de la linea del pedido: ");
		
		long id = Main.sc.nextLong();
		PedidoLinea lineaPedido = DAOManager.Get(PedidoLinea.class, id);
		
		if (lineaPedido == null)
		{
			System.out.println("Linea de pedido no encontrada con la ID " + id);
			return;
		}
		
		lineaPedido.getPedido().removeLineaPedido(lineaPedido);
		DAOManager.Borrar(lineaPedido);
	}
}
