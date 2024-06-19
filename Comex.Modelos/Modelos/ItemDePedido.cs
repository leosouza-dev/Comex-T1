namespace Comex.Modelos;

/// <summary>
/// Representa um item de um pedido.
/// </summary>
public class ItemDePedido
{
    /// <summary>
    /// Inicializa uma nova instância da classe ItemDePedido com um produto e uma quantidade específica.
    /// </summary>
    /// <param name="produto">O produto do item do pedido.</param>
    /// <param name="quantidade">A quantidade do produto no item do pedido.</param>
    public ItemDePedido(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
        PrecoUnitario = produto.PrecoUnitario;
        SubTotal = quantidade * produto.PrecoUnitario;
    }

    /// <summary>
    /// Obtém o produto do item de pedido.
    /// </summary>
    public Produto Produto { get; private set; }
    /// <summary>
    /// Obtém a quantidade do produto no item de pedido.
    /// </summary>
    public int Quantidade { get; private set; }

    /// <summary>
    /// Obtém o preço unitário do produto.
    /// </summary>
    public double PrecoUnitario { get; private set; }

    /// <summary>
    /// Obtém o subtotal do item de pedido calculado como quantidade vezes o preço unitário.
    /// </summary>
    public double SubTotal { get; private set; }

    /// <summary>
    /// Retorna uma string que representa o item de pedido atual.
    /// </summary>
    /// <returns>Uma string que representa o item de pedido atual.</returns>
    public override string ToString()
    {
        return $"Produto: {Produto.Nome}, Quantidade: {Quantidade}, " +
            $"Preço Unitário: {PrecoUnitario:F2}, Subtotal: {SubTotal}";
    }
}
