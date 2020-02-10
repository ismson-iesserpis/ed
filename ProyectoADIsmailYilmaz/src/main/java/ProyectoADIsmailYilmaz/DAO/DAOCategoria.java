package ProyectoADIsmailYilmaz.DAO;

import java.util.List;

import ProyectoADIsmailYilmaz.Categoria;
import ProyectoADIsmailYilmaz.Main;

public class DAOCategoria
{
	public static void MenuListarCategoria(boolean especifico)
	{
		if (especifico)
		{
			System.out.print("Escribe la ID de la categoria: ");
			long id = Main.sc.nextLong();
			
			Categoria categoria = DAOManager.Get(Categoria.class, id);
			
			if (categoria == null)
			{
				System.out.println("No se ha encontrado una categoria con ID " + id);
			}
			
			System.out.println(categoria.toString());
		}
		else
		{
			List<Categoria> categorias = DAOManager.GetAll(Categoria.class);
			
			if (categorias.isEmpty())
			{
				System.out.println("No se han encontrado categorías");
				return;
			}
			
			for (int i = 0; i < categorias.size(); ++i)
			{
				System.out.println(categorias.get(i).toString());
			}
		}
	}
	
	public static void MenuInsertarCategoria()
	{
		System.out.print("Escribe el nombre de la categoria: ");
		
		if (Main.sc.hasNextLine())
			Main.sc.nextLine();
		
		String nombre = Main.sc.nextLine();
		
		Categoria categoria = new Categoria(nombre);
		
		DAOManager.Guardar(categoria);
	}
	
	public static void MenuModificarCategoria()
	{
		System.out.print("Escribe la ID de la categoria: ");
		
		long id = Main.sc.nextLong();
		Categoria categoria = DAOManager.Get(Categoria.class, id);
		
		if (categoria == null)
		{
			System.out.println("No se ha encontrado una categoría con ID " + id);
			return;
		}
		
		System.out.print("Escribe el nuevo nombre de la categoria: ");
		
		if (Main.sc.hasNextLine())
			Main.sc.nextLine();
		
		String nombre = Main.sc.nextLine();
		
		if (!nombre.isEmpty())
			categoria.setNombre(nombre);
		
		DAOManager.Actualizar(categoria);
	}
	
	public static void MenuEliminarCategoria()
	{
		System.out.println("Escribe la ID de la categoria: ");
		
		long id = Main.sc.nextLong();
		Categoria categoria = DAOManager.Get(Categoria.class, id);
		
		if (categoria == null)
		{
			System.out.println("No se ha encontrado una categoría con ID " + id);
			return;
		}
		
		DAOManager.Borrar(categoria);
	}
}
