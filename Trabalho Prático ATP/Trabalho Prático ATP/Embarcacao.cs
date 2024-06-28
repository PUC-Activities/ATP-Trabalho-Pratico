using System;
using System.Text;
using System.IO;

namespace Batalha_Naval
{
    internal class Embarcacao
    {
        private string nome;
        private int tamanho;

        public Embarcacao(string nome, int tamanho)
        {
            this.nome = nome;
            this.tamanho = tamanho;
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Tamanho
        {
            get { return tamanho; }
            set { tamanho = value; }
        }
    }
}