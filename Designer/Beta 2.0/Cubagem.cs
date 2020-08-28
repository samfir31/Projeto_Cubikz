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
    public partial class Cubagem : Form
    {
        public Cubagem()
        {
            InitializeComponent();
        }

        private void Calcular_Click(object sender, EventArgs e)
        {
            if (txtAltura.Text == "")
            {

            }
            else if (txtLargura.Text == "")
            {

            }
            else if (txtComprimento.Text == "")
            {

            }
            else
            {
                int alt = Convert.ToInt32(txtAltura.Text);
                int lar = Convert.ToInt32(txtLargura.Text);
                int com = Convert.ToInt32(txtComprimento.Text);
                
                double resu = Convert.ToDouble(alt * lar * com * 300);
                if ((alt * lar * com > 1000000))
                {
                    MessageBox.Show("Valores muito altos");
                }
                else
                {
                    Resultado.Text = Convert.ToString(resu);
                }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();                     
            BancoProduto tela = new BancoProduto();
            tela.quem = "Cubagem";
            tela.Closed += (s, args) => this.Close();
            tela.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CadastroProd tela = new CadastroProd();
            tela.Show();
        }

        public void alterar_valores (String nome, String altura, String largura, String comprimento){
            txtNome.Text = nome;
            txtAltura.Text = altura;
            txtLargura.Text = largura;
            txtComprimento.Text = comprimento;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

    
        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            if (txtAltura.Text == "")
            {

            }
            else if (txtLargura.Text == "")
            {

            }
            else if (txtComprimento.Text == "")
            {

            }
            else
            {
                double alt = Convert.ToDouble(txtAltura.Text)/100;
                double lar = Convert.ToDouble(txtLargura.Text)/100;
                double com = Convert.ToDouble(txtComprimento.Text)/100;

                double resu = Convert.ToDouble(alt * lar * com * 300);
                if ((alt * lar * com > 1000000))
                {
                    MessageBox.Show("Valores muito altos");
                }
                else
                {
                    Resultado.Text = Convert.ToString(resu);
                }
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    

    }
}
