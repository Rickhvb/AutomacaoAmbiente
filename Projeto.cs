using System;

public class Projeto
{
    private string nomeProjeto;
    private string nomeCliente;
    private string endereco;
    private DateTime dataAtualizacao;

    public string NomeProjeto { get => nomeProjeto; set => nomeProjeto = value; }
    public string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
    public string Endereco { get => endereco; set => endereco = value; }
    public DateTime DataAtualizacao { get => dataAtualizacao; set => dataAtualizacao = value; }

    public Projeto()
	{
	}

    public void AdicionarAmbiente(Ambiente AAmbiente)
    {

    }

    public void RemoverAmbiente(Ambiente AAmbiente)
    {

    }

    public void SalvarProjeto()
    {
        Projeto overview = new Projeto();
        overview.NomeProjeto = "Teste";
        System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(Projeto));

        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//Teste.xml";
        System.IO.FileStream file = System.IO.File.Create(path);

        writer.Serialize(file, overview);
        file.Close();
    }

    public void CarregarProjeto()
    {
    }
}
