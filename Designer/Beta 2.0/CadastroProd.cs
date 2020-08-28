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
    public partial class CadastroProd : Form
    {OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_Alunos;

        BindingSource bs_Caminhão = new BindingSource();

        String _query;

        private void carregar_grid()
        {
            _query = "Select * from Produto";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_Alunos = _dataCommand.ExecuteReader();

            if (dr_Alunos.HasRows == true)
            {
                bs_Caminhão.DataSource = dr_Alunos;
                dgv_Prod.DataSource = bs_Caminhão;
            }
        }
        private void igualar_text()
        {
            lblProd.DataBindings.Add("Text", bs_Caminhão, "Cod_produto");
            lblProd.DataBindings.Clear();

            txtTipo.DataBindings.Add("Text", bs_Caminhão, "Tipo_produto");
            txtTipo.DataBindings.Clear();

            txtNome.DataBindings.Add("Text", bs_Caminhão, "Nome_produto");
            txtNome.DataBindings.Clear();

            txtLarg.DataBindings.Add("Text", bs_Caminhão, "Largura");
            txtLarg.DataBindings.Clear();

            txtAlt.DataBindings.Add("Text", bs_Caminhão, "Altura");
            txtAlt.DataBindings.Clear();

            txtComp.DataBindings.Add("Text", bs_Caminhão, "Comprimento");
            txtComp.DataBindings.Clear();

           
            txtLimite.DataBindings.Add("Text", bs_Caminhão, "Peso");
            txtLimite.DataBindings.Clear();



        }
        private void limpar()
        {
            lblProd.Text = "";
            txtNome.Clear();
            txtLarg.Clear();
            txtAlt.Clear();
            txtComp.Clear();
            txtLimite.Clear();
            txtTipo.Clear();

        }
        private bool valida()
        {
            bool erro = true;
            if (txtNome.Text == "")
            {
                MessageBox.Show("Nome inválido. Redigite !!", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNome.Focus();
            }

            else if (txtLarg.Text == "")
            {
                MessageBox.Show("Largura inválida. Redigite !!", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLarg.Focus();
            }
            else if (txtAlt.Text == "")
            {
                MessageBox.Show("Altura inválida. Redigite !!", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAlt.Focus();
            }

            else if (txtLimite.Text == "")
            {
                MessageBox.Show("Limite de peso inválido. Redigite !!", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLimite.Focus();
            }
            else if (txtTipo.Text == "")
            {
                MessageBox.Show("Limite de peso inválido. Redigite !!", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTipo.Focus();
            }

            else
            {
                erro = false;
            }

            return erro;
        }

        public CadastroProd()
        {
            InitializeComponent();
        }

        private void CadastroProd_Load(object sender, EventArgs e)
        {
            carregar_grid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bs_Caminhão.MoveFirst();
            // atualiza os campos do formulário com o registro posicionado na memória 
            igualar_text();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Count == bs_Caminhão.Position + 1)
                MessageBox.Show("Fim de arquivo encontrado!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_Caminhão.MoveNext();
            igualar_text();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Position == 0)
                MessageBox.Show("Inicio de arquivo encontrado !!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_Caminhão.MovePrevious();
            igualar_text();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            bs_Caminhão.MoveLast();
            // atualiza os campos do formulário com o registro posicionado na memória 
            igualar_text();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            limpar();
            txtNome.Focus();
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            _query = "Select * from Produto where Nome_produto like '" + txtPesquisar.Text + "%'";
            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
            dr_Alunos = _dataCommand.ExecuteReader();

            if (dr_Alunos.HasRows == true)
            {
                bs_Caminhão.DataSource = dr_Alunos;
            }
            else
            {
                MessageBox.Show("Não existem produtos com esse nome  !!!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPesquisar.Text = "";
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            bool teste; // variável para receber o valor de "erro" após executar valida
            teste = valida(); // executando "valida" e armazenando o resultado na variável "erro"
            if (teste == false)
            {
                _query = "Insert into Produto (Nome_produto, Tipo_produto, Largura, Altura, Comprimento, Peso) Values ";
                _query += "('" + txtNome.Text + "','" + txtTipo.Text + "','" + txtLarg.Text + "','" + txtAlt.Text + "','" + txtComp.Text + "','" + txtLimite.Text + "')";
                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                    carregar_grid();
                    MessageBox.Show("Incluido com sucesso !!!!", "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception)
                {
                    MessageBox.Show("Problemas com a Inclusão  !!!!", "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _query = "delete from Produto where Cod_produto like '" + lblProd.Text + "'";
            try
            {
                OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                _dataCommand.ExecuteNonQuery();
                carregar_grid();
                MessageBox.Show("Excluido com sucesso !!!!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Problemas com a Exclusão  !!!!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            igualar_text();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool teste; // variável para receber o valor de "erro" após executar valida
            teste = valida(); // executando "valida" e armazenando o resultado na variável "erro"
            if (teste == false)
            {

                _query = "Update Produto set Nome_produto ='" + txtNome.Text + "',";
                _query += "Tipo_produto = '" + txtTipo.Text + "',";
                _query += "Largura = '" + txtLarg.Text + "',";
                _query += "Altura = '" + txtAlt.Text + "',";
                _query += "Comprimento = '" + txtComp.Text + "',";
                _query += "Peso = '" + txtLimite.Text + "'";
                _query += "where Cod_produto like '" + lblProd.Text + "'";

                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                    carregar_grid();
                    MessageBox.Show("Alterado com sucesso !!!!", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception)
                {
                    MessageBox.Show("Problemas com a Alteração  !!!!", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void receber_valores(String nome, String largura, String comprimento, String altura)
        {
            txtNome.Text = nome;
            txtLarg.Text = largura;
            txtComp.Text = comprimento;
            txtAlt.Text = altura;
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
                
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
