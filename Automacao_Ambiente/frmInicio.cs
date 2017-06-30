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

namespace Automacao_Ambiente
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }
        
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                frmCadastro frmCadastro = new frmCadastro();
                frmCadastro.ShowDialog();
            }
            finally
            {
                this.Show();
            }
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {

        }

        //Após o usuário clicar no ícone de fechar, uma confirmação aparece
        private void frmInicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(this, "Deseja realmente sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
