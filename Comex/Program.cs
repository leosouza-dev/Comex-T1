// See https://aka.ms/new-console-template for more information
using Comex.Modelos;
using System.Text.Json;

var listaPedidos = new List<Pedido>();
var listaProdutos = new List<Produto>
{
    new Produto("Notebook")
    {
        Descricao = "Notebook Dell Inspiron",
        PrecoUnitario = 3500.00,
        Quantidade = 10
    },
    new Produto("Smartphone")
    {
        Descricao = "Smartphone Samsung Galaxy",
        PrecoUnitario = 1200.00,
        Quantidade = 25
    },
    new Produto("Monitor")
    {
        Descricao = "Monitor LG Ultrawide",
        PrecoUnitario = 800.00,
        Quantidade = 15
    },
    new Produto("Teclado")
    {
        Descricao = "Teclado Mecânico RGB",
        PrecoUnitario = 250.00,
        Quantidade = 50
    }
};

void ExibirLogo()
{
    Console.WriteLine(@"
────────────────────────────────────────────────────────────────────────────────────────
─██████████████─██████████████─██████──────────██████─██████████████─████████──████████─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██████████████░░██─██░░░░░░░░░░██─██░░░░██──██░░░░██─
─██░░██████████─██░░██████░░██─██░░░░░░░░░░░░░░░░░░██─██░░██████████─████░░██──██░░████─
─██░░██─────────██░░██──██░░██─██░░██████░░██████░░██─██░░██───────────██░░░░██░░░░██───
─██░░██─────────██░░██──██░░██─██░░██──██░░██──██░░██─██░░██████████───████░░░░░░████───
─██░░██─────────██░░██──██░░██─██░░██──██░░██──██░░██─██░░░░░░░░░░██─────██░░░░░░██─────
─██░░██─────────██░░██──██░░██─██░░██──██████──██░░██─██░░██████████───████░░░░░░████───
─██░░██─────────██░░██──██░░██─██░░██──────────██░░██─██░░██───────────██░░░░██░░░░██───
─██░░██████████─██░░██████░░██─██░░██──────────██░░██─██░░██████████─████░░██──██░░████─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██──────────██░░██─██░░░░░░░░░░██─██░░░░██──██░░░░██─
─██████████████─██████████████─██████──────────██████─██████████████─████████──████████─
────────────────────────────────────────────────────────────────────────────────────────");
    Console.WriteLine("\nBem vindo ao COMEX");
}

async Task ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 Criar Produto");
    Console.WriteLine("Digite 2 Listar Produtos");
    Console.WriteLine("Digite 3 Consultar API Externa");
    Console.WriteLine("Digite 4 Criar Pedido");
    Console.WriteLine("Digite 5 Listar Pedidos");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine();
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    switch (opcaoEscolhidaNumerica) 
    { 
        case 1:
            CriarProduto();
            break;
        case 2:
            ListarProdutos();
            break;
        case 3:
            await ConsultarApiExterna();
            break;
        case 4:
            await CriarPedido();
            break;
        case 5:
            await ListarPedidos();
            break;
        case -1:
            Console.WriteLine("Saindo!");
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }
}



async Task CriarPedido()
{
    Console.Clear();
    Console.WriteLine("Criando um novo Pedido\n");

    Console.WriteLine("Digite o nome do Cliente: ");
    string nomeCliente = Console.ReadLine();
    var cliente = new Cliente();
    cliente.Nome = nomeCliente;

    var pedido = new Pedido(cliente);

    Console.WriteLine("\nProdutos disponíveis: ");
    for (int i = 0; i < listaProdutos.Count; i++)
    {
        Console.WriteLine($"{i + 1} - {listaProdutos[i].Nome}");
    }

    Console.WriteLine("Digite o número do produto que deseja adicionar: ");
    int numeroProduto = int.Parse( Console.ReadLine() );

    Produto produtoEscolhido = listaProdutos[numeroProduto - 1];

    Console.WriteLine("Digite a Quantidade: ");
    int quantidadeProduto = int.Parse( Console.ReadLine() );

    var itemPedido = new ItemDePedido(produtoEscolhido, quantidadeProduto);

    pedido.AdicionarItem(itemPedido);
    Console.WriteLine($"Item Adicionado com sucesso: {itemPedido}\n");

    listaPedidos.Add(pedido);
    Console.WriteLine($"\nPedido criado com sucesso: {pedido}\n");

    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    await ExibirOpcoesDoMenu();
}



async Task ListarPedidos()
{
    Console.Clear();
    Console.WriteLine("Exibindo todos os Pedidos na nossa aplicação");

    var pedidosOrdanados = listaPedidos.OrderBy(p => p.Cliente.Nome).ToList();

    foreach (var pedido in pedidosOrdanados)
    {
        Console.WriteLine($"\nCliente: {pedido.Cliente.Nome}, Total: {pedido.Total:F2}");
    }

    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    await ExibirOpcoesDoMenu();
}

async Task ConsultarApiExterna()
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            Console.Clear();
            Console.WriteLine("\nExibindo Produtos da Api Externa\n");
            string response = await client.GetStringAsync("https://fakestoreapi.com/products");
            var produtos = JsonSerializer.Deserialize<List<Produto>>(response);

            foreach (var produto in produtos)
            {
                Console.WriteLine($"\nNome: {produto.Nome}, " +
                    $"Descrição: {produto.Descricao}, " +
                    $"Preço: {produto.PrecoUnitario}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Temos um problema ao buscar a API externa: {ex.Message}");
        }
    }
}

async Task CriarProduto()
{
    Console.Clear();
    Console.WriteLine("Registro de Produtos");

    Console.WriteLine("\nDigite o nome do Produto: ");
    string nomeDoProduto = Console.ReadLine();
    var produto = new Produto(nomeDoProduto);

    Console.WriteLine("\nDigite a descrição do Produto: ");
    string descricao = Console.ReadLine();
    produto.Descricao = descricao;

    Console.WriteLine("\nDigite o preço do Produto: ");
    string preco = Console.ReadLine();
    produto.PrecoUnitario = double.Parse(preco);

    Console.WriteLine("\nDigite a quantidade do Produto: ");
    string quantidade = Console.ReadLine();
    produto.Quantidade = int.Parse(quantidade);

    listaProdutos.Add(produto);
    Console.WriteLine($"\nO Produto {produto.Nome} foi registrado com sucesso!");
    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    await ExibirOpcoesDoMenu();
}

async Task ListarProdutos()
{
    Console.Clear();
    Console.WriteLine("Listagem de Produtos");

    foreach (var produto in listaProdutos)
    {
        Console.WriteLine($"Produto: {produto.Nome}, " +
            $"Preço: {produto.PrecoUnitario}, " +
            $"Quantidade: {produto.Quantidade}, " +
            $"Descrição: {produto.Descricao}");
    }

    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
    await ExibirOpcoesDoMenu();
}

await ExibirOpcoesDoMenu();