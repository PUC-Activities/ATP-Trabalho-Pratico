using System;
using System.Text;
using System.IO;
using Batalha_Naval;

namespace Trabalho_Prático_ATP
{
    internal class BatalhaNaval
    {
        static void LimpaTemp(string[] temp)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = "";
            }
        }
        static void ArquivoComputador(char[,] tabuleiroComputador)
        {
            string linhaArq;
            string[] temp;
            PreencheTabuleiros(tabuleiroComputador);

            try
            {
                StreamReader frotaComputadores = new StreamReader("frotaComputador.txt", Encoding.UTF8);

                linhaArq = frotaComputadores.ReadLine();

                while(linhaArq != null)
                {
                    temp = linhaArq.Split(';');

                    for (int i = 0;i < tabuleiroComputador.GetLength(0) ; i++)
                    {
                        for (int j = 0; j < tabuleiroComputador.GetLength(1); j++)
                        {

                        }
                    }
                }

                frotaComputadores.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }

        }

        static void PreencheTabuleiros(char[,] tabuleiro)
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    tabuleiro[i,j] = 'A';
                }
            }
        }

        static void Main(string[] args)
        {
            //Declaração de Variáveis 
            int linhasTab = 10, colunasTab = 10;
            string nomeHumano;
            char[,] tabuleiroComputador = new char[10, 10];

            
            Console.WriteLine($"Seja bem vindo ao jogo de batalha naval, seu nome completo: ");
            Console.Write("Nome: ");
            nomeHumano = Console.ReadLine();


            //Instanciando as Classes
            JogadorHumano humano = new JogadorHumano(linhasTab, colunasTab, nomeHumano);
            JogadorComputador computador = new JogadorComputador(linhasTab, colunasTab);

            Console.WriteLine("Seu Nickname: " + humano.Nickname);



            Console.ReadLine();
        }
    }
}
