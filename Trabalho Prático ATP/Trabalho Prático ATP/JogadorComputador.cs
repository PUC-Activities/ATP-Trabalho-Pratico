using System;
using System.Text;
using System.IO;
using Batalha_Naval;

namespace Trabalho_Prático_ATP
{
    internal class JogadorComputador
    {
        private char[,] tabuleiro;
        private int pontuacao;
        private int numTirosDados;
        private Posicao[] posTirosDados;
        private Posicao posTiroDado = new Posicao();
        private Random random = new Random();

        public JogadorComputador(int linhas, int colunas)
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
                posTirosDados[i] = new Posicao(-1, -1);
            }
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

        public Posicao EscolherAtaque()
        {
            bool teste;
            do
            {
                int lin = random.Next(0, tabuleiro.GetLength(0));
                int col = random.Next(0, tabuleiro.GetLength(1));

                posTiroDado = new Posicao(lin, col);
                teste = true;

                teste = true;
                for (int i = 0; i < posTirosDados.Length && teste; i++)
                {
                    if (posTiroDado.Linha == posTirosDados[i].Linha && posTiroDado.Coluna == posTirosDados[i].Coluna)
                    {
                        Console.WriteLine("Posição já utilizada. Tente novamente.");
                        teste = false;
                    }
                }

            } while (!teste);

            posTirosDados[numTirosDados] = posTiroDado;
            numTirosDados++;

            return posTiroDado;
        }
        public bool ReceberAtaque(Posicao tiroRecebido)
        {
            bool acertou = false;

            if (tiroRecebido.Linha >= 0 && tiroRecebido.Linha < tabuleiro.GetLength(0) && tiroRecebido.Coluna >= 0 && tiroRecebido.Coluna < tabuleiro.GetLength(1))
            {
                char posicaoAtual = tabuleiro[tiroRecebido.Linha, tiroRecebido.Coluna];

                if (posicaoAtual == 'X') 
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
            
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    Console.Write($"\t{tabuleiro[i, j]}");
                }
                Console.WriteLine();
            }
        }

        public void ImprimirTabuleiroAdversario()
        {
           
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[i, j] == 'A' || tabuleiro[i,j] == 'T' || tabuleiro[i, j] == 'X')
                    {
                        Console.Write($"\t{tabuleiro[i, j]}");
                    }
                    else{
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