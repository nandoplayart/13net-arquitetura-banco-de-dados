using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Perguntas();
            Chamada();
            Console.ReadKey();
        }

        static void Perguntas()
        {
            string channel = "perguntas";
            string connectionsString = "191.232.234.20:6379";
            //string connectionsString = "127.0.0.1:6379";
            var redis = ConnectionMultiplexer.Connect(connectionsString);
            var db = redis.GetDatabase();

            Console.WriteLine("Aguardando perguntas: ");
            Eval eval = new Eval();
            var sub = redis.GetSubscriber();
            sub.Subscribe(channel, (ch, msg) =>
            {
                string resp = (msg.ToString().Contains(":")) ? msg.ToString().Split(':')[0] : msg.ToString();
                db.HashSet(resp, "Delphos", eval.Resposta(msg.ToString().Split(':')[1]));
                Console.WriteLine($"Para a pergunta '{msg.ToString()}' a resposta foi '{db.HashGet(resp, "Delphos")}'");
            });
        }

        static void Chamada()
        {
            string channel = "Chamada";
            //string connectionsString = "191.232.234.20:6379";
            string connectionsString = "127.0.0.1:6379";
            var redis = ConnectionMultiplexer.Connect(connectionsString);
            var db = redis.GetDatabase();

            Console.WriteLine("Aguardando Nomes: ");
            Eval eval = new Eval();
            var sub = redis.GetSubscriber();
            sub.Subscribe(channel, (ch, msg) =>
            {
                string resp = (msg.ToString().Contains(":")) ? msg.ToString().Split(':')[0] : msg.ToString();
                if (eval.Presente(resp))
                {
                    db.HashSet("Chamada", resp, "Presente");
                    Console.WriteLine($"Aluno '{msg.ToString()}' esta '{db.HashGet("Chamada", resp)}'");
                }
            });
        }
    }
}
