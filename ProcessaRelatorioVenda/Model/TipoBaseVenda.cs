using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessaRelatorioVenda.Model
{
    public abstract class TipoBaseVenda
    {
        public TipoBaseVenda(int tipo)
        {
            this.Tipo = (TipoEnum)Enum.Parse(typeof(TipoEnum), tipo.ToString());
        }

        public int Id { get; set; }

        private TipoEnum _tipo { get; set; }
        public TipoEnum Tipo
        {
            get { return _tipo; }
            set
            {
                Id = (int)value;
                _tipo = value;
            }
        }

        public abstract Task<TipoBaseVenda> ProcessaLinha(string linha, int indexLinha, string arquivo);

        public static int BuscaTipo(string linha)
        {
            var tipoId = linha.Substring(0, 3);
            int tipo = Int32.Parse(tipoId);
            if (tipo > 3)
                throw new FormatException();

            return tipo;
        }
    }
}
