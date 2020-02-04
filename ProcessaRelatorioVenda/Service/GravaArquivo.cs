using ProcessaRelatorioVenda.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcessaRelatorioVenda.Service
{
    public class GravaArquivo
    {
        public void Grava(string arquivoOrigem, RelatorioSaida relatorio)
        {
            var arquivoSaida = arquivoOrigem.Replace("\\data\\in", "\\data\\out");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Clientes no arquivo de entrada: " + relatorio.NumeroClientes);
            sb.AppendLine("Vendedores no arquivo de entrada: " + relatorio.NumeroVendedores);
            sb.AppendLine("ID da venda mais cara: " + relatorio.IdVendaMaisCara);
            sb.AppendLine("Pior vendedor: "  + relatorio.PiorVendedor);

            Directory.CreateDirectory(arquivoSaida.Substring(0, arquivoSaida.LastIndexOf("\\")));
            File.Create(arquivoSaida).Close();
            File.WriteAllText(arquivoSaida, sb.ToString());
        }
    }
}
