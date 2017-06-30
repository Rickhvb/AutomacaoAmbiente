namespace Automacao_Ambiente
{
    partial class frmInicio
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInicio));
            this.pbxScenario = new System.Windows.Forms.PictureBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxScenario)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxScenario
            // 
            this.pbxScenario.Image = ((System.Drawing.Image)(resources.GetObject("pbxScenario.Image")));
            this.pbxScenario.Location = new System.Drawing.Point(245, 91);
            this.pbxScenario.Name = "pbxScenario";
            this.pbxScenario.Size = new System.Drawing.Size(279, 211);
            this.pbxScenario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxScenario.TabIndex = 3;
            this.pbxScenario.TabStop = false;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(326, 311);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(130, 76);
            this.btnIniciar.TabIndex = 5;
            this.btnIniciar.Text = "INICIAR";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(802, 552);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.pbxScenario);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "frmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AUTOMAÇÃO DE AMBIENTES";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInicio_FormClosing);
            this.Load += new System.EventHandler(this.frmInicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxScenario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbxScenario;
        private System.Windows.Forms.Button btnIniciar;
    }
}

