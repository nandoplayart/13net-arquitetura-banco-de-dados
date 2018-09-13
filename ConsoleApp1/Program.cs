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
            bool bResp = true;
            string channel = "perguntas";
            string connectionsString = "127.0.0.1:6379";
            var redis = ConnectionMultiplexer.Connect(connectionsString);
            var db = redis.GetDatabase();

            Console.WriteLine("Aguardando perguntas: ");

            var sub = redis.GetSubscriber();
            sub.Subscribe(channel, (ch, msg) =>
            {
                bResp = !bResp;
                string resp = (msg.ToString().Contains(":")) ? msg.ToString().Split(':')[0] : msg.ToString();
                db.HashSet(resp, "Delphos", GetRespose(bResp));
                Console.WriteLine($"Para a pergunta '{msg.ToString()}' a resposta foi '{db.HashGet(resp, "Delphos")}'");
            });
            Console.ReadKey();
        }

        private static Random rnd = new Random();
        private static string GetRespose(bool b)
        {
            var choices = b
                ? new[] { "Very good!", "Excellent!", "Nice work!", "Keep up the good work!", }
                : new[] { "Bad!", "V Bad!", "VV Bad!", "VVV Bad!", };

            return choices[rnd.Next(0, choices.Length)];
        }
    }
}
