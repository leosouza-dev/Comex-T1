using Comex.Modelos;

namespace Comex.Tests.Modelos
{
    public class PedidoTests
    {
        [Fact]
        public void PedidoDeveInicializarComClienteEDataCorreta()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Leo" };

            // Act
            var pedido = new Pedido(cliente);

            // Assert
            //Assert.Equal(DateTime.Now, pedido.Data);
            Assert.True((DateTime.Now - pedido.Data).TotalSeconds < 1);
            Assert.Equal(cliente, pedido.Cliente);
            Assert.Empty(pedido.Itens);
            Assert.Equal(0, pedido.Total);
        }

        [Theory]
        [InlineData("Produto A", 100.0, 2)]
        [InlineData("Produto B", 200.0, 1)]
        [InlineData("Produto C", 300.0, 3)]
        public void AdiconarItemDeveAdicionarItemEAtualizarTotal(string nomeProduto, double precoUnitario, int quantidade)
        {
            // Arrange
            var cliente = new Cliente { Nome = "Leo" };
            var pedido = new Pedido(cliente);
            var produto = new Produto(nomeProduto) { PrecoUnitario = precoUnitario };
            var itemDePedido = new ItemDePedido(produto, quantidade);
            var esperadoTotal = precoUnitario * quantidade;

            // Act
            pedido.AdicionarItem(itemDePedido);

            // Assert
            Assert.Contains(itemDePedido, pedido.Itens);
            Assert.Equal(esperadoTotal, pedido.Total);
        }

        [Fact]
        public void ToStringDeveRetornarStringCorreta()
        {
            // Arrange
            var cliente = new Cliente();
            cliente.Nome = "Jao";

            var pedido = new Pedido(cliente);

            var produto = new Produto("Produto A");
            produto.PrecoUnitario = 100.0;

            var item = new ItemDePedido(produto, 2);

            pedido.AdicionarItem(item);

            var stringEsperada = $"Cliente: {pedido.Cliente.Nome}, Data: {pedido.Data}, Total: {pedido.Total:F2}";

            // Act
            var resultado = pedido.ToString();

            // Assert
            Assert.Equal(stringEsperada, resultado);
        }
    }
}