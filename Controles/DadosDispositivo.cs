using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controles
{
    public class DadosDispositivo
    {
        private long id;
        private string nome;

        public long Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }

        public DadosDispositivo()
        {
        }

        public DadosDispositivo(long AId, string ANome)
        {
            id = AId;
            nome = ANome;
        }

    }

}
