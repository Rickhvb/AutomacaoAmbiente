using System;

public class DispositivosComuns : DadosDispositivo
{
    private string fabricante;
    public string Fabricante { get => fabricante; set => fabricante = value; }

    public DispositivosComuns()
	{
	}

    public DispositivosComuns(long AId, string ANome, string AFabricante)
    {
        this.id = AId;
        this.nome = ANome;
        fabricante = AFabricante;
    }
}
