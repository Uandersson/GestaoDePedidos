using System;

namespace P2_Prova_de_POO
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }

        public Produto(int id, string nome, decimal preco, string categoria)
        {
            if (preco <= 0) throw new ArgumentException("Preço inválido!");
            Id = id;
            Nome = nome;
            Preco = preco;
            Categoria = categoria;
        }

        public Produto(string nome, decimal preco, string categoria)
        {
            Nome = nome;
            Preco = preco;
            Categoria = categoria;
        }
    }
}
