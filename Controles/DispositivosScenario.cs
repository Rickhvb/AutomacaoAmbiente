using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controles
{
    public class DispositivosScenario : DadosDispositivo
    {
        public enum enumDispositivosScenario { ModuloIluminacao = 1, Teclados = 2, ModuloControleCortina = 3 };
        private enumDispositivosScenario tipo;

        public enumDispositivosScenario Tipo { get => tipo; set => tipo = value; }

        public DispositivosScenario()
        {
        }

        public DispositivosScenario(long AId, string ANome, enumDispositivosScenario ATipo)
        {
            this.Id = AId;
            this.Nome = ANome;
            Tipo = ATipo;
        }
    }

}
