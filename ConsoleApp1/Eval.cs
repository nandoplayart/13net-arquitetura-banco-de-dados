using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Eval
    {

        public string Resposta(string pergunta)
        {
            bool bResp = true;

            string sResp = "";
            string sinais = "+/-*";
            foreach (var item in sinais.ToCharArray())
            {
                string[] validacao = pergunta.Replace("?","").Split(item);
                if (validacao.Length > 1)
                {
                    try
                    {
                        switch (item)
                        {
                            case '+':
                                sResp = (int.Parse(validacao[0].Trim()) + int.Parse(validacao[1].Trim())).ToString();
                                break;
                            case '-':
                                sResp = (int.Parse(validacao[0].Trim()) - int.Parse(validacao[1].Trim())).ToString();
                                break;
                            case '*':
                                sResp = (int.Parse(validacao[0].Trim()) * int.Parse(validacao[1].Trim())).ToString();
                                break;
                            case '/':
                                sResp = (int.Parse(validacao[0].Trim()) / int.Parse(validacao[1].Trim())).ToString();
                                break;
                            default:
                                bResp = !bResp;
                                sResp = GetRespose(bResp);
                                break;
                        }
                    }
                    catch
                    {
                        bResp = !bResp;
                        sResp = GetRespose(bResp);
                    }
                    break;

                }
            }
            if (string.IsNullOrWhiteSpace(sResp))
            {
                bResp = !bResp;
                sResp = GetRespose(bResp);
            }

            return sResp;
        }

        public bool Presente(string pergunta)
        {
            string alunos = "jean alonso|tiago de souza|luiz fernando|priscila lanna";
            foreach (var item in alunos.Split('|'))
            {
                if (pergunta.ToLower().Contains(item))
                    return true;
            }
            return false;
        }
        private static Random rnd = new Random();
        private static string GetRespose(bool b)
        {
            var choices = b
                ? new[] { "Muito bom!", "Excelente!", "Bom trabalho!", "Continue fazendo um bom trabalho!", }
                : new[] { "Hummm!", "Não foi legal!", "Não pergunte isso!", "Essa não foi boa!", };

            return choices[rnd.Next(0, choices.Length)];
        }
    }
}
