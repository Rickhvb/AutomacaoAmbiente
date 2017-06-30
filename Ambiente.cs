using System;
using System.IO;

public class Ambiente
{
    private string nome;
    private MemoryStream foto;
    
    public string Nome { get => nome; set => nome = value; }
    public MemoryStream Foto { get => foto; set => foto = value; }

    public Ambiente()
    {
    }

    public Ambiente(string ANome)
    {
        nome = ANome;
    }

    public void AplicarFoto(MemoryStream AFoto)
    {
        foto = AFoto;
    }

    public void AdicionarDispositivo(DadosDispositivo ADispositivo)
    {
    }

    public void RemoverDispositivo(DadosDispositivo ADispositivo)
    {
    }
}
