using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcessaRelatorioVenda.Service
{
    public static class Logger
    {
        public static void LogarErro(string message)
        {
            var arquivolog = ".\\log.txt";
            File.Create(arquivolog).Close();
            File.AppendAllText(arquivolog, message);
        }

        public static void LogarErroLeituraLinha(int index, string arquivo)
        {
            Logger.LogarErro($"Os dados da linha {index}, no arquivo \"{arquivo}\" não puderam ser interpretados pois estão fora do formato esperado");
        }
    }
}
