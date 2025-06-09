using System.Collections.Generic;
using System.Linq;

namespace P2_Prova_de_POO
{
    public class ProdutoRepository : IRepository<Produto>
    {
        private readonly List<Produto> _produtos = new List<Produto>();

        public void Adicionar(Produto produto) => _produtos.Add(produto);

        public Produto ObterPorId(int id) => _produtos.FirstOrDefault(p => p.Id == id);

        public List<Produto> ObterTodos() => new List<Produto>(_produtos);
    }
}
