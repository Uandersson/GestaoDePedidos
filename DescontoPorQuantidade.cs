using System.Linq;

namespace P2_Prova_de_POO
{
    public class DescontoPorQuantidade : IDescontoStrategy
    {
        public decimal AplicarDesconto(Pedido pedido)
        {
            return pedido.Itens
                .Where(i => i.Quantidade >= 5)
                .Sum(i => i.Subtotal() * 0.05m);
        }
    }
}
