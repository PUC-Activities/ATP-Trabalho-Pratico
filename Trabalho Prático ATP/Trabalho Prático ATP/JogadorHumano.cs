using System;
using System.Text;
using System.IO;
using Batalha_Naval;

namespace Batalha_Naval
{
    internal class JogadorHumano
    {
        private char[,] tabuleiro;
        private int pontuacao;
        private int numTirosDados;
        private Posicao[] posTirosDados;
        private string nickname;

        public JogadorHumano(int linhas, int colunas, string nomeCompleto)
        {
            tabuleiro = new char[linhas, colunas];
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    tabuleiro[i, j] = 'A';
                }
            }

            pontuacao = 0;
            numTirosDados = 0;
            posTirosDados = new Posicao[linhas * colunas];
            for (int i = 0; i < posTirosDados.Length; i++)
            {
                posTirosDados[i] = new Posicao(-1, -1); ;
            }

            nickname = GerarNickname(nomeCompleto);
        }

        public char[,] Tabuleiro
        {
            get { return tabuleiro; }
            set { tabuleiro = value; }
        }

        public int Pontuacao
        {
            get { return pontuacao; }
            set { pontuacao = value; }
        }

        public int NumTirosDados
        {
            get { return numTirosDados; }
            set { numTirosDados = value; }
        }

        public Posicao[] PosTirosDados
        {
            get { return posTirosDados; }
            set { posTirosDados = value; }
        }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public string GerarNickname(string nomeCompleto)
        {
            string[] vetorNome = nomeCompleto.Split(' ');
            string apelido = vetorNome[0];

            for (int i = 1; i < vetorNome.Length; i++)
            {
                apelido += vetorNome[i].Substring(0, 1).ToUpper();
            }
            return apelido;
        }

        public Posicao EscolherAtaque()
        {
            bool teste = true;
            Posicao tiro = null;

            do
            {
                Console.WriteLine($"\n----------------------------------------------------------------------");
                Console.Write("Informe a linha do Ataque: ");
                int lin = int.Parse(Console.ReadLine());
                Console.Write("Informe a coluna do Ataque: ");
                int col = int.Parse(Console.ReadLine());
                tiro = new Posicao(lin, col);
                Console.WriteLine($"----------------------------------------------------------------------\n");

                if (lin < 0 || lin >= tabuleiro.GetLength(0) || col < 0 || col >= tabuleiro.GetLength(1))
                {
                    Console.WriteLine("Posição fora dos limites do tabuleiro. Tente novamente.");
                    teste = false;
                }
                else
                {
                    teste = true;
                    for (int i = 0; i < posTirosDados.Length && teste; i++)
                    {
                        if (tiro.Linha == posTirosDados[i].Linha && tiro.Coluna == posTirosDados[i].Coluna)
                        {
                            Console.WriteLine("Posição já utilizada. Tente novamente.");
                            teste = false;
                        }
                    }
                }

            } while (!teste);

            posTirosDados[numTirosDados] = tiro;
            numTirosDados++;

            return tiro;
        }

        public bool ReceberAtaque(Posicao tiroRecebido)
        {
            bool acertou = false;

            if (tiroRecebido.Linha >= 0 && tiroRecebido.Linha < tabuleiro.GetLength(0) && tiroRecebido.Coluna >= 0 && tiroRecebido.Coluna < tabuleiro.GetLength(1))
            {
                char posicaoAtual = tabuleiro[tiroRecebido.Linha, tiroRecebido.Coluna];
S
                if (posicaoAtual != 'A') 
                {
                    acertou = true;
                    tabuleiro[tiroRecebido.Linha, tiroRecebido.Coluna] = 'T'; 
                }
                else if (posicaoAtual == 'A')
                {
                    tabuleiro[tiroRecebido.Linha, tiroRecebido.Coluna] = 'X'; 
                }
                
            }
            else
            {
                Console.WriteLine("Posição fora dos limites do tabuleiro.");
            }

            return acertou;
        }

        public void ImprimirTabuleiroJogador()
        {

            for (int i = 0; i < tabuleiro.GetLength(1); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(0); j++)
                {
                    Console.Write($"\t{tabuleiro[i, j]}");
                }
                Console.WriteLine();
            }
        }

        public void ImprimirTabuleiroAdversario()
        {

            for (int i = 0; i < tabuleiro.GetLength(1); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(0); j++)
                {
                    if (tabuleiro[i, j] == 'A' || tabuleiro[i, j] == 'T' || tabuleiro[i, j] == 'X')
                    {
                        Console.Write($"\t{tabuleiro[i, j]}");
                    }
                    else
                    {
                        Console.Write("\tA");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool AdicionarEmbarcacao(Embarcacao embarcacao, Posicao posEmbarcacao)
        {
            int linha = posEmbarcacao.Linha;
            int coluna = posEmbarcacao.Coluna;

            if (linha < 0 || linha >= tabuleiro.GetLength(0) || coluna < 0 || coluna + embarcacao.Tamanho > tabuleiro.GetLength(1))
            {
                return false;
            }

            for (int i = coluna; i < coluna + embarcacao.Tamanho; i++)
            {
                if (tabuleiro[linha, i] != 'A')
                {
                    return false;
                }
            }

            for (int i = coluna; i < coluna + embarcacao.Tamanho; i++)
            {
                tabuleiro[linha, i] = 'X'; 
            }

            return true;
        }
    }
}