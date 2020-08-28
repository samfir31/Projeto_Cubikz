using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Beta_2._0
{
    public partial class Combustivel : Form
    {
        public Combustivel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mapa tela = new Mapa();
            tela.Closed += (s, args) => this.Close();
            tela.Show();
        }

       
        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            double Qkm = Convert.ToDouble(txtQkm.Text);            
            double KmCaminhão = Convert.ToDouble(txtKmCaminhão.Text);
            double PCombustivel = Convert.ToDouble(txtPCombustivel.Text);
            
            double Combustivel = ((Qkm / KmCaminhão) * PCombustivel);


            MessageBox.Show("Seu Gasto sera: " + Convert.ToInt16(Combustivel) + " reais");
        }

        private void Combustivel_Load(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       

     }
}
