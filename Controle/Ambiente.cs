using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Controle
{
    public class Ambiente
    {
        private string nome;
        private MemoryStream foto;
        private List<DadosDispositivo> lista = new List<DadosDispositivo>();
        public string Nome { get => nome; set => nome = value; }
        public MemoryStream Foto { get => foto; set => foto = value; }
        public List<DadosDispositivo> Lista { get => lista; set => lista = value; }


        public Ambiente()
        {
        }

        public Ambiente(string ANome)
        {
            Nome = ANome;
        }

        //Aplica a foto selecionada
        public void AplicarFoto(MemoryStream AFoto)
        {
            Foto = AFoto;
        }

        //Adiciona um dispositivo à lista 
        public void AdicionarDispositivo(DadosDispositivo ADispositivo)
        {
            Lista.Add(ADispositivo);
        }

        //Remove um dispositivo da lista de dispositivos armazenados
        public void RemoverDispositivo(DadosDispositivo ADispositivo)
        {
            foreach (DadosDispositivo dispositivo in Lista)
            {
                if (dispositivo.Nome == ADispositivo.Nome)
                {
                    Lista.Remove(ADispositivo);
                }
            }
        }
    }

}
