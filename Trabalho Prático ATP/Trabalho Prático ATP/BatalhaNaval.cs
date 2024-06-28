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
        static void ArquivoComputador(char[,] tabuleiroComputador, Embarcacao[] nomesEmbarcacoes)
        {
            string linhaArq;
            string[] temp;
            Posicao posEmbarcaoArquivo = new Posicao();

            PreencheTabuleiros(tabuleiroComputador);

            try
            {
                StreamReader frotaComputadores = new StreamReader("frotaComputador.txt", Encoding.UTF8);

                linhaArq = frotaComputadores.ReadLine();

                while (linhaArq != null)
                {
                    temp = linhaArq.Split(';');
                    posEmbarcaoArquivo.Linha = int.Parse(temp[1]);
                    posEmbarcaoArquivo.Coluna = int.Parse(temp[2]);

                    for (int i = 0; i < tabuleiroComputador.GetLength(0); i++)
                    {
                        for (int j = 0; j < tabuleiroComputador.GetLength(1); j++)
                        {
                            switch (i)
                            {
                                case 0:
                                    nomesEmbarcacoes[0].Nome = temp[0];
                                    nomesEmbarcacoes[0].Tamanho = 5;

                                    tabuleiroComputador[posEmbarcaoArquivo.Linha, posEmbarcaoArquivo.Coluna] = 'P';
                                    break;

                                case 1:
                                case 2:
                                    nomesEmbarcacoes[1].Nome = temp[0];
                                    nomesEmbarcacoes[0].Tamanho = 4;

                                    tabuleiroComputador[posEmbarcaoArquivo.Linha, posEmbarcaoArquivo.Coluna] = 'E';
                                    break;

                                case 3:
                                case 4:
                                    nomesEmbarcacoes[2].Nome = temp[0];
                                    nomesEmbarcacoes[0].Tamanho = 3;

                                    tabuleiroComputador[posEmbarcaoArquivo.Linha, posEmbarcaoArquivo.Coluna] = 'C';
                                    break;

                                case 5:
                                case 6:
                                    nomesEmbarcacoes[3].Nome = temp[0];
                                    nomesEmbarcacoes[0].Tamanho = 2;

                                    tabuleiroComputador[posEmbarcaoArquivo.Linha, posEmbarcaoArquivo.Coluna] = 'H';

                                    break;

                                case 7:
                                case 8:
                                case 9:
                                    nomesEmbarcacoes[4].Nome = temp[0];
                                    nomesEmbarcacoes[0].Tamanho = 1;

                                    tabuleiroComputador[posEmbarcaoArquivo.Linha, posEmbarcaoArquivo.Coluna] = 'S';

                                    break;
                            }
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
                    tabuleiro[i, j] = 'A';
                }
            }
        }

        static void PreenchePosicaoEmbarcao(Posicao embarcacao)
        {
            Console.WriteLine("Linha: ");
            embarcacao.Linha = int.Parse(Console.ReadLine());

            Console.WriteLine("Coluna: ");
            embarcacao.Coluna = int.Parse(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            //Declaração de Variáveis 
            bool podeAdicionar = true;
            int linhasTab = 10, colunasTab = 10, count = 0, nEmbarcacao;
            string nomeHumano, nickName;
            char[,] tabuleiroComputador = new char[10, 10];
            char[,] tabuleiroHumano = new char[10, 10];
            Embarcacao[] nomesEmbarcacoes = new Embarcacao[5];
            Embarcacao embarcacaoHumano;
            Posicao posEmbarcaoHumano = new Posicao(); 


            Console.WriteLine($"Seja bem vindo ao jogo de batalha naval, digite seu nome completo: ");
            Console.Write("Nome completo: ");
            nomeHumano = Console.ReadLine();


            //Instanciando as Classes e gerando nickName
            JogadorHumano humano = new JogadorHumano(linhasTab, colunasTab, nomeHumano);
            JogadorComputador computador = new JogadorComputador(linhasTab, colunasTab);

            nickName = humano.GerarNickname(nomeHumano);

            //Abrindo o arquivo frotaComputadores e preenchendo o tabuleiro do computador
            ArquivoComputador(tabuleiroComputador, nomesEmbarcacoes);

            //Preenchendo o tabuleiro do jogador com A, e escolhendo onde ficarão suas embarcações
            PreencheTabuleiros(tabuleiroHumano);

            Console.WriteLine($"{nickName} posicione suas embarcações. Atenção: ");
            Console.WriteLine($"Embarcação\tQuantidade\tRepresentação no tabuleiro");
            Console.WriteLine($"0- Submarino\t 3 \tS");
            Console.WriteLine($"1- Hidroavião\t 2 \tHH");
            Console.WriteLine($"2- Cruzador\t 2 \tCCC");
            Console.WriteLine($"3- Encouraçado\t 1 \tEEEE");
            Console.WriteLine($"4- Porta-aviões\t 1 \tPPPPP");

            while (count < 10)
            {
                Console.WriteLine("Digite o numero da embarcação que deseja adicionar ");
                nEmbarcacao = int.Parse(Console.ReadLine());

                //o case é referente às embarcações, 1 é submarino, 2 hidroavião, e etc
                switch (nEmbarcacao)
                {
                    case 0:

                        //esse primeiro for itera sobre a quantidade de embarcações daquele tipo, no caso do submarino: 3 
                        for (int i = 0; i < 3; i++)
                        {
                            PreenchePosicaoEmbarcao(posEmbarcaoHumano);

                            //chama o método AdicoinarEmbarcacao da classe JogadorHumano para verificar se aquela embarcação pode ser criada
                            podeAdicionar = humano.AdicionarEmbarcacao(nomesEmbarcacoes[0], posEmbarcaoHumano);

                            while (podeAdicionar)
                            {
                                //esses for, iteram sobre o tabuleiro, e verifica se a posição em que ele está é igual a posiçao que o jogador humano escolheu, quando for, ele adiciona a embarcação que o usuário quer  
                                for (int j = 0; j < tabuleiroHumano.GetLength(0); j++)
                                {
                                    for (int k = 0; k < tabuleiroHumano.GetLength(1); k++)
                                    {
                                        if (tabuleiroHumano[j, k] == posEmbarcaoHumano.Linha && tabuleiroHumano[j, k] == posEmbarcaoHumano.Coluna)
                                        {
                                            tabuleiroHumano[posEmbarcaoHumano.Linha, posEmbarcaoHumano.Coluna] = 'S';
                                        }
                                    }       
                                }
                            }
                        }

                        break;

                    //tem que fazer a mesma lógica para os outros cases, acho que pode ser feita uma função para isso
                    case 1:
                    case 2:

                        for (int i = 0; i < 2; i++)
                        {

                        }

                        break;

                    case 3:
                    case 4:

                        for (int i = 0; i < 2; i++)
                        {

                        }

                        break;

                    case 5:
                    case 6:

                        for (int i = 0; i < 1; i++)
                        {

                        }

                        break;

                    case 7:
                    case 8:
                    case 9:

                        for (int i = 0; i < 1; i++)
                        {

                        }

                        break;
                }

                count++;
            }

            Console.ReadLine();
        }

    }
}
    

