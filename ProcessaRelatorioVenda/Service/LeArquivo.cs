using ProcessaRelatorioVenda.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessaRelatorioVenda.Service
{
    public class LeArquivo
    {
        List<TipoBaseVenda> retorno = new List<TipoBaseVenda>();

        public async Task<List<TipoBaseVenda>> RetornaDadosArquivo(string[] linhas, string arquivo)
        {
            
            try
            {
                var tasks = linhas.ToList().Select(c => PreProcessaLinha(c, linhas.ToList().FindIndex(d => d == c), arquivo));

                await Task.WhenAll(tasks);
            }
            catch(FormatException)
            {
                Logger.LogarErro("Identificador de tipo não suportado");
            }
            return retorno;
        }

        public async Task PreProcessaLinha(string linha, int indexLinha, string arquivo)
        {
            var tipo = TipoBaseVenda.BuscaTipo(linha);

            TipoBaseVenda dado;
            switch (tipo)
            {
                case 1:
                    dado = new Vendedor(tipo);
                    break;
                case 2:
                    dado = new Cliente(tipo);
                    break;
                case 3:
                    dado = new Venda(tipo);
                    break;
                default:
                    dado = new Vendedor(tipo);
                    break;
            }

            await Task.WhenAll(dado.ProcessaLinha(linha, indexLinha, arquivo));

            retorno.Add(dado);
        }
    }
}
