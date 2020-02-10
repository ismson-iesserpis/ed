package ProyectoADIsmailYilmaz;

import java.math.BigDecimal;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.ForeignKey;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;

@Entity(name = "Articulo")
public class Articulo
{
	public Articulo()
	{
		
	}
	
	public Articulo(String nombre, BigDecimal precio, Categoria categoria)
	{
		setNombre(nombre);
		setPrecio(precio);
		setCategoria(categoria);
	}
	
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	
	@Column(name = "nombre", length = 50, nullable = false, unique = true)
	private String nombre;
	
	@Column(name = "precio", scale = 2, precision = 10)
	private BigDecimal precio;
	
	@ManyToOne
	@JoinColumn(name = "categoria", foreignKey = @ForeignKey(name = "CATEGORIA_ID_FK"))
	private Categoria categoria;
	
	public long getId() { return id; }
	public void setId(long id) { this.id = id; }
	
	public String getNombre() { return nombre; }
	public void setNombre(String nombre) { this.nombre = nombre; }
	
	public BigDecimal getPrecio() { return precio; }
	public void setPrecio(BigDecimal precio) { this.precio = precio; }
	
	public Categoria getCategoria() { return categoria; }
	public void setCategoria(Categoria categoria) { this.categoria = categoria; }
	
	@Override
	public String toString()
	{
		String str = "--- Articulo ID: " + getId() + " ---\n";
		
		str += "Nombre: " + getNombre() + "\n";
		str += "Precio: " + getPrecio() + "\n";
		str += "Categoria: " + getCategoria().getNombre() + " (" + getCategoria().getId() + ")" + "\n";
		
		return str;
	}
}
