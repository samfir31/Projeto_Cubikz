using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Beta_2._0
{
    public partial class SelecionarProduto : Form
    {
        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_produto;

        BindingSource bs_produto = new BindingSource();

        String _query;

        public SelecionarProduto()
        {
            InitializeComponent();
        }

        private void SelecionarProduto_Load(object sender, EventArgs e)
        {
            carregar_grid();
        }

        public void carregar_grid()
        {
            _query = "Select * from Produto";
            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
            dr_produto = _dataCommand.ExecuteReader();
            if (dr_produto.HasRows == true)
            {
                bs_produto.DataSource = dr_produto;
                dgvBanco.DataSource = bs_produto;
                igualar_text();
            }
            else
            {
                MessageBox.Show("Não temos clientes cadastrados !!!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void igualar_text()
        {
            textBox1.DataBindings.Add("Text", bs_produto, "Nome_produto");
            textBox1.Clear();
        }

        

        private void dgvBanco_Click(object sender, EventArgs e)
        {
            igualar_text();
        }
    }
}
