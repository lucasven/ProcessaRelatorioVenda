using ProcessaRelatorioVenda.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProcessaRelatorioVenda.Service
{
    public class GeradorRelatorioFinal
    {
        public RelatorioSaida Gerar(List<TipoBaseVenda> dados)
        {
            List<Venda> listaVenda = new List<Venda>();
            List<Vendedor> listaVendedores = new List<Vendedor>();
            List<Cliente> listaClientes = new List<Cliente>();

            foreach (var item in dados)
            {
                Venda venda = item as Venda;
                Vendedor vendedor = item as Vendedor;
                Cliente cliente = item as Cliente;
                if (venda != null)
                    listaVenda.Add(venda);
                if (vendedor != null)
                    listaVendedores.Add(vendedor);
                if (cliente != null)
                    listaClientes.Add(cliente);
            }
            var retorno = new RelatorioSaida();

            retorno.NumeroClientes = listaClientes.Count;
            retorno.NumeroVendedores = listaVendedores.Count;
            retorno.IdVendaMaisCara = listaVenda.FirstOrDefault(d => d.TotalVenda == listaVenda.Max(c => c.TotalVenda)).SaleId;
            List<KeyValuePair<string, double>> VendedoresTotalVendas = new List<KeyValuePair<string, double>>();
            foreach (var item in listaVenda)
            {
                if(VendedoresTotalVendas.Exists(c => c.Key == item.SalesmanName))
                {
                    //atualiza
                    var idxVendedor = VendedoresTotalVendas.FindIndex(c => c.Key == item.SalesmanName);
                    VendedoresTotalVendas[idxVendedor] = new KeyValuePair<string, double>(item.SalesmanName, VendedoresTotalVendas[idxVendedor].Value + item.TotalVenda);
                }
                else
                {
                    //insere
                    VendedoresTotalVendas.Add(new KeyValuePair<string, double>(item.SalesmanName, item.TotalVenda));
                }
            }
            retorno.PiorVendedor = VendedoresTotalVendas.FirstOrDefault(d => d.Value == VendedoresTotalVendas.Min(c => c.Value)).Key;

            return retorno;
        }
    }
}
