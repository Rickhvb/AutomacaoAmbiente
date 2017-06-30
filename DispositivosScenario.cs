using System;

public class DispositivosScenario : DadosDispositivo
{
    private enum enumDispositivosScenario { ModuloIluminacao = 1, Teclados = 2, ModuloControleCortina = 3 };
    private enumDispositivosScenario tipo;

    public DispositivosScenario()
	{
	}

    public DispositivosScenario(long AId, string ANome, enumDispositivosScenario ATipo)
    {
        this.id = AId;
        this.nome = ANome;
        tipo = ATipo;
    }
}
