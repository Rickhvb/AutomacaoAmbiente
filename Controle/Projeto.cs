using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Controle
{
    public class Projeto
    {
        private string nomeProjeto;
        private string nomeCliente;
        private string endereco;
        private DateTime dataAtualizacao;
        private List<Ambiente> ambientes = new List<Ambiente>();
        public string NomeProjeto { get => nomeProjeto; set => nomeProjeto = value; }
        public string NomeCliente { get => nomeCliente; set => nomeCliente = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public DateTime DataAtualizacao { get => dataAtualizacao; set => dataAtualizacao = value; }
        public List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }

        public Projeto()
        {
        }

        //Adiciona um ambiente ao projeto
        public void AdicionarAmbiente(Ambiente AAmbiente)
        {
            Ambientes.Add(AAmbiente);
        }

        //Remove um ambiente da lista de ambientes cadastrados
        public void RemoverAmbiente(Ambiente AAmbiente)
        {
            foreach (Ambiente ambiente in Ambientes)
            {
                if (AAmbiente.Nome == ambiente.Nome)
                {
                    Ambientes.Remove(AAmbiente);
                }
            }
        }
        
        //Salva o projeto no formato xml
        public void SalvarProjeto()
        {
            // Mostra uma caixa de diálogo para o usuário salvar o projeto.
            SaveFileDialog caixaDeDialogo = new SaveFileDialog();
            caixaDeDialogo.Filter = "Arquivo XML|*.xml";
            caixaDeDialogo.Title = "Salvar um Projeto";
            if (caixaDeDialogo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //variável path que armazena o caminho do arquivo a ser salvo
                    var path = caixaDeDialogo.FileName;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    //Escreve todas as informações com o auxílio da classe XmlTextWriter
                    if (!System.IO.File.Exists(path))
                    {
                        XmlTextWriter writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                        writer.WriteStartDocument(true);
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        writer.WriteStartElement("Projeto");
                        writer.WriteStartElement("NomeDoProjeto");
                        writer.WriteString(NomeProjeto.ToString());
                        writer.WriteEndElement();

                        writer.WriteStartElement("NomeDoCliente");
                        writer.WriteString(NomeCliente.ToString());
                        writer.WriteEndElement();

                        writer.WriteStartElement("Endereço");
                        writer.WriteString(Endereco.ToString());
                        writer.WriteEndElement();

                        writer.WriteStartElement("Data");
                        writer.WriteString(Convert.ToString(DataAtualizacao));
                        writer.WriteEndElement();

                        writer.WriteStartElement("Ambientes");
                        foreach (Ambiente ambiente in Ambientes)
                        {
                            writer.WriteStartElement(ambiente.Nome.ToString());
                            
                            foreach (DadosDispositivo dispositivo in ambiente.Lista)
                            {
                                writer.WriteStartElement("Dispositivo");
                                writer.WriteString(dispositivo.Nome.ToString());
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    MessageBox.Show("Projeto exportado com sucesso.", "Confirmação",
                            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
               
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao ler arquivo. Erro: " + ex.Message);
                }
            }
        }

        //Lê um arquivo xml e armazena as informações para serem exibidas
        public Projeto CarregarProjeto(string caminho)
        {
            DataSet dsResultado = new DataSet();
            Projeto projeto = new Projeto();
            dsResultado.ReadXml(caminho);

            projeto.NomeProjeto = dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["NomeDoProjeto"].ToString();
            projeto.NomeCliente = dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["NomeDoCliente"].ToString();
            projeto.Endereco = dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["Endereço"].ToString();
            projeto.DataAtualizacao = Convert.ToDateTime(dsResultado.Tables[0].Rows[dsResultado.Tables[0].Rows.Count - 1]["Data"]);
            
            return projeto;
        }

        //Altera a imagem do ambiente selecionado
        public void AlterarFoto(MemoryStream file, String nomeAmbiente)
        {
            //Percorre os ambientes cadastrados e muda a imagem do ambiente selecionado
            foreach (Ambiente ambiente in Ambientes)
            {
                if (ambiente.Nome == nomeAmbiente)
                {
                    ambiente.Foto = file;
                    break;
                }
            }
        }
    }
}
