using ProcessaRelatorioVenda.Service;
using System;
using System.Threading;

namespace ProcessaRelatorioVenda
{
    class Program
    {
        static void Main(string[] args)
        {
            //Questões a se considerar:

            //Como não foi definido um formato de arquivo na prova, presumi que seriam arquivos .txt e toda a formatação foi feita com base nesses arquivos.

            //fiz em formato de console application pois fica mais fácil de se debugar, 
            //mas dados os requisitos do projeto ele deveria ser instalado como um serviço e rodar silenciosamente
            //isso pode ser implementado colocando os arquivos de instalação em uma pasta 
            //e transformando essa chamada do timer em uma nova thread, dessa forma rodaria uma vez e ficaria rodando para sempre


            

            new Timer((e) =>
            {
                new ProcessaEntrada().ProcessaDados(Environment.GetEnvironmentVariable("HOMEPATH") + "\\data\\in");
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            Console.WriteLine("Aperte algum botão para terminar a aplicação");
            Console.ReadLine();
        }
    }
}
