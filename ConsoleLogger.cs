using System;

namespace P2_Prova_de_POO
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string mensagem) => Console.WriteLine("[LOG] " + mensagem);
    }
}
