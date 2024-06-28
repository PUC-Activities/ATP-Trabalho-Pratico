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
            posTirosDados = new Posicao[numTirosDados];
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
            bool posicaoValida;

            do
            {
                posicaoValida = true;

                posTiroDado.Linha = random.Next(0, 9);
                posTiroDado.Coluna = random.Next(0, 9);

                
                for(int i=0 ; i < posTirosDados.Length ; i++)
                {
                    if (posTirosDados[i].Linha == posTiroDado.Linha && PosTirosDados[i].Coluna == posTiroDado.Coluna)
                    {
                        posicaoValida = false;
                    }
                }

            } while (!posicaoValida);

            //falta adicionar no final do vetor posTirosDados

            return posTiroDado;
        }
        public bool ReceberAtaque(Posicao tiroRecebido)
        {
            bool acertou = false;

            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if(i == tiroRecebido.Linha && j == tiroRecebido.Coluna)
                    {
                        if (tabuleiro[i, j] != 'A')
                        {
                            acertou = true;
                            tabuleiro[i, j] = 'T';
                        }
                        else if (tabuleiro[i, j] == 'A')
                        {
                            tabuleiro[i, j] = 'X';
                        }
                    }
                }
            }

            return acertou;
        }

        public void ImprimirTabuleiroJogador()
        {
            
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    Console.WriteLine($"\t{tabuleiro[i, j]}");
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
                        Console.WriteLine($"\t{tabuleiro[i, j]}");
                    }
                    else{
                        Console.WriteLine("A");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool AdicionarEmbarcacao(Embarcacao embarcacao, Posicao posEmbarcacao)
        {
            
            bool ehPossivel = false;

            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (i == posEmbarcacao.Linha && j == posEmbarcacao.Coluna)
                    {
                        if (i + embarcacao.Tamanho <= 9)
                        {
                            ehPossivel = true;
                        }
                    }
                }
                Console.WriteLine();
            }

            return ehPossivel;
        }
    
    }
}