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
    public partial class CadastroCam : Form
    {
        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_Caminhao;

        BindingSource bs_Caminhão = new BindingSource();

        String _query;

        private void carregar_grid()
        {
            _query = "Select * from Caminhao";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_Caminhao = _dataCommand.ExecuteReader();

            if (dr_Caminhao.HasRows == true)
            {
                bs_Caminhão.DataSource = dr_Caminhao;
                dgv_caminhao.DataSource = bs_Caminhão;
            }
        }

        public CadastroCam()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CadastroCam_Load(object sender, EventArgs e)
        {
            carregar_grid();
        }
        private void igualar_text()
        {
            lblCam.DataBindings.Add("Text", bs_Caminhão, "Cod_caminhao");
            lblCam.DataBindings.Clear();

            txtNome.DataBindings.Add("Text", bs_Caminhão, "Nome_Caminhao");
            txtNome.DataBindings.Clear();

            txtLarg.DataBindings.Add("Text", bs_Caminhão, "Largura");
            txtLarg.DataBindings.Clear();

            txtAlt.DataBindings.Add("Text", bs_Caminhão, "Altura");
            txtAlt.DataBindings.Clear();

            txtComp.DataBindings.Add("Text", bs_Caminhão, "Comprimento");
            txtComp.DataBindings.Clear();

            txtLimite.DataBindings.Add("Text", bs_Caminhão, "Limite_peso");
            txtLimite.DataBindings.Clear();

            txtEixos.DataBindings.Add("Text", bs_Caminhão, "Tipo_De_Caminhao");
            txtEixos.DataBindings.Clear();

        }
        private void limpar()
        {
            lblCam.Text = "";
            txtNome.Clear();
            txtLarg.Clear();
            txtAlt.Clear();
            txtComp.Clear();
            txtLimite.Clear();

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

            else
            {
                erro = false;
            }

            return erro;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            limpar();
            txtNome.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool teste; // variável para receber o valor de "erro" após executar valida
            teste = valida(); // executando "valida" e armazenando o resultado na variável "erro"
            if (teste == false)
            {
                _query = "Insert into Caminhao (Nome_Caminhao, Largura, Altura, Comprimento, Limite_peso, Tipo_De_Caminhao) Values ";
                _query += "('" + txtNome.Text + "','" + txtLarg.Text + "','" + txtAlt.Text + "','" + txtComp.Text + "','" + txtLimite.Text + "','" + txtEixos.Text + "')";
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

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            _query = "Select * from Caminhao where Nome_Caminhao like '" + txtPesquisar.Text + "%'";
            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
            dr_Caminhao = _dataCommand.ExecuteReader();

            if (dr_Caminhao.HasRows == true)
            {
                bs_Caminhão.DataSource = dr_Caminhao;
            }
            else
            {
                MessageBox.Show("Não existem caminhões com esse código no registro  !!!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPesquisar.Text = "";
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs_Caminhão.MoveFirst();
            // atualiza os campos do formulário com o registro posicionado na memória 
            igualar_text();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bs_Caminhão.MoveLast();
            // atualiza os campos do formulário com o registro posicionado na memória 
            igualar_text();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Count == bs_Caminhão.Position + 1)
                MessageBox.Show("Fim de arquivo encontrado!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_Caminhão.MoveNext();
            igualar_text();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Position == 0)
                MessageBox.Show("Inicio de arquivo encontrado !!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_Caminhão.MovePrevious();
            igualar_text();
        }

        private void txtComp_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtLarg_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void dgv_caminhao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            igualar_text();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            _query = "delete from Caminhao where Cod_caminhao like '" + lblCam.Text + "'";
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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            bool teste; // variável para receber o valor de "erro" após executar valida
            teste = valida(); // executando "valida" e armazenando o resultado na variável "erro"
            if (teste == false)
            {

                _query = "Update Caminhao set Nome_Caminhao ='" + txtNome.Text + "',";
                _query += "Largura = '" + txtLarg.Text + "',";
                _query += "Altura = '" + txtAlt.Text + "',";
                _query += "Comprimento = '" + txtComp.Text + "',";
                _query += "Limite_peso = '" + txtLimite.Text + "',";
                _query += "Tipo_De_Caminhao = '" + txtEixos.Text + "'";
                _query += "where Cod_caminhao like '" + lblCam.Text + "'";

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

       

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        


    }
}