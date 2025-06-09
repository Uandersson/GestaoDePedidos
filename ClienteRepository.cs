using System.Collections.Generic;
using System.Linq;

namespace P2_Prova_de_POO
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly List<Cliente> _clientes = new List<Cliente>();

        public void Adicionar(Cliente cliente) => _clientes.Add(cliente);

        public Cliente ObterPorId(int id) => _clientes.FirstOrDefault(c => c.Id == id);

        public List<Cliente> ObterTodos() => new List<Cliente>(_clientes);
    }
}
