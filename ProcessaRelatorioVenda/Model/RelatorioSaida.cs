using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessaRelatorioVenda.Model
{
    public class RelatorioSaida
    {
        public int NumeroClientes { get; set; }
        public int NumeroVendedores { get; set; }
        public int IdVendaMaisCara { get; set; }
        public string PiorVendedor { get; set; }
    }
}
