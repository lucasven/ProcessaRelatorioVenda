using ProcessaRelatorioVenda.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessaRelatorioVenda.Model
{
    public class Vendedor : TipoBaseVenda
    {
        public Vendedor(int tipo):base(tipo)
        {

        }
        public string CPF { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }

        public override async Task<TipoBaseVenda> ProcessaLinha(string linha, int index, string arquivo)
        {
            await Task.Run(() =>
            {
                try
                {
                    var dados = linha.Split('ç');
                    if (dados.Length < 3)
                        throw new FormatException();
                    this.CPF = dados[1];
                    this.Name = dados[2];
                    this.Salary = Double.Parse(dados[3]);
                }
                catch (FormatException)
                {
                    Logger.LogarErroLeituraLinha(index, arquivo);
                }
            });

            return this;
        }
    }
}
