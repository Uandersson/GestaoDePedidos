using System.Collections.Generic;
using System.Linq;

namespace P2_Prova_de_POO
{
    public class PedidoRepository : IRepository<Pedido>
    {
        private readonly List<Pedido> _pedidos = new List<Pedido>();

        public void Adicionar(Pedido pedido) => _pedidos.Add(pedido);

        public Pedido ObterPorId(int id) => _pedidos.FirstOrDefault(p => p.Id == id);

        public List<Pedido> ObterTodos() => new List<Pedido>(_pedidos);
    }
}
