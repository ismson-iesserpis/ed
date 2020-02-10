package ProyectoADIsmailYilmaz;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity(name = "Cliente")
public class Cliente
{
	public Cliente()
	{
		
	}
	
	public Cliente(String nombre)
	{
		setNombre(nombre);
	}
	
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	
	@Column(name = "nombre", length = 50, nullable = false, unique = true)
	private String nombre;
	
	public long getId() { return id; }
	public void setId(Long id) { this.id = id; }
	
	public String getNombre() { return nombre; }
	public void setNombre(String nombre) { this.nombre = nombre; }
	
	@Override
	public String toString()
	{
		String str = "--- Cliente ID: " + getId() + " ---\n";
		
		str += "Nombre: " + getNombre() + "\n";
		
		return str;
	}
}
