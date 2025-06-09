using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_Prova_de_POO
{
    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        public DateTime Data { get; set; } = DateTime.Now;
        public IDescontoStrategy DescontoStrategy { get; set; }

        public decimal CalcularTotal()
        {
            var total = Itens.Sum(item => item.Subtotal());
            var desconto = DescontoStrategy?.AplicarDesconto(this) ?? 0;
            return total - desconto;
        }
    }
}
