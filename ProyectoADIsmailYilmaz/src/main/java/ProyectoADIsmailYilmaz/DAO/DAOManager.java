package ProyectoADIsmailYilmaz.DAO;

import java.math.BigDecimal;
import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceException;
import javax.persistence.QueryTimeoutException;
import javax.persistence.TypedQuery;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;

import org.hibernate.exception.ConstraintViolationException;
import org.hibernate.exception.JDBCConnectionException;

public class DAOManager
{	
	public static <T extends Object> T Get(Class<T> type, long id)
	{
		return type.cast(em.find(type, id));
	}
	
	public static <T extends Object> List<T> GetAll(Class<T> type)
	{
		CriteriaBuilder cb = em.getCriteriaBuilder();
		CriteriaQuery<T> cq = cb.createQuery(type);
		Root<T> rootEntry = cq.from(type);
		CriteriaQuery<T> all = cq.select(rootEntry);
		TypedQuery<T> allQuery = em.createQuery(all);
		return allQuery.getResultList();
	}
	
	public static void Guardar(Object entity)
	{
		try 
		{
			BeginTransaction();
			em.persist(entity);
			CommitTransaction();
			
			System.out.println("Datos guardados con éxito");
		}
		catch (PersistenceException ex)
		{
			System.out.println("Error guardando los datos:");
			HandleException(ex);
		}
	}
	
	public static void Actualizar(Object entity)
	{
		try
		{
			BeginTransaction();
			em.merge(entity);
			CommitTransaction();
			
			System.out.println("Datos actualizados con éxito");
		}
		catch (PersistenceException ex)
		{
			System.out.println("Error actualizando los datos:");
			HandleException(ex);
		}
	}
	
	public static void Borrar(Object entity)
	{
		try
		{
			BeginTransaction();
			em.remove(entity);
			CommitTransaction();
			
			System.out.println("Datos borrados con éxito");
		}
		catch (PersistenceException ex)
		{
			System.out.println("Error borrando datos:");
			HandleException(ex);
		}

	}
	
	private static void BeginTransaction()
	{
		em.getTransaction().begin();
	}
	
	private static void CommitTransaction()
	{
		em.getTransaction().commit();
	}
	
	private static void RollbackTransaction()
	{
		em.getTransaction().rollback();
	}
	
	private static void HandleException(PersistenceException ex)
	{
		Throwable throwable = ex.getCause();
		if (throwable instanceof ConstraintViolationException)
		{
			System.out.println("Los datos no cumplen las restricciones");
		}
		else if (throwable instanceof JDBCConnectionException)
		{
			System.out.println("No se ha podido conectar con la base de datos");
		}
		else if (throwable instanceof QueryTimeoutException)
		{
			System.out.println("Tiempo limite alcanzado para la consulta");
		}
		
		RollbackTransaction();
	}
	
	public static EntityManager em;
	
	public static BigDecimal skipValue = new BigDecimal("-1");
}