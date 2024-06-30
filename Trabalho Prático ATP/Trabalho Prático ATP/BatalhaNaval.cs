using System;
using System.Text;
using System.IO;
using Batalha_Naval;

namespace Trabalho_Prático_ATP
{
    internal class BatalhaNaval
    {
        static bool ArquivoComputador(JogadorComputador computador, Embarcacao[] nomesEmbarcacoes)
        {
            bool arquivoValido = true;
            string linhaArq;

            Posicao posEmbarcaoArquivo = new Posicao();

            PreencheTabuleiros(computador.Tabuleiro);

            try
            {
                StreamReader frotaComputadores = new StreamReader("frotaComputador.txt", Encoding.UTF8);

                //inicializando o vetor de embarcações
                for (int i = 0; i < nomesEmbarcacoes.Length; i++)
                {
                    nomesEmbarcacoes[i] = new Embarcacao("", 0);
                }

                while ((linhaArq = frotaComputadores.ReadLine()) != null)
                {
                    bool podeAdicionar = false;
                    char simbolo = ' ';
                    string[] temp = linhaArq.Split(';');
                    string nomeEmbarcacao = temp[0];
                    posEmbarcaoArquivo.Linha = int.Parse(temp[1]);
                    posEmbarcaoArquivo.Coluna = int.Parse(temp[2]);
                    Embarcacao embarcacaoAtual;

                    int tipoEmbarcacao = DeterminarTipoEmbarcacao(nomeEmbarcacao);

                    if (tipoEmbarcacao >= 0 && tipoEmbarcacao < 5)
                    {

                        switch (tipoEmbarcacao)
                        {
                            case 0:
                                nomesEmbarcacoes[tipoEmbarcacao].Tamanho = 5;
                                simbolo = 'P';
                                break;
                            case 1:
                                ;
                                nomesEmbarcacoes[tipoEmbarcacao].Tamanho = 4;
                                simbolo = 'E';
                                break;
                            case 2:
                                nomesEmbarcacoes[tipoEmbarcacao].Tamanho = 3;
                                simbolo = 'C';
                                break;
                            case 3:
                                nomesEmbarcacoes[tipoEmbarcacao].Tamanho = 2;
                                simbolo = 'H';
                                break;
                            case 4:
                                nomesEmbarcacoes[tipoEmbarcacao].Tamanho = 1;
                                simbolo = 'S';
                                break;
                        }

                        nomesEmbarcacoes[tipoEmbarcacao].Nome = temp[0];

                        embarcacaoAtual = nomesEmbarcacoes[tipoEmbarcacao];

                        podeAdicionar = computador.AdicionarEmbarcacao(embarcacaoAtual, posEmbarcaoArquivo);

                        if (podeAdicionar)
                        {
                            for (int k = posEmbarcaoArquivo.Coluna; k < posEmbarcaoArquivo.Coluna + nomesEmbarcacoes[tipoEmbarcacao].Tamanho; k++)
                            {
                                computador.Tabuleiro[posEmbarcaoArquivo.Linha, k] = simbolo;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro: Não há espaço suficiente para o tamanho da embarcação ou a posição já está ocupada por outra.\n Reescreva no arquivo e tente novamente");

                            arquivoValido = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erro: Tipo de embarcação não reconhecido. Digite corretamete ou escreva uma embarcação válida");
                        arquivoValido = false;
                    }
                }
                frotaComputadores.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                arquivoValido = false;
            }

            return arquivoValido;
        }

        static int DeterminarTipoEmbarcacao(string nomeEmbarcacao)
        {
            switch (nomeEmbarcacao)
            {
                case "Porta-aviões":
                    return 0;
                case "Encouraçado":
                    return 1;
                case "Cruzador":
                    return 2;
                case "Hidroavião":
                    return 3;
                case "Submarino":
                    return 4;
                default:
                    return -1;
            }
        }
        static void PreencheTabuleiros(char[,] tabuleiro) //trocar o tabuleiro
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
            Console.Write("Linha: ");
            embarcacao.Linha = int.Parse(Console.ReadLine());

            Console.Write("Coluna: ");
            embarcacao.Coluna = int.Parse(Console.ReadLine());
        }

        static void PreencheTabuleiroHumano(char letraEmbarcacao, int nEmbarcacao, Posicao posEmbarcaoHumano, Embarcacao[] nomesEmbarcacoes, JogadorHumano humano)
        {
            bool podeAdicionar = false;
            Embarcacao embarcacaoAtual = nomesEmbarcacoes[nEmbarcacao];

            while (podeAdicionar == false)
            {

                //chama o método AdicinarEmbarcacao da classe JogadorHumano para verificar se aquela embarcação pode ser criada
                podeAdicionar = humano.AdicionarEmbarcacao(embarcacaoAtual, posEmbarcaoHumano);


                if (podeAdicionar == true)
                {
                    // Preencher a matriz com a embarcação
                    for (int l = posEmbarcaoHumano.Coluna; l < posEmbarcaoHumano.Coluna + nomesEmbarcacoes[nEmbarcacao].Tamanho; l++)
                    {
                        humano.Tabuleiro[posEmbarcaoHumano.Linha, l] = letraEmbarcacao;
                    }

                }
                else
                {
                    Console.WriteLine("Posição inválida, tente outra.");
                    PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                }
            }
        }

        static void Main(string[] args)
        {

            //Abrindo o arquivo frotaComputadores verificando se é válido e se for, continua o programa

            int linhasTab = 10, colunasTab = 10;
            Embarcacao[] nomesEmbarcacoes = new Embarcacao[5];
            JogadorComputador computador = new JogadorComputador(linhasTab, colunasTab);
            bool arquivoValido = ArquivoComputador(computador, nomesEmbarcacoes);

            if (arquivoValido == true)
            {
                //Declaração de Variáveis 
                bool acertouTiro = false;
                int count = 0, nEmbarcacao;
                string nomeHumano, nickName;
                Embarcacao embarcacaoHumano;
                Posicao posEmbarcaoHumano = new Posicao();
                Posicao posTiro = new Posicao();


                Console.WriteLine($"Seja bem vindo ao jogo de batalha naval, digite seu nome completo: ");
                Console.Write("Nome completo: ");
                nomeHumano = Console.ReadLine();


                //Instanciando as Classes e gerando nickName
                JogadorHumano humano = new JogadorHumano(linhasTab, colunasTab, nomeHumano);


                //Preenchendo o tabuleiro do jogador com A, e escolhendo onde ficarão suas embarcações
                PreencheTabuleiros(humano.Tabuleiro);

                Console.WriteLine($"\n{humano.Nickname} posicione suas embarcações. Atenção: ");
                Console.WriteLine($"Embarcação\tQuantidade\tRepresentação no tabuleiro");

                Console.WriteLine($"Porta-aviões\t 1 \t\tPPPPP");
                Console.WriteLine($"Encouraçado\t 1 \t\tEEEE");
                Console.WriteLine($"Cruzador\t 2 \t\tCCC");
                Console.WriteLine($"Hidroavião\t 2 \t\tHH");
                Console.WriteLine($"Submarino\t 3 \t\tS");


                while (count < 5)
                {
                    switch (count)
                    {
                        case 0:

                            for (int i = 0; i < 1; i++)
                            {
                                Console.WriteLine($"\n\nPreencha o {nomesEmbarcacoes[0].Nome}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('P', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }

                            //humano.ImprimirTabuleiroJogador();

                            break;
                        case 1:

                            for (int i = 0; i < 1; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[1].Nome}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('E', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }

                            //humano.ImprimirTabuleiroJogador();

                            break;
                        case 2:

                            for (int i = 0; i <= 2; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[2].Nome} numero {i + 1}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('C', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }

                            //humano.ImprimirTabuleiroJogador();

                            break;
                        case 3:

                            for (int i = 0; i < 2; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[3].Nome} numero {i + 1}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('H', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }

                            //humano.ImprimirTabuleiroJogador();
                            break;
                        case 4:

                            //esse primeiro for itera sobre a quantidade de embarcações daquele tipo, no caso do submarino: 3 
                            for (int i = 0; i < 3; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[4].Nome} numero {i + 1}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano); //Lê a linha e a coluna e colocar no objeto = Posicoes() posEmbarcacaoHumano;
                                PreencheTabuleiroHumano('S', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }

                            //humano.ImprimirTabuleiroJogador();
                            break;
                    }

                    count++;
                }

                humano.ImprimirTabuleiroJogador();

                while (humano.Pontuacao < 23 || computador.Pontuacao < 23)
                {
                    Console.WriteLine("Essa é a posição atual do tabuleiro do jogador computador: ");

                    computador.ImprimirTabuleiroAdversario();

                    posTiro = humano.EscolherAtaque();
                    acertouTiro = computador.ReceberAtaque(posTiro);

                    while (acertouTiro == true)
                    {
                        computador.ImprimirTabuleiroAdversario();
                        posTiro = humano.EscolherAtaque();

                        acertouTiro = computador.ReceberAtaque(posTiro);

                        humano.Pontuacao++;
                    }

                    Console.WriteLine($"Essa é a posição atual do tabuleiro do {humano.Nickname}: ");

                    humano.ImprimirTabuleiroAdversario();

                    posTiro = computador.EscolherAtaque();
                    acertouTiro = humano.ReceberAtaque(posTiro);

                    while (acertouTiro == true)
                    {
                        humano.ImprimirTabuleiroAdversario();
                        posTiro = computador.EscolherAtaque();

                        acertouTiro = humano.ReceberAtaque(posTiro);

                        computador.Pontuacao++;
                    }
                }

                string vencedor;
                if (humano.Pontuacao == 22)
                {
                    vencedor = humano.Nickname;
                }
                else
                {
                    vencedor = "Computador";
                }

                using (StreamWriter sw = new StreamWriter("jogadas.txt"))
                {
                    sw.WriteLine($"O vencedor foi: {vencedor}");

                    Posicao[] tiros;
                    if (vencedor == humano.Nickname)
                    {
                        tiros = humano.PosTirosDados;
                    }
                    else
                    {
                        tiros = computador.PosTirosDados;
                    }

                    foreach (var tiro in tiros)
                    {
                        if (tiro.Linha != -1 && tiro.Coluna != -1)
                        {
                            sw.WriteLine($"Linha: {tiro.Linha}, Coluna: {tiro.Coluna}");
                        }
                    }
                }
            }
            Console.ReadLine(); 
        }
    }
}

//tirar os métodos construtores da classe posicao 
//arruma função de abrir o arquivo
//adicionar função de adicinar embarcação na função arquivo do main 
//não deixar mais de uma embarcação ser adicionada