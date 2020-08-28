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
    public partial class BancoProduto : Form
    {
        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_produto;

        BindingSource bs_produto = new BindingSource();

        String _query;

        public string quem, nome;

        public int altura, largura, comprimento;
        

        public BancoProduto()
        {
            InitializeComponent();
        }

        private void BancoProduto_Load(object sender, EventArgs e)
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
            txtnome.DataBindings.Add("Text", bs_produto, "Nome_produto");
            txtnome.DataBindings.Clear();   
            txtaltura.DataBindings.Add("Text", bs_produto, "Altura");
            txtaltura.DataBindings.Clear();
            txtlargura.DataBindings.Add("Text", bs_produto, "Largura");
            txtlargura.DataBindings.Clear();
            txtcomprimento.DataBindings.Add("Text", bs_produto, "Comprimento");
            txtcomprimento.DataBindings.Clear();
        }

        private void dgvBanco_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            igualar_text();
        }

        private void Selecionar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cubagem a = new Cubagem();
            a.alterar_valores(txtnome.Text, txtaltura.Text, txtlargura.Text, txtcomprimento.Text);
            a.Closed += (s, args) => this.Close();
            a.Show();
        }

        private void Proximo_Click(object sender, EventArgs e)
        {
            if (bs_produto.Count == bs_produto.Position + 1)
                MessageBox.Show("Fim de arquivo encontrado!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_produto.MoveNext();
            igualar_text();
        }

        private void Anterior_Click(object sender, EventArgs e)
        {
            if (bs_produto.Position == 0)
                MessageBox.Show("Inicio de arquivo encontrado !!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_produto.MovePrevious();
            igualar_text();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Primeiro_Click(object sender, EventArgs e)
        {
            bs_produto.MoveFirst();
            igualar_text();
        }

        private void Ultimo_Click(object sender, EventArgs e)
        {
            bs_produto.MoveLast();
            igualar_text();
        }
       
        }
        }
    

