using System;
using System.Text;
using System.IO;
using Batalha_Naval;

namespace Batalha_Naval
{
    internal class Posicao
    {
        private int linha;
        private int coluna;

        public Posicao()
        {
            this.linha = linha;
            this.coluna = coluna;
        }
        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public int Linha
        {
            get { return linha; }
            set { linha = value; }
        }

        public int Coluna
        {
            get { return coluna; }
            set { coluna = value; }
        }
    }
}