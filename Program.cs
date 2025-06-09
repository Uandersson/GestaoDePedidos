using P2_Prova_de_POO;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static ProdutoRepository produtoRepo = new ProdutoRepository();
    static ClienteRepository clienteRepo = new ClienteRepository();
    static PedidoRepository pedidoRepo = new PedidoRepository();

    static ILogger logger = new ConsoleLogger();
    static PedidoFactory pedidoFactory = new PedidoFactory();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1. Cadastrar Produto");
            Console.WriteLine("2. Cadastrar Cliente");
            Console.WriteLine("3. Criar Pedido");
            Console.WriteLine("4. Listar Pedidos");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": CadastrarProduto(); break;
                case "2": CadastrarCliente(); break;
                case "3": CriarPedido(); break;
                case "4": ListarPedidos(); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida!"); break;
            }
        }
    }

    static void CadastrarProduto()
    {
        Console.Write("Nome (somente letras): ");
        string nome = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nome) || !nome.All(char.IsLetter))
        {
            Console.WriteLine("Nome inválido! Use apenas letras.");
            return;
        }

        Console.Write("Preço: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal preco) || preco <= 0)
        {
            Console.WriteLine("Preço inválido!");
            return;
        }

        Console.Write("Categoria (somente letras): ");
        string categoria = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(categoria) || !categoria.All(char.IsLetter))
        {
            Console.WriteLine("Categoria inválida! Use somente letras.");
            return;
        }

        var produto = new Produto(nome, preco, categoria.ToLower());
        produtoRepo.Adicionar(produto);
        logger.Log($"Produto '{nome}' cadastrado com categoria '{categoria}'.");

        Console.WriteLine("\nProdutos cadastrados:");
        var produtos = produtoRepo.ObterTodos();
        for (int i = 0; i < produtos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {produtos[i].Nome} - R${produtos[i].Preco:F2}");
        }
    }

    static void CadastrarCliente()
    {
        Console.Write("Nome (somente letras): ");
        string nome = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nome) || !nome.All(char.IsLetter))
        {
            Console.WriteLine("Nome inválido! Use apenas letras.");
            return;
        }

        Console.Write("Email: ");
        string email = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
        {
            Console.WriteLine("Email inválido!");
            return;
        }

        Console.Write("CPF (somente números): ");
        string cpf = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(cpf) || !cpf.All(char.IsDigit) || cpf.Length != 11)
        {
            Console.WriteLine("CPF inválido! Use apenas números (11 dígitos).");
            return;
        }

        var cliente = new Cliente(nome, email, cpf);
        clienteRepo.Adicionar(cliente);
        logger.Log($"Cliente '{nome}' cadastrado.");

        Console.WriteLine("\nClientes cadastrados:");
        var clientes = clienteRepo.ObterTodos();
        for (int i = 0; i < clientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {clientes[i].Nome} - {clientes[i].Email}");
        }
    }

    static void CriarPedido()
    {
        var clientes = clienteRepo.ObterTodos().ToList();
        var produtos = produtoRepo.ObterTodos().ToList();

        if (!clientes.Any() || !produtos.Any())
        {
            Console.WriteLine("Cadastre ao menos 1 cliente e 1 produto.");
            return;
        }

        Console.WriteLine("\nSelecione o cliente:");
        for (int i = 0; i < clientes.Count; i++)
            Console.WriteLine($"{i + 1}. {clientes[i].Nome}");

        if (!int.TryParse(Console.ReadLine(), out int indiceCliente) || indiceCliente < 1 || indiceCliente > clientes.Count)
        {
            Console.WriteLine("Cliente inválido!");
            return;
        }
        var cliente = clientes[indiceCliente - 1];

        var itens = new List<ItemPedido>();
        while (true)
        {
            Console.WriteLine("\nSelecione o produto (0 para finalizar):");
            for (int i = 0; i < produtos.Count; i++)
                Console.WriteLine($"{i + 1}. {produtos[i].Nome} - R${produtos[i].Preco:F2}");

            if (!int.TryParse(Console.ReadLine(), out int indiceProduto)) continue;
            if (indiceProduto == 0) break;
            if (indiceProduto < 1 || indiceProduto > produtos.Count)
            {
                Console.WriteLine("Produto inválido!");
                continue;
            }

            var produto = produtos[indiceProduto - 1];

            Console.Write("Quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0)
            {
                Console.WriteLine("Quantidade inválida!");
                continue;
            }

            itens.Add(new ItemPedido(produto, qtd));
        }

        if (!itens.Any())
        {
            Console.WriteLine("Nenhum item selecionado. Pedido cancelado.");
            return;
        }

        Console.WriteLine("\nEscolha a estratégia de desconto:");
        Console.WriteLine("1 - Por Quantidade (5% para 5 ou mais unidades)");
        Console.WriteLine("2 - Sem desconto");
        string opcao = Console.ReadLine();

        IDescontoStrategy desconto;
        switch (opcao)
        {
            case "1":
                desconto = new DescontoPorQuantidade();
                break;
            default:
                desconto = null;
                break;
        }
        var pedido = pedidoFactory.CriarPedido(cliente, itens, desconto);
        pedidoRepo.Adicionar(pedido);
        logger.Log($"Pedido #{pedido.Id} criado. Total: R${pedido.CalcularTotal():F2}");
    }
    static void ListarPedidos()
    {
        var pedidos = pedidoRepo.ObterTodos();
        if (!pedidos.Any())
        {
            Console.WriteLine("Nenhum pedido cadastrado.");
            return;
        }

        foreach (var p in pedidos)
        {
            Console.WriteLine($"\nPedido #{p.Id} - Cliente: {p.Cliente.Nome} - Data: {p.Data}");
            Console.WriteLine($"Total com desconto: R${p.CalcularTotal():F2}");
            Console.WriteLine("Itens:");
            foreach (var item in p.Itens)
            {
                Console.WriteLine($"  {item.Produto.Nome} x{item.Quantidade} - R${item.Subtotal():F2}");
            }
        }
    }
}
