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
import javax.persistence.OneToOne;

@Entity(name = "PedidoLinea")
public class PedidoLinea
{
	public PedidoLinea()
	{
		
	}
	
	public PedidoLinea(Pedido pedido, Articulo articulo, BigDecimal unidades)
	{
		setPedido(pedido);
		setArticulo(articulo);
		setPrecio(articulo.getPrecio());
		setUnidades(unidades);
		setImporte(getPrecio().multiply(unidades));
	}
	
	public PedidoLinea(Pedido pedido, Articulo articulo, BigDecimal precio, BigDecimal unidades)
	{
		setPedido(pedido);
		setArticulo(articulo);
		setPrecio(precio);
		setUnidades(unidades);
		setImporte(precio.multiply(unidades));
	}
	
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	
	@ManyToOne
	@JoinColumn(name = "pedido", nullable = false, foreignKey = @ForeignKey(name = "PEDIDO_ID_FK"))
	private Pedido pedido;
	
	@OneToOne
	@JoinColumn(name = "articulo", nullable = false, foreignKey = @ForeignKey(name = "ARTICULO_ID_FK"))
	private Articulo articulo;
	
	@Column(name = "precio", nullable = false, scale = 2, precision = 10)
	private BigDecimal precio;
	
	@Column(name = "unidades", nullable = false, scale = 2, precision = 10)
	private BigDecimal unidades;
	
	@Column(name = "importe", nullable = false, scale = 2, precision = 10)
	private BigDecimal importe;
	
	public long getId() { return id; }
	public void setId(long id) { this.id = id; }
	
	public Pedido getPedido() { return pedido; }
	public void setPedido(Pedido pedido) { this.pedido = pedido; }
	
	public Articulo getArticulo() { return articulo; }
	public void setArticulo(Articulo articulo) { this.articulo = articulo; }
	
	public BigDecimal getPrecio() { return precio; }
	public void setPrecio(BigDecimal precio) { this.precio = precio; }
	
	public BigDecimal getUnidades() { return unidades; }
	public void setUnidades(BigDecimal unidades) { this.unidades = unidades; }
	
	public BigDecimal getImporte() { return importe; }
	public void setImporte(BigDecimal importe) { this.importe = importe; }
	
	@Override
	public String toString()
	{
		String str = "--- Linea pedido ID: " + getId() + " ---\n";
		
		str += "Pedido: " + getPedido().getId() + "\n";
		str += "Articulo: " + getArticulo().getId() + "\n";
		str += "Precio: " + getPrecio() + "\n";
		str += "Unidades: " + getUnidades() + "\n";
		str += "Importe: " + getImporte() + "\n";
		
		return str;
	}
}
