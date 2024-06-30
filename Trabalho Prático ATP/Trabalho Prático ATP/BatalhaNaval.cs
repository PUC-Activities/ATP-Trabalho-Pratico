using System;
using System.Text;
using System.IO;
using Batalha_Naval;
using System.CodeDom;

namespace Trabalho_Prático_ATP
{
    internal class BatalhaNaval
    {
        //função que abre o arquivo de frotas 
        static bool ArquivoComputador(JogadorComputador computador, Embarcacao[] nomesEmbarcacoes)
        {
            bool arquivoValido = true;
            string linhaArq;

            Posicao posEmbarcaoArquivo = new Posicao();

            //preenche todo o tabuleiro do computador com água 
            PreencheTabuleiros(computador.Tabuleiro);

            // try catch que faz a manipualção do arquivo 
            try
            {
                StreamReader frotaComputadores = new StreamReader("frotaComputador.txt", Encoding.UTF8);


                //inicializa um vetor de objetos de embarcações que conterá as 5 embarcações lidas do arquivo
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

                    //chama a função que determina o tipo de embarcação que é e retorna um inteiro com o tipo de embarcação, se estiver escrito errado retorna -1
                    int tipoEmbarcacao = DeterminarTipoEmbarcacao(nomeEmbarcacao);

                    if (tipoEmbarcacao >= 0 && tipoEmbarcacao < 5)
                    {
                        // se o numero de tipoEmbarcação for 0, quer dizer que é um porta-aviões, ele escreve 5 no objeto.tamanho e define a letra q vai ser escrita no tabuleiro 
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

                        //guarda o nome que estava salvo em temp (variável temporário) no objeto
                        nomesEmbarcacoes[tipoEmbarcacao].Nome = temp[0];

                        embarcacaoAtual = nomesEmbarcacoes[tipoEmbarcacao];

                        //booleano que guarda o valor de retorno da funçao, se pode ou n adicioar aquela embarcação 
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

        //função que recebe o nome da embarcação e determina o tipo dela 
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

        //funçao para preencher os tabuleiros com água 
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

        //função que pega a linha e a coluna de uma embarcação 
        static void PreenchePosicaoEmbarcao(Posicao embarcacao)
        {
            Console.Write("Linha: ");
            embarcacao.Linha = int.Parse(Console.ReadLine());

            Console.Write("Coluna: ");
            embarcacao.Coluna = int.Parse(Console.ReadLine());
        }

        //função que é chamada quando o humano for posicionar suas embarcações 
        static void PreencheTabuleiroHumano(char letraEmbarcacao, int nEmbarcacao, Posicao posEmbarcaoHumano, Embarcacao[] nomesEmbarcacoes, JogadorHumano humano)
        {
            bool podeAdicionar = false;
            Embarcacao embarcacaoAtual = nomesEmbarcacoes[nEmbarcacao];

            while (podeAdicionar == false)
            {
                //chama a função da classe, que retorna um booleano dizendo se pode ou não existir aquela embarcação naquele lugar
                podeAdicionar = humano.AdicionarEmbarcacao(embarcacaoAtual, posEmbarcaoHumano);


                if (podeAdicionar == true)
                {
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
            //verifica se o arquivo é válido, se não, não abre o programa

            int linhasTab = 10, colunasTab = 10;
            Embarcacao[] nomesEmbarcacoes = new Embarcacao[5];
            JogadorComputador computador = new JogadorComputador(linhasTab, colunasTab);
            bool arquivoValido = ArquivoComputador(computador, nomesEmbarcacoes);

            if (arquivoValido == true)
            {
                bool acertouTiro = false;
                int count = 0, nEmbarcacao;
                string nomeHumano, vencedor ;
                Posicao posEmbarcaoHumano = new Posicao();
                Posicao posTiro = new Posicao();


                Console.WriteLine($"Seja bem vindo ao jogo de batalha naval, digite seu nome completo: ");
                Console.Write("Nome completo: ");
                nomeHumano = Console.ReadLine();


                JogadorHumano humano = new JogadorHumano(linhasTab, colunasTab, nomeHumano);


                PreencheTabuleiros(humano.Tabuleiro);

                Console.WriteLine($"\n{humano.Nickname} posicione suas embarcações. Atenção: ");
                Console.WriteLine($"----------------------------------------------------------------------");

                Console.WriteLine($"Embarcação\tQuantidade\tRepresentação no tabuleiro");

                Console.WriteLine($"\nPorta-aviões\t 1 \t\tPPPPP");
                Console.WriteLine($"Encouraçado\t 1 \t\tEEEE");
                Console.WriteLine($"Cruzador\t 2 \t\tCCC");
                Console.WriteLine($"Hidroavião\t 2 \t\tHH");
                Console.WriteLine($"Submarino\t 3 \t\tS");
                Console.WriteLine($"----------------------------------------------------------------------");

                //o while itera sobre as 5 embarcações, e pra cada delas, itera a quantidade de vezes que ela deve existir, Ex: lê 3 vzs as informações de posição do submarino, pq precisam de 3 dele
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


                            break;
                        case 1:

                            for (int i = 0; i < 1; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[1].Nome}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('E', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }


                            break;
                        case 2:

                            for (int i = 0; i <= 2; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[2].Nome} numero {i + 1}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('C', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }


                            break;
                        case 3:

                            for (int i = 0; i < 2; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[3].Nome} numero {i + 1}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('H', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }

                            break;
                        case 4:
 
                            for (int i = 0; i < 3; i++)
                            {
                                Console.WriteLine($"Preencha o {nomesEmbarcacoes[4].Nome} numero {i + 1}");
                                PreenchePosicaoEmbarcao(posEmbarcaoHumano);
                                PreencheTabuleiroHumano('S', count, posEmbarcaoHumano, nomesEmbarcacoes, humano);
                            }
                            break;
                    }

                    count++;
                }

                //imprime o tabuleiro do jogador 
                computador.ImprimirTabuleiroJogador();

                //efetivamente roda o jogo
                while (humano.Pontuacao < 23 && computador.Pontuacao < 23)
                {
                    Console.WriteLine("\nEssa é a posição atual do tabuleiro do jogador computador: ");
                    Console.WriteLine($"----------------------------------------------------------------------");
                    computador.ImprimirTabuleiroAdversario();

                    //chama a função para escolher uma posição válida, e verifica se essa posição é válida no tabuleiro do computador
                    posTiro = humano.EscolherAtaque();
                    acertouTiro = computador.ReceberAtaque(posTiro);
                    humano.NumTirosDados++;

                    if (acertouTiro)
                    {
                        //se ele acertou o tiro, teoricamente era pra poder repetir até errar
                        Console.WriteLine("Você acertou um tiro!");
                        humano.Pontuacao++;


                        while (acertouTiro)
                        {
                            Console.WriteLine($"----------------------------------------------------------------------");
                            posTiro = humano.EscolherAtaque();
                            acertouTiro = computador.ReceberAtaque(posTiro);

                            if (acertouTiro)
                            {
                                Console.WriteLine("Você acertou um tiro!");
                                humano.Pontuacao++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Você errou o tiro. Agora é a vez do computador.");
                    }

                    //faz a mesma lógica anterior só que dessa vez pro computador 
                    Console.WriteLine($"\nEssa é a posição atual do tabuleiro do jogador {humano.Nickname}: ");
                    Console.WriteLine($"----------------------------------------------------------------------");
                    computador.ImprimirTabuleiroAdversario();

                    posTiro = computador.EscolherAtaque();
                    acertouTiro = humano.ReceberAtaque(posTiro);
                    computador.NumTirosDados++;

                    if (acertouTiro)
                    {
                        Console.WriteLine("O computador acertou um tiro!");
                        computador.Pontuacao++;

                        while (acertouTiro)
                        {
                            posTiro = computador.EscolherAtaque();
                            acertouTiro = humano.ReceberAtaque(posTiro);

                            if (acertouTiro)
                            {
                                Console.WriteLine("O computador acertou mais um tiro!");
                                computador.Pontuacao++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("O computador errou o tiro.");
                    }
                }

                if (humano.Pontuacao == 22)
                {
                    vencedor = humano.Nickname;
                    Console.Write($"Vencedor: {humano.Nickname}. \nParabéns!");
                }
                else
                {
                    vencedor = "Computador";
                    Console.Write($"Vencedor: Computador. \nParabéns!");
                }

                //parte que abre o arquivo, escreve o vencedor lá e as suas jogadas 
                try
                {
                    using (StreamWriter arqVencedor = new StreamWriter("jogadas.txt", false, Encoding.UTF8))
                    {
                        arqVencedor.WriteLine($"O vencedor foi: {vencedor}");

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
                                arqVencedor.WriteLine($"Linha: {tiro.Linha}, Coluna: {tiro.Coluna}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exceção: " + ex);
                }


            }
            Console.ReadLine(); 
        }
    }
}
