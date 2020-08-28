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
    public partial class Calculo : Form
    {
        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_Alunos;

        BindingSource bs_Caminhão = new BindingSource();

        String _query;
        int m3Cam;
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

            lblTipo.DataBindings.Add("Text", bs_Caminhão, "Tipo_produto");
            lblTipo.DataBindings.Clear();

            lblNome.DataBindings.Add("Text", bs_Caminhão, "Nome_produto");
            lblNome.DataBindings.Clear();

            lblLarg.DataBindings.Add("Text", bs_Caminhão, "Largura");
            lblLarg.DataBindings.Clear();

            lblAlt.DataBindings.Add("Text", bs_Caminhão, "Altura");
            lblAlt.DataBindings.Clear();

            lblComp.DataBindings.Add("Text", bs_Caminhão, "Comprimento");
            lblComp.DataBindings.Clear();


            lblPeso.DataBindings.Add("Text", bs_Caminhão, "Peso");
            lblPeso.DataBindings.Clear();




        }
        public Calculo()
        {
            InitializeComponent();
        }
        public Calculo(String CamName, string CamLarg,string CamAlt,string CamComp, string CamPeso)
        {
            InitializeComponent();

            lbl_nome_cam.Text =  CamName;
            lbl_larg_cam.Text = CamLarg;
            lbl_alt_cam.Text = CamAlt;
            lbl_comp_cam.Text = CamComp;
            lbl_peso_cam.Text = CamPeso;

            int altura = Convert.ToInt32(CamAlt);
            int Largura = Convert.ToInt32(CamLarg);
            int Comprimento = Convert.ToInt32(CamComp);
            int Peso = Convert.ToInt32(CamPeso);

            m3Cam = ((altura * Largura)*Comprimento);


            MessageBox.Show("M3 do caminhão = " + m3Cam);


        }
        private void Calcular_Load(object sender, EventArgs e)
        {
            carregar_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Position == 0)
                MessageBox.Show("Inicio de arquivo encontrado !!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_Caminhão.MovePrevious();
            igualar_text();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bs_Caminhão.Count == bs_Caminhão.Position + 1)
                MessageBox.Show("Fim de arquivo encontrado!!!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                bs_Caminhão.MoveNext();
            igualar_text();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtPorcentagem.Text == "100")
            {
                MessageBox.Show("Caminhão lotado", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPorcentagem.Focus();
            }


            //int trabson = Convert.ToInt32(txtPorcentagem.Text);

            int altura = Convert.ToInt32(lblAlt.Text);

            int largura = Convert.ToInt32(lblLarg.Text);

            int comprimento = Convert.ToInt32(lblComp.Text);

            int Peso = Convert.ToInt32(lblPeso.Text);


            int m2Box = (altura * largura * comprimento);


            int qtd = Convert.ToInt32(cmb_qtd.Text);
           
            
            int trab = (qtd*m2Box);

            int trabalho = (trab - m3Cam);
            int coroi = (m3Cam / trabalho);
            MessageBox.Show("Igual a : " + coroi);


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        
        }

    }
}