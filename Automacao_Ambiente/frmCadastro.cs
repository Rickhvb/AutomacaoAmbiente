using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controle;
using System.Xml;
using System.IO;

namespace Automacao_Ambiente
{
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }
        
        private void frmCadastro_Load(object sender, EventArgs e)
        {
            limpaDados();
            AtualizaComboBox();
            pnlAmbiente.Visible = false;
            pnlCadastro.Visible = false;
            pbxImagemAmbiente.Visible = false;
            lblImagemAmbiente.Visible = false;
        }

        //As comboboxes são preenchidas com as informações dos dispositivos
        private void AtualizaComboBox()
        {
            cmbOutros.Items.Insert(0,"SensorMovimento");
            cmbOutros.Items.Insert(1, "SensorLuz");

            cmbScenario.Items.Insert(0, "MóduloIluminação");
            cmbScenario.Items.Insert(1, "Teclados");
            cmbScenario.Items.Insert(2, "MóduloControleCortina");
        }
        
        Projeto projeto;
        Random randNum = new Random();
        private string pathFoto; 
        public string PathFoto { get => pathFoto; set => pathFoto = value; }

        //Chama a função que salva o projeto no formato xml
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            projeto.SalvarProjeto();
        }

        //Torna visível o painel para se adicionar um novo ambiente
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            pbxImagemAmbiente.Visible = false;
            lblImagemAmbiente.Visible = false;
            pnlAmbiente.Visible = true;
            txtAmbiente.Focus();
        }

        /* Abre uma caixa de diálogo para o usuário carregar uma foto ao ambiente a ser adicionado, 
            através da classe OpenFileDialog */
        private void btnImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog caixaDeDialogo = new OpenFileDialog();
            caixaDeDialogo.Filter = "Imagens (*.JPG; *.PNG)|*.jpg;*.png";
            caixaDeDialogo.Title = "Selecione uma Imagem";
            caixaDeDialogo.CheckFileExists = true;
            caixaDeDialogo.CheckPathExists = true;
            caixaDeDialogo.FilterIndex = 2;
            caixaDeDialogo.RestoreDirectory = true;
            caixaDeDialogo.ReadOnlyChecked = true;
            caixaDeDialogo.ShowReadOnly = true;

            if (caixaDeDialogo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image imagem = Image.FromFile(caixaDeDialogo.FileName);
                    PathFoto = caixaDeDialogo.FileName;
                    pbxImagem.Image = imagem;
                    lblImagemNaoCarregada.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao ler arquivo. Erro: " + ex.Message);
                }
            }
        }

        //Cancela a operação de adicionar um novo ambiente e retorna à página anterior
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cancelar Operação?", "Atenção",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question)
         == DialogResult.Yes)
            {
                limpaCampos();
                dgvDispAdicionados.Rows.Clear();
                dgvDispAdicionados.Refresh();
            }
        }

        //Insere um novo ambiente ao projeto
        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (txtAmbiente.Text != "") //Verifica se o usuário digitou um nome de ambiente
            {
                try
                {
                    Ambiente ambiente = new Ambiente(txtAmbiente.Text);
                    //Caso o usuário carregue uma imagem, a mesma é adicionada ao ambiente através da classe MemoryStream
                    MemoryStream file = new MemoryStream(); 
                    if (pbxImagem.Image != null)
                    {
                        using (FileStream fs = File.OpenRead(PathFoto))
                        {
                            fs.CopyTo(file);
                            ambiente.AplicarFoto(file);
                        }
                    }
                    //Percorre a tabela de dispositivos inseridos e realiza a sua inclusão no ambiente
                    foreach (DataGridViewRow item in dgvDispAdicionados.Rows)
                    {
                        if (item.Cells[0].Value.ToString() == "Módulo Iluminação")
                        {
                            DadosDispositivo dispositivoScenario = new DispositivosScenario
                                (randNum.Next(), item.Cells[0].Value.ToString(), DispositivosScenario.enumDispositivosScenario.ModuloIluminacao);
                            ambiente.AdicionarDispositivo(dispositivoScenario);
                        }
                        else if (item.Cells[0].Value.ToString() == "Teclados")
                        {
                            DadosDispositivo dispositivoScenario = new DispositivosScenario
                                (randNum.Next(), item.Cells[0].Value.ToString(), DispositivosScenario.enumDispositivosScenario.Teclados);
                            ambiente.AdicionarDispositivo(dispositivoScenario);
                        }
                        else if (item.Cells[0].Value.ToString() == "Módulo Controle Cortina")
                        {
                            DadosDispositivo dispositivoScenario = new DispositivosScenario
                                (randNum.Next(), item.Cells[0].Value.ToString(), DispositivosScenario.enumDispositivosScenario.ModuloControleCortina);
                            ambiente.AdicionarDispositivo(dispositivoScenario);
                        }
                        else if (item.Cells[0].Value.ToString() == "Sensor de Movimento")
                        {
                            DadosDispositivo dispositivoComum = new DispositivosComuns
                                (randNum.Next(), item.Cells[0].Value.ToString(), "Samsung");
                            ambiente.AdicionarDispositivo(dispositivoComum);
                        }
                        else
                        {
                            DadosDispositivo dispositivoComum = new DispositivosComuns
                                (randNum.Next(), item.Cells[0].Value.ToString(), "LG");
                            ambiente.AdicionarDispositivo(dispositivoComum);
                        }
                    }
                    projeto.AdicionarAmbiente(ambiente);
                    
                    //Através da classe TreeNode, os ambientes são exibidos de forma hierárquica
                    TreeNode rootNode = tvwAmbientes.Nodes.Add(ambiente.Nome);
                    rootNode.ImageIndex = 0;
                    tvwAmbientes.ItemHeight = 30;
                    //Cada dispositivo é exibido agrupado pelos seus ambientes correspondentes
                    foreach (DataGridViewRow row in dgvDispAdicionados.Rows)
                    {
                         TreeNode child = rootNode.Nodes.Add(row.Cells[0].Value.ToString());
                    }
                    rootNode.TreeView.ExpandAll();
                    dgvDispAdicionados.Rows.Clear();
                    dgvDispAdicionados.Refresh();

                    limpaCampos();
                    MessageBox.Show("Ambiente incluído com sucesso!", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Digite um ambiente.", "Campos necessários",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        
        //Limpa todos os campos após a operação de adicionar um ambiente seja concluída ou cancelada
        private void limpaCampos()
        {
            pnlAmbiente.Visible = false;
            txtAmbiente.Text = null;
            pbxImagem.Image = null;
            lblImagemNaoCarregada.Visible = true;
            cmbOutros.Text = null;
            cmbScenario.Text = null;
            lblNomeDoAmbiente.Text = "";
            lblNomeDoDispositivo.Text = "";
        }

        //Caso o usuário feche a janela do seu projeto, retorna-se à tela inicial
        private void volta()
        {
            this.Hide();
            frmInicio frmInicio = new frmInicio();
            frmInicio.ShowDialog();
            this.Close();
        }
        
        //Remove um ambiente ou dispositivo do projeto
        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (tvwAmbientes.Nodes.Count > 0) //Verifica se há itens cadastrados
            {
                if (lblNomeDoAmbiente.Text != "") //Verifica se um item foi selecionado pra ser removido
                {
                    if (MessageBox.Show("Confirma exclusão do item selecionado?", "Atenção",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                     == DialogResult.Yes)
                    {
                        try
                        {
                            if (lblNomeDoDispositivo.Text == "") //Se lblNomeDoDispositivo for nulo, um ambiente será removido
                            {
                                tvwAmbientes.SelectedNode.Remove(); //Remove da lista o ambiente selecionado
                                Ambiente ambiente = new Ambiente();
                                ambiente.Nome = lblNomeDoAmbiente.Text;
                                projeto.RemoverAmbiente(ambiente); //Remove do projeto o ambiente selecionado
                                lblNomeDoAmbiente.Text = "";
                                lblNomeDoDispositivo.Text = "";
                                pbxImagemAmbiente.Visible = false;
                                lblImagemAmbiente.Visible = false;
                                MessageBox.Show("Item removido do projeto!", "Confirmação",
                                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                            {
                                tvwAmbientes.SelectedNode.Remove();  //Remove do projeto o dispositivo selecionado
                                Ambiente ambiente = new Ambiente();
                                DadosDispositivo dispositivo = new DadosDispositivo();
                                dispositivo.Nome = lblNomeDoDispositivo.Text;
                                ambiente.RemoverDispositivo(dispositivo);  //Remove do projeto o dispositivo selecionado
                                lblNomeDoAmbiente.Text = "";
                                lblNomeDoDispositivo.Text = "";
                                pbxImagemAmbiente.Visible = false;
                                lblImagemAmbiente.Visible = false;
                                MessageBox.Show("Item removido do projeto!", "Confirmação",
                                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Nenhum item selecionado", "Confirmação",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        lblNomeDoAmbiente.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show(this, "Selecione um item para remover", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Sem itens para remover", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Botão que torna visível o painel das informações do novo projeto a ser criado
        private void btnNovoProjeto_Click(object sender, EventArgs e)
        {
            pnlDadosProjeto.Visible = true;
            btnCarregarProjeto.Enabled = false;
            btnNovoProjeto.Enabled = true;
            txtNomeDoCliente.Focus();
            txtData.Text = Convert.ToString(DateTime.Now);
        }

        //Variável para verificar se um arquivo xml foi carregado
        private bool arquivoCarregado = false;

        //Carrega um arquivo xml salvo anteriormente
        private void btnCarregarProjeto_Click(object sender, EventArgs e)
        {
            // Mostra uma caixa de diálogo para selecionar um arquivo xml.
            OpenFileDialog caixaDeDialogo = new OpenFileDialog();
            caixaDeDialogo.Filter = "Arquivo XML|*.xml";
            caixaDeDialogo.Title = "Selecione um Projeto";

            // Mostra a caixa de diálogo e realiza a importação do arquivo
            if (caixaDeDialogo.ShowDialog() == DialogResult.OK)
            {
                arquivoCarregado = true;
                DataSet dsResultado = new DataSet();
                TreeView treeView = new TreeView();
                
                dsResultado.ReadXml(caixaDeDialogo.FileName); //Lê o caminho especificado e o armazena em uma tabela
                projeto = new Projeto();
                projeto = projeto.CarregarProjeto(caixaDeDialogo.FileName);
                MessageBox.Show("Projeto carregado com sucesso!", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                pnlInicio.Visible = false;
                pnlCadastro.Visible = true;
                lblNomeProjeto.Text = projeto.NomeProjeto;
                lblNomeCliente.Text = projeto.NomeCliente;
                lblEndereco.Text = projeto.Endereco;
                lblData.Text = Convert.ToString(projeto.DataAtualizacao);
                /* O arquivo é carregado e lido através das classes FileStream e XmlDataDocument. Após isso,
                   é armazenado em nós hierárquicos através da classe TreeNode e exibido na TreeView*/
                XmlDataDocument xmldoc = new XmlDataDocument();
                XmlNode xmlnode;
                FileStream fs = new FileStream(caixaDeDialogo.FileName, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                xmlnode = xmldoc.ChildNodes[1];
                tvwAmbientes.Nodes.Clear();
                tvwAmbientes.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));
                TreeNode tNode;
                tNode = tvwAmbientes.Nodes[0];
                AddNode(xmlnode, tNode);
                tNode.TreeView.ExpandAll();
            }
        }

        //Adiciona cada nó de um arquivo xml em uma árvore hierárquica
        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i = 0;
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = inXmlNode.InnerText.ToString();
            }
        }

        //Torna visível o painel principal do projeto
        private void btnCriar_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(txtNomeDoCliente.Text) == "" || Convert.ToString(txtNomeDoProjeto.Text) == ""
                || Convert.ToString(txtEndereco.Text) == "" || Convert.ToString(txtData.Text) == "")
            {
                MessageBox.Show("Todos os campos são obrigatórios!", "Campos necessários",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeDoCliente.Focus();
            }
            else
            {
                //Limpa-se todos os campos e um novo projeto é instanciado
                pnlInicio.Visible = false;
                pnlCadastro.Visible = true;
                lblNomeProjeto.Text = txtNomeDoProjeto.Text;
                lblNomeCliente.Text = txtNomeDoCliente.Text;
                lblEndereco.Text = txtEndereco.Text;
                lblData.Text = txtData.Text;
                limpaDados();
                projeto = new Projeto();
                projeto.NomeProjeto = Convert.ToString(lblNomeProjeto.Text);
                projeto.NomeCliente = Convert.ToString(lblNomeCliente.Text);
                projeto.Endereco = Convert.ToString(lblEndereco.Text);
                projeto.DataAtualizacao = Convert.ToDateTime(lblData.Text);
            }
        }

        //Torna visível os botões da tela inicial, caso o usuário cancele a criação de um novo projeto
        private void btnCancelarProjeto_Click(object sender, EventArgs e)
        {
            limpaDados();
            btnCarregarProjeto.Enabled = true;
            btnNovoProjeto.Enabled = true;
        }

        //Após o usuário entrar com as informações básicas do projeto, os campos são limpados
        private void limpaDados()
        {
            pnlDadosProjeto.Visible = false;
            txtNomeDoProjeto.Text = "";
            txtNomeDoCliente.Text = "";
            txtEndereco.Text = "";
            txtData.Text = "";
        }

        //Uma tela de confirmação é mostrada caso o usuário deseje fechar a janela
        private void frmCadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(this, "Deseja realmente sair?\n*Salve qualquer projeto iniciado antes de sair!",
                    "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        //O usuário seleciona dispositivos e os insere numa lista para serem adicionados ao ambiente
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbScenario.Text != "") 
            {
                int count = 0;
                foreach (DataGridViewRow row in dgvDispAdicionados.Rows) //Percorre a lista de dispositos inseridos
                {
                    if (cmbScenario.Text == row.Cells[0].Value.ToString()) 
                    {
                        count++;
                    }
                }
                if (count == 0 || dgvDispAdicionados.Rows.Count == 0)
                {
                    dgvDispAdicionados.Rows.Add(cmbScenario.Text); //Adiciona apenas uma vez cada dispositivo
                }
            }
            if (cmbOutros.Text != "")
            {
                int count2 = 0;
                foreach (DataGridViewRow row in dgvDispAdicionados.Rows)
                {
                    if (cmbOutros.Text == row.Cells[0].Value.ToString())
                    {
                        count2++;
                    }
                }
                if (count2 == 0 || dgvDispAdicionados.Rows.Count == 0)
                {
                    dgvDispAdicionados.Rows.Add(cmbOutros.Text);
                }
            }
        }

        //O usuário seleciona dispositivos e os remove da lista antes de adicioná-los ao ambiente
        private void btnRem_Click(object sender, EventArgs e)
        {
            if (dgvDispAdicionados.Rows.Count > 0)
            {
                dgvDispAdicionados.Rows.RemoveAt(dgvDispAdicionados.CurrentRow.Index);
            }
        }

        //Mostra a imagem de cada ambiente cadastrado após o mesmo ser clicado
        private void tvwAmbientes_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (arquivoCarregado == false)
            {
                TreeNode nNome = e.Node;
                lblNomeDoAmbiente.Text = nNome.Text;

                foreach (Ambiente ambiente in projeto.Ambientes) //Percorre a lista de ambientes cadastrados
                {
                    if (ambiente.Nome == lblNomeDoAmbiente.Text) //Verifica se o item selecionado é um ambiente
                    {
                        pbxImagemAmbiente.Visible = true;
                        lblNomeDoDispositivo.Text = "";
                        if (ambiente.Foto != null) //Verifica se há uma imagem cadastrada e a exibe no painel
                        {
                            lblImagemAmbiente.Visible = false;
                            Image img = Image.FromStream(ambiente.Foto, true);
                            if (pbxImagemAmbiente.Image != null)
                            {
                                pbxImagemAmbiente.Image.Dispose();
                                pbxImagemAmbiente.Image = null;
                            }
                            pbxImagemAmbiente.Image = img;
                            lblImagemAmbiente.Visible = false;
                        }
                        else //Caso não haja imagem, o texto "Imagem Não Carregada" é exibido
                        {
                            pbxImagemAmbiente.Image = null;
                            lblImagemAmbiente.Visible = true;
                        }
                        break;
                    }
                    else //Caso não seja um ambiente, nada á mostrado
                    {
                        pbxImagemAmbiente.Visible = false;
                        lblImagemAmbiente.Visible = false;
                        lblNomeDoDispositivo.Text = lblNomeDoAmbiente.Text;
                    }
                }
            }
        }

        //Remove uma imagem do ambiente
        private void btnRemoverImagem_Click(object sender, EventArgs e)
        {
            pbxImagem.Image = null;
            lblImagemNaoCarregada.Visible = true;
        }

        //Permite que apenas um dispositivo de cada vez seja adicionado à lista de dispositivos
        private void cmbScenario_Click(object sender, EventArgs e)
        {
            cmbOutros.Text = null;
        }

        //Permite que apenas um dispositivo de cada vez seja adicionado à lista de dispositivos
        private void cmbOutros_Click(object sender, EventArgs e)
        {
            cmbScenario.Text = null;
        }

        //Permite editar uma imagem do ambiente selecionado
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tvwAmbientes.Nodes.Count > 0)
            {
                if (lblNomeDoAmbiente.Text != "")
                {
                    if (lblNomeDoDispositivo.Text == "")
                    {
                        try
                        {
                            OpenFileDialog caixaDeDialogo = new OpenFileDialog();
                            caixaDeDialogo.Filter = "Imagens (*.JPG; *.PNG)|*.jpg;*.png";
                            caixaDeDialogo.Title = "Selecione uma Imagem";
                            caixaDeDialogo.CheckFileExists = true;
                            caixaDeDialogo.CheckPathExists = true;
                            caixaDeDialogo.FilterIndex = 2;
                            caixaDeDialogo.RestoreDirectory = true;
                            caixaDeDialogo.ReadOnlyChecked = true;
                            caixaDeDialogo.ShowReadOnly = true;

                            if (caixaDeDialogo.ShowDialog() == DialogResult.OK)
                            {

                                try
                                {
                                    Image imagem = Image.FromFile(caixaDeDialogo.FileName);
                                    PathFoto = caixaDeDialogo.FileName;

                                    MemoryStream file = new MemoryStream();
                                    using (FileStream fs = File.OpenRead(PathFoto))
                                        {
                                        fs.CopyTo(file);
                                        projeto.AlterarFoto(file, lblNomeDoAmbiente.Text);
                                        pbxImagemAmbiente.Visible = false;
                                        lblImagemAmbiente.Visible = false;
                                        MessageBox.Show("Imagem alterada com sucesso!", "Confirmação",
                                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Erro ao ler arquivo. Erro: " + ex.Message);
                                }
                            }
                            else
                            {
                                pbxImagemAmbiente.Visible = false;
                                lblImagemAmbiente.Visible = false;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Nenhum ambiente selecionado", "Confirmação",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Selecione um ambiente para alterar", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Selecione um ambiente para alterar", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Sem ambientes para alterar", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
