namespace Beta_2._0
{
    partial class SelecionarProduto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvBanco = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanco)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBanco
            // 
            this.dgvBanco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBanco.Location = new System.Drawing.Point(50, 32);
            this.dgvBanco.Name = "dgvBanco";
            this.dgvBanco.Size = new System.Drawing.Size(240, 150);
            this.dgvBanco.TabIndex = 0;
            this.dgvBanco.Click += new System.EventHandler(this.dgvBanco_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 226);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // SelecionarProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 262);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvBanco);
            this.Name = "SelecionarProduto";
            this.Text = "SelecionarProduto";
            this.Load += new System.EventHandler(this.SelecionarProduto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBanco)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBanco;
        private System.Windows.Forms.TextBox textBox1;
    }
}