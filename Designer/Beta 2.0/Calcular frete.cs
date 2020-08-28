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
    public partial class Calcular_frete : Form
    {
        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_Alunos;

        BindingSource bs_Caminhão = new BindingSource();

        String _query;

        public Calcular_frete()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cubagem tela = new Cubagem();
            tela.Show();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            int pesoMerc = Convert.ToInt32(txtPesoMerc.Text);
            int pesoCubado = Convert.ToInt32(txtPesoCubado.Text);
            int valor = Convert.ToInt32(txtValorMerc.Text);
            int fretePeso = Convert.ToInt32(FretePesoQuilo.Text);
            int porcentagemICMS = 0;
            String origem = txtEstadoOrigem.Text;
            String destino = txtEstadoDestino.Text;

            _query = "Select " + destino + " FROM FreteTeste WHERE Estados like '" + origem + "%'";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_Alunos = _dataCommand.ExecuteReader();

            if (dr_Alunos.Read())
            {
                porcentagemICMS = dr_Alunos.GetInt32(0);
                MessageBox.Show(Convert.ToString(porcentagemICMS));
                Calculo(porcentagemICMS, pesoMerc, pesoCubado, valor, fretePeso);
            }
            else
            {
                MessageBox.Show("Moio" + origem + destino);
            }

        }


        private void Calculo(int porcentagemICMS, int pesoMerc, int pesoCubado, int valor, int fretePeso)
        {
            int freteValor = 5; // não é o valor certo, é só pra ver se ta funcionando

            int taxaDeTDE = 10; //não é o valor certo
            int taxaDeCTRC = 20; //tbm não

            

            int peso;

            if (Convert.ToInt32(pesoCubado) > Convert.ToInt32(pesoMerc))
            {
                peso = Convert.ToInt32(pesoCubado);
            }
            else
            {
                peso = Convert.ToInt32(pesoMerc);
            }
            double taxaICMS = (100 - Convert.ToDouble(porcentagemICMS)) / 100;
            int freteSobrePeso = peso * fretePeso;
            int freteSobreValor = Convert.ToInt32(txtValorMerc.Text) * freteValor;
            int pedagio = (3 * peso) / 100; // 5 não é o valor certo           

            double subTotal = (taxaDeCTRC + taxaDeTDE + freteSobrePeso + freteSobreValor + pedagio)/100;

            double valorTotalDoFrete = Convert.ToDouble(subTotal) * taxaICMS;

            txtSubTotal.Text = Convert.ToString(subTotal);
            txtTotal.Text = Convert.ToString(valorTotalDoFrete);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            int pesoMerc = Convert.ToInt32(txtPesoMerc.Text);
            int pesoCubado = Convert.ToInt32(txtPesoCubado.Text);
            int valor = Convert.ToInt32(txtValorMerc.Text);
            int fretePeso = Convert.ToInt32(FretePesoQuilo.Text);
            String porcentagemICMS = "";
            String origem = txtEstadoOrigem.Text;
            String destino = txtEstadoDestino.Text;

            _query = "Select " + destino + " FROM Frete WHERE Estados like '" + origem + "%'";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_Alunos = _dataCommand.ExecuteReader();
            
            
            if (dr_Alunos.Read())
            {
                porcentagemICMS = dr_Alunos.GetString(0);
                //MessageBox.Show(Convert.ToString(porcentagemICMS));
                Calculo(Convert.ToInt16(porcentagemICMS), pesoMerc, pesoCubado, valor, fretePeso);
            }
            else
            {
                MessageBox.Show("Moio" + origem + destino);
            }            
        }

        

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Form1();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
        

        
    }
}
