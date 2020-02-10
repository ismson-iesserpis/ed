package ProyectoADIsmailYilmaz.DAO;

import java.math.BigDecimal;
import java.util.List;

import ProyectoADIsmailYilmaz.Articulo;
import ProyectoADIsmailYilmaz.Categoria;
import ProyectoADIsmailYilmaz.Main;

public class DAOArticulo
{
	public static void MenuListarArticulo(boolean especifico)
	{
		if (especifico)
		{
			System.out.print("Escribe la ID del articulo: ");
			long id = Main.sc.nextLong();
			
			Articulo articulo = DAOManager.Get(Articulo.class, id);
			
			if (articulo == null)
			{
				System.out.println("No se ha encontrado un artículo con ID " + id);
			}
			
			System.out.println(articulo.toString());
		}
		else
		{
			List<Articulo> articulos = DAOManager.GetAll(Articulo.class);
			
			if (articulos.isEmpty())
			{
				System.out.println("No se han encontrado artículos");
				return;
			}
			
			for (int i = 0; i < articulos.size(); ++i)
			{
				System.out.println(articulos.get(i).toString());
			}
		}
	}
	
	public static void MenuInsertarArticulo()
	{
		System.out.print("Escribe el nombre del articulo: ");
		
		if (Main.sc.hasNextLine())
			Main.sc.nextLine();
		
		String nombre = Main.sc.nextLine();
		
		System.out.print("Escribe el precio del articulo: ");
		BigDecimal precio = Main.sc.nextBigDecimal();
		
		System.out.print("Escribe la ID de la categoría: ");
		long id = Main.sc.nextLong();
		
		Categoria categoria = DAOManager.Get(Categoria.class, id);
		
		if (categoria == null)
		{
			System.out.println("No se ha encontrado una categoría con ID: " + id);
			return;
		}
		
		Articulo articulo = new Articulo(nombre, precio, categoria);
		DAOManager.Guardar(articulo);
	}
	
	public static void MenuModificarArticulo()
	{
		System.out.println("Escribe la ID del articulo: ");
		long id = Main.sc.nextLong();
		
		Articulo articulo = DAOManager.Get(Articulo.class, id);
		
		if (articulo == null)
		{
			System.out.println("No se ha encontrado un artículo con ID: " + id);
			return;
		}
		
		System.out.print("Escribe el nuevo nombre del articulo: ");
		
		if (Main.sc.hasNextLine())
			Main.sc.nextLine();
		
		String nombre = Main.sc.nextLine();
		if (!nombre.isEmpty())
			articulo.setNombre(nombre);
		
		System.out.print("Escribe el nuevo precio del articulo: ");
		BigDecimal precio = Main.sc.nextBigDecimal();
		
		if (precio.compareTo(DAOManager.skipValue) != 0)
			articulo.setPrecio(precio);
		
		System.out.print("Escribe la nueva ID de la categoría: ");
		long idCat = Main.sc.nextLong();
		
		Categoria categoria = DAOManager.Get(Categoria.class, idCat);
		
		if (categoria == null)
		{
			System.out.println("No se ha encontrado una categoría con ID " + idCat);
			return;
		}
		
		articulo.setCategoria(categoria);
				
		DAOManager.Actualizar(articulo);
	}
	
	public static void MenuEliminarArticulo()
	{
		System.out.println("Escribe la ID del articulo: ");
		
		long id = Main.sc.nextLong();
		Articulo articulo = DAOManager.Get(Articulo.class, id);
		
		if (articulo == null)
		{
			System.out.println("No se ha encontrado una categoría con ID " + id);
			return;
		}
		
		DAOManager.Borrar(articulo);
	}
}
