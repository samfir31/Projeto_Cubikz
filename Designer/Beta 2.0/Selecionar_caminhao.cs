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
    public partial class Selecionar_caminhao : Form
    {
        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_Alunos;

        BindingSource bs_Caminhão = new BindingSource();

        String _query;

        private void carregar_grid()
        {
            _query = "Select * from Caminhao";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_Alunos = _dataCommand.ExecuteReader();

            if (dr_Alunos.HasRows == true)
            {
                bs_Caminhão.DataSource = dr_Alunos;
                dgv_caminhao.DataSource = bs_Caminhão;
            }
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

        public Selecionar_caminhao()
        {
            InitializeComponent();
        }

        private void Selecionar_caminhao_Load(object sender, EventArgs e)
        {
            carregar_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Count == bs_Caminhão.Position + 1)
            {

            }
            else
            {
                bs_Caminhão.MovePrevious();
                igualar_text();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Count == bs_Caminhão.Position + 1)
            {

            }
            else
            {
                bs_Caminhão.MoveNext();
                igualar_text();
            }
        }

        private void btn_Selecionar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtAlt.Text == "" || txtLarg.Text == "" || txtComp.Text == "")
            {

            }
            else
            {
                Alocacao2 a = new Alocacao2();
                a.nomeCaminhao = txtNome.Text;
                a.alturaCaminhao = 100 * (Convert.ToDouble(txtAlt.Text));
                a.larguraCaminhao = 100 * (Convert.ToDouble(txtLarg.Text));
                a.comprimentoCaminhao = 100 * (Convert.ToDouble(txtComp.Text));
                a.limiteCaminhao = 1000 * (Convert.ToDouble(txtLimite.Text));
                a.tipoCaminhao = txtEixos.Text;
                a.Show();
                this.Hide();
                a.Closed += (s, args) => this.Close();
                a.Show();
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Form1();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dgv_caminhao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            igualar_text();
        }
    }
}
