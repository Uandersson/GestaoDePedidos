using System.Collections.Generic;

namespace P2_Prova_de_POO
{
    public interface IRepository<T>
    {
        void Adicionar(T item);
        T ObterPorId(int id);
        List<T> ObterTodos();
    }
}
