using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controles
{
    public class DispositivosComuns : DadosDispositivo
    {
        private string fabricante;
        public string Fabricante { get => fabricante; set => fabricante = value; }

        public DispositivosComuns()
        {
        }

        public DispositivosComuns(long AId, string ANome, string AFabricante)
        {
            this.Id = AId;
            this.Nome = ANome;
            fabricante = AFabricante;
        }
    }

}
