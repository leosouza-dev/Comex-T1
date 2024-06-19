namespace Comex.Modelos;

/// <summary>
/// Representa um pedido realizado por um cliente.
/// </summary>
public class Pedido
{
    /// <summary>
    /// Inicializa uma nova instância da classe Pedido com um cliente especófico.
    /// </summary>
    /// <param name="cliente">O cliente que realizou o pedido.</param>
    public Pedido(Cliente cliente)
    {
        Cliente = cliente;
        Data = DateTime.Now;
        Itens = new List<ItemDePedido>();
    }

    /// <summary>
    /// Obtém o cliente que realizou o pedido.
    /// </summary>
    public Cliente Cliente { get; private set; }
    /// <summary>
    /// Obtém a data em que o pedido foi realizado.
    /// </summary>
    public DateTime Data { get; private set; }
    /// <summary>
    /// Obtém a lista de ítens de pedido.
    /// </summary>
    public List<ItemDePedido> Itens { get; private set; }
    /// <summary>
    /// Obtém o valor total do pedido.
    /// </summary>
    public double Total { get; private set; }

    /// <summary>
    /// Adiciona um item ao pedido e atualiza o valor total.
    /// </summary>
    /// <param name="item">O item a ser adicionado ao pedido.</param>
    public void AdicionarItem(ItemDePedido item)
    {
        Itens.Add(item);
        Total += item.SubTotal;
    }

    /// <summary>
    /// Retorna uma string que representa o pedido atual.
    /// </summary>
    /// <returns>Uma string que representa o pedido atual.</returns>
    public override string ToString()
    {
        return $"Cliente: {Cliente.Nome}, Data: {Data}, Total: {Total:F2}";
    }
}
