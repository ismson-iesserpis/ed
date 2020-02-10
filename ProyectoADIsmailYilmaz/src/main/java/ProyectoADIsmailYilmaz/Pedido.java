package ProyectoADIsmailYilmaz;

import java.math.BigDecimal;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.ForeignKey;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;

@Entity(name = "Pedido")
public class Pedido
{	
	public Pedido()
	{
		
	}
	
	public Pedido(Timestamp fecha, Cliente cliente)
	{
		setFecha(fecha);
		setCliente(cliente);
		setImporte(new BigDecimal(0));
	}
	
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	
	@Column(name = "fecha", nullable = false, unique = true)
	private Timestamp fecha;
	
	@ManyToOne
	@JoinColumn(name = "cliente", nullable = false, foreignKey = @ForeignKey(name = "CLIENTE_ID_FK"))
	private Cliente cliente;
	
	@Column(name = "importe", nullable = false, scale = 2, precision = 10)
	private BigDecimal importe;
	
	@OneToMany(mappedBy = "pedido", cascade = CascadeType.ALL, orphanRemoval = true)
	private List<PedidoLinea> lineasPedido = new ArrayList<>();
	
	public long getId() { return id; }
	public void setId(long id) { this.id = id; }
	
	public Timestamp getFecha() { return fecha; }
	public void setFecha(Timestamp fecha) { this.fecha = fecha; }
	
	public Cliente getCliente() { return cliente; }
	public void setCliente(Cliente cliente) { this.cliente = cliente; }
	
	public BigDecimal getImporte() { return importe; }
	public void setImporte(BigDecimal importe) { this.importe = importe; }
	
	public List<PedidoLinea> getLineasPedido() { return lineasPedido; }
	public void addLineaPedido(PedidoLinea lineaPedido)
	{
		setImporte(getImporte().add(lineaPedido.getImporte()));
		
		lineasPedido.add(lineaPedido);
	}
	public void updateLineaPedido(int index, PedidoLinea nuevaLinea, BigDecimal importeAntes)
	{
		BigDecimal importeDsps = nuevaLinea.getImporte();
		BigDecimal diff = importeAntes.subtract(importeDsps).negate();
		
		setImporte(getImporte().add(diff));
		
		lineasPedido.set(index, nuevaLinea);
	}
	public void removeLineaPedido(PedidoLinea linea)
	{
		setImporte(getImporte().subtract(linea.getImporte()));
		
		lineasPedido.remove(linea);
	}
	
	@Override
	public String toString()
	{
		String str = "--- Pedido ID: " + getId() + " ---\n";
		
		str += "Fecha: " + getFecha().toString() + "\n";
		str += "Cliente: " + getCliente().getNombre() + " (" + getCliente().getId() + ")\n";
		str += "Importe: " + getImporte() + "\n";
		
		return str;
	}
}
