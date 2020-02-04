using ProcessaRelatorioVenda.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessaRelatorioVenda.Service
{
    public class ProcessaEntrada
    {
        public async void ProcessaDados(string caminho)
        {
            var tasks = BuscaArquivos(caminho).ToList().Select(c => ProcessaArquivoUnico(c, caminho));
            await Task.WhenAll(tasks);
        }

        private async Task ProcessaArquivoUnico(string arquivo, string caminho)
        {
            await Task.Run(async () =>
                {
                    if (!ArquivoSaidaJaExiste(arquivo))
                    {
                        var conteudoArquivo = await RetornaConteudo(arquivo);
                        var retornoParcial = await new LeArquivo().RetornaDadosArquivo(conteudoArquivo, arquivo);
                        var dadosFinais = new GeradorRelatorioFinal().Gerar(retornoParcial);
                        new GravaArquivo().Grava(arquivo, dadosFinais);
                    }
                });
        }
        public bool ArquivoSaidaJaExiste(string arquivoEntrada)
        {
            return File.Exists(arquivoEntrada.Replace("\\data\\in", "\\data\\out"));
        }

        public List<string> BuscaArquivos(string path)
        {
            try
            {
                return Directory.EnumerateFiles(path).ToList();
            }
            catch(System.IO.DirectoryNotFoundException)
            {
                Logger.LogarErro($"A pasta {path} não existe");
            }

            return new List<string>();
        }

        public async Task<string[]> RetornaConteudo(string arquivo)
        {
            return await File.ReadAllLinesAsync(arquivo, Encoding.UTF7);
        }
    }
}
