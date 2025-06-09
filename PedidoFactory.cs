using System;
using System.Collections.Generic;

namespace P2_Prova_de_POO
{
    public class PedidoFactory
    {
        private readonly ILogger logger;

        public PedidoFactory() { }

        public PedidoFactory(ILogger logger)
        {
            this.logger = logger;
        }

        public Pedido CriarPedido(Cliente cliente, List<ItemPedido> itens, IDescontoStrategy descontoStrategy)
        {
            return new Pedido
            {
                Cliente = cliente,
                Itens = itens,
                DescontoStrategy = descontoStrategy,
                Data = DateTime.Now
            };
        }
    }
}
