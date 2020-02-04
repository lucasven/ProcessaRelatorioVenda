using ProcessaRelatorioVenda.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessaRelatorioVenda.Model
{
    public class Venda : TipoBaseVenda
    {
        public Venda(int tipo) : base(tipo)
        {
            Itens = new List<VendaDetalhe>();
        }
        public int SaleId { get; set; }
        public List<VendaDetalhe> Itens { get; set; }
        public string SalesmanName { get; set; }
        public double TotalVenda
        {
            get
            {
                return Itens.Sum(c => c.ItemPrice * c.ItemQuantity);
            }
        }

        public override async Task<TipoBaseVenda> ProcessaLinha(string linha, int index, string arquivo)
        {
            await Task.Run(() =>
            {
                try
                {
                    var dados = linha.Split('ç');
                    if (dados.Length < 3)
                        throw new FormatException();
                    this.SaleId = Int32.Parse(dados[1]);
                    this.SalesmanName = dados[3];

                    var listaVendas = dados[2].Substring(1, dados[2].Length -2).Split(',');
                    foreach (var venda in listaVendas)
                    {
                        var dadosVenda = venda.Split('-');
                        Itens.Add(
                            new VendaDetalhe
                            {
                                ItemId = Int32.Parse(dadosVenda[0]),
                                ItemQuantity = Int32.Parse(dadosVenda[1]),
                                ItemPrice = Double.Parse(dadosVenda[2])
                            });

                    }
                }
                catch (FormatException)
                {
                    Logger.LogarErroLeituraLinha(index, arquivo);
                }
            });
            return this;
        }
    }

    public class VendaDetalhe
    {
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public double ItemPrice { get; set; }
    }
}
