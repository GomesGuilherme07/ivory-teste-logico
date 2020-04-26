using System;

namespace Ivory.TesteEstagio.CampoMinado
{
    class Program
    {
        // Função utilizada no desenvolvimento do algoritmo para verificar o tabuleiro.
        static void ExibirMatriz(char[,] matriz)
        {
            Console.WriteLine("\nMatriz cópia do jogo\n");

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    Console.Write(matriz[i, j]);
                }

                Console.WriteLine();
            }
        }

        // Função que recebe a string do tabuleiro e transforma em uma matriz.
        public static char[,] Copiar(string campo, char[,] matriz)
        {
            var posicao = 0;

            // substituindo os valores "/r/n" da string por vazio

            campo = campo.Replace("\r\n", "");

            // Copiando String para uma matriz

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(matriz[i,j] != '*')
                    {
                        matriz[i, j] = campo[posicao];                        
                    }

                    posicao++;
                }
            }

            return matriz;
        }        

        // Função que verifica uma casa segura para ser aberta e marca opções identificadas como bomba.
        public static char[,] VerificaCasaSegura(char[,] matrizTabuleiro, CampoMinado campoMinado, string campo)
        {
            // contador será utilizado para contar as casas não abertas ao redor de cada posição.
            var contador = 0;

            // auxJ será utilizado para guardar a linha de um casa segura de ser aberta.
            var auxJ = 0;

            // auxI será utilizado para guardar a coluna de um casa segura de ser aberta.
            var auxI = 0;

            // contador será utilizado para contar as bombas ao redor de cada posição.
            var contadorBomba = 0;            
            
            campo = campoMinado.Tabuleiro;

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    
                    // Verificando casas seguras de abrir ao redor do valor 1.
                    if (matrizTabuleiro[i, j] == '1')
                    {
                        // Verificar lado direito.
                        if (j + 1 >= 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i, j + 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i, j + 1] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verificar diagonal direita inferior.
                        if (i + 1 >= 0 && i + 1 < 9 && j + 1 >= 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i + 1, j + 1] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verificar abaixo
                        if (i + 1 >= 0 && i + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i + 1, j] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verificar diagonal esquerda inferior.
                        if (i + 1 >= 0 && i + 1 < 9 && j - 1 >= 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i + 1, j - 1] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verificar lado esquerdo.
                        if (j - 1 >= 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i, j - 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i, j - 1] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verificar diagonal esquerda superior.
                        if (i - 1 >= 0 && i - 1 < 9 && j - 1 >= 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i - 1, j - 1] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verificar superior
                        if (i - 1 >= 0 && i - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i - 1, j] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verificar diagonal direita superior.
                        if (i - 1 >= 0 && i - 1 < 9 && j + 1 >= 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i - 1, j + 1] == '*')
                            {
                                contadorBomba++;                               
                            }
                        }

                        // Verifica se apenas uma das 8 possiveis posições em volta do elemento não foi aberta.
                        // Caso o contador esteja em 1 a condição será satisfeita o que determina que a posição '-' é bomba.
                        // Caso exista bomba ao redor do valor 1, se existir bomba e casas vazias abre a casa vazia.
                        if (contador == 1 && contadorBomba == 0)
                        {
                            matrizTabuleiro[auxI, auxJ] = '*';
                        }
                        if (contador == 1 && contadorBomba == 1)
                        {
                            // Bloco chama o metodo da classe CampoMinado para abrir um a posição.
                            // Exibe o resultado na tela e faz uma nova cópia do tabuleiro.
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }
                        if (contador >= 1 && contadorBomba == 1)
                        {
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }
                        

                        contador = 0;
                        contadorBomba = 0;
                    }


                    // Verificando casas seguras de abrir ao redor do valor 2.
                    if (matrizTabuleiro[i, j] == '2')
                    {
                        // Verificar lado direito.
                        if (j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i, j + 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal direita inferior.
                        if (i + 1 > 0 && i + 1 < 9 && j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i + 1, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar abaixo
                        if (i + 1 > 0 && i + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i + 1, j] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal esquerda inferior.
                        if (i + 1 > 0 && i + 1 < 9 && j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i + 1, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar lado esquerdo.
                        if (j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i, j - 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal esquerda superior.
                        if (i - 1 > 0 && i - 1 < 9 && j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i - 1, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar superior
                        if (i - 1 > 0 && i - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i - 1, j] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal direita superior.
                        if (i - 1 > 0 && i - 1 < 9 && j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i - 1, j + 1] == '*')
                            {
                                contadorBomba++;                                
                            }
                        }

                        // Verifica se existe bomba ao redor do valor 2, se existir 2 bombas e casas vazias abre a casa vazia
                        // Verifica se existe bomba ao redor do valor 2, se existir bomba e uma casa vazia marca a casa vazia como bomba

                        if (contador == 1 && contadorBomba == 1)
                        {
                            matrizTabuleiro[auxI, auxJ] = '*';
                        }
                        if (contador == 1 && contadorBomba == 2)
                        {
                            // Bloco chama o metodo da classe CampoMinado para abrir um a posição.
                            // Exibe o resultado na tela e faz uma nova cópia do tabuleiro.
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }
                        if (contador > 1 && contadorBomba == 2)
                        {
                            // Bloco chama o metodo da classe CampoMinado para abrir um a posição.
                            // Exibe o resultado na tela e faz uma nova cópia do tabuleiro.
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }
                        if(contador >= 2 && contadorBomba == 0)
                        {
                            matrizTabuleiro[auxI, auxJ] = '*';
                        }

                        contador = 0;
                        contadorBomba = 0;
                    }


                    // Verificando casas seguras de abrir ao redor do valor 3.
                    if (matrizTabuleiro[i, j] == '3')
                    {
                        // Verificar lado direito.
                        if (j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i, j + 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal direita inferior.
                        if (i + 1 > 0 && i + 1 < 9 && j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i + 1, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar abaixo
                        if (i + 1 > 0 && i + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i + 1, j] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal esquerda inferior.
                        if (i + 1 > 0 && i + 1 < 9 && j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i + 1, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar lado esquerdo.
                        if (j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i, j - 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal esquerda superior.
                        if (i - 1 > 0 && i - 1 < 9 && j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i - 1, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar superior
                        if (i - 1 > 0 && i - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i - 1, j] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal direita superior.
                        if (i - 1 > 0 && i - 1 < 9 && j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i - 1, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verifica se existe bomba ao redor do valor 3, se existir 3 bombas e casas vazias abre a casa vazia
                        // Verifica se existe bomba ao redor do valor 3, se existir 1 bomba e 2 casas vazias marca a casa vazia como bomba

                        if (contador == 1 && contadorBomba == 2)
                        {
                            matrizTabuleiro[auxI, auxJ] = '*';
                        }                       
                        if (contador == 1 && contadorBomba == 3)
                        {
                            // Bloco chama o metodo da classe CampoMinado para abrir um a posição.
                            // Exibe o resultado na tela e faz uma nova cópia do tabuleiro.
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }
                        if (contador >= 1 && contadorBomba == 3)
                        {
                            // Bloco chama o metodo da classe CampoMinado para abrir um a posição.
                            // Exibe o resultado na tela e faz uma nova cópia do tabuleiro.
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }


                        contador = 0;
                        contadorBomba = 0;
                    }


                    // Verificando casas seguras de abrir ao redor do valor 4.
                    if (matrizTabuleiro[i, j] == '4')
                    {
                        // Verificar lado direito.
                        if (j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i, j + 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal direita inferior.
                        if (i + 1 > 0 && i + 1 < 9 && j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i + 1, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar abaixo
                        if (i + 1 > 0 && i + 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i + 1, j] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal esquerda inferior.
                        if (i + 1 > 0 && i + 1 < 9 && j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i + 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i + 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i + 1, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar lado esquerdo.
                        if (j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i, j - 1] == '-')
                            {
                                contador++;
                                auxI = i;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal esquerda superior.
                        if (i - 1 > 0 && i - 1 < 9 && j - 1 > 0 && j - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j - 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j - 1;
                            }
                            else if (matrizTabuleiro[i - 1, j - 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar superior
                        if (i - 1 > 0 && i - 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j;
                            }
                            else if (matrizTabuleiro[i - 1, j] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verificar diagonal direita superior.
                        if (i - 1 > 0 && i - 1 < 9 && j + 1 > 0 && j + 1 < 9)
                        {
                            if (matrizTabuleiro[i - 1, j + 1] == '-')
                            {
                                contador++;
                                auxI = i - 1;
                                auxJ = j + 1;
                            }
                            else if (matrizTabuleiro[i - 1, j + 1] == '*')
                            {
                                contadorBomba++;
                            }
                        }

                        // Verifica se existe bomba ao redor do valor 4, se existir 4 bombas e casas vazias abre a casa vazia
                        // Verifica se existe bomba ao redor do valor 4, se existir 3 bombas e 1 casa vazia marca a casa vazia como bomba

                        if (contador == 1 && contadorBomba == 3)
                        {
                            matrizTabuleiro[auxI, auxJ] = '*';
                        }
                        if (contador == 1 && contadorBomba == 4)
                        {
                            // Bloco chama o metodo da classe CampoMinado para abrir um a posição.
                            // Exibe o resultado na tela e faz uma nova cópia do tabuleiro.
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }
                        if (contador >= 1 && contadorBomba == 4)
                        {
                            // Bloco chama o metodo da classe CampoMinado para abrir um a posição.
                            // Exibe o resultado na tela e faz uma nova cópia do tabuleiro.
                            campoMinado.Abrir(auxI + 1, auxJ + 1);
                            Console.WriteLine(campoMinado.Tabuleiro);
                            Console.WriteLine();
                            campo = campoMinado.Tabuleiro;
                            matrizTabuleiro = Copiar(campo, matrizTabuleiro);
                        }

                        contador = 0;
                        contadorBomba = 0;
                    }

                }

            }

            return matrizTabuleiro;
        }


        static void Main(string[] args)
        {
            var campoMinado = new CampoMinado();
            Console.WriteLine("Início do jogo\n=========");
            Console.WriteLine(campoMinado.Tabuleiro);

            // Realize sua codificação a partir deste ponto, boa sorte!

            // Declaração de uma matriz que irá receber um cópia do campo minado; E declaração de uma string para receber o tabuleiro.
            char[,] matrizTabuleiro = new char[9, 9];
            string campo = "";           

            campo = campoMinado.Tabuleiro;

            // Chama a função que copia a string do campo minado e preenche uma matriz [9,9] com os valores da string.
            matrizTabuleiro = Copiar(campo, matrizTabuleiro);            
            
            // Bloco que chama a função para verificar uma casa segura dentro do tabuleiro e abrir.
            while(campoMinado.JogoStatus == 0)
            {
                matrizTabuleiro = VerificaCasaSegura(matrizTabuleiro, campoMinado, campo);                 
            }

            Console.WriteLine("Status do Jogo: {0}", campoMinado.JogoStatus);

            
            Console.ReadKey();
        }
    }
}
