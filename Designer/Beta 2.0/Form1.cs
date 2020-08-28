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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
            
                
      
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            SelecionarProduto a = new SelecionarProduto();
            a.Show();
            a.Closed += (s, args) => this.Close();
            a.Show();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Calcular_frete f = new Calcular_frete();
            f.Closed += (s, args) => this.Close();
            f.Show();
                        
            }
          
        private void btnCadCam_Click(object sender, EventArgs e)
        {
           
            CadastroCam cad = new CadastroCam();
            
            cad.Show();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            
            CadastroProd cad = new CadastroProd();
            
            cad.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Selecionar_caminhao a = new Selecionar_caminhao();
            a.Closed += (s, args) => this.Close();
            a.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Cubagem tela = new Cubagem();
            tela.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Combustivel frete = new Combustivel();
            frete.Show();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

      

        //private void button1_Click_1(object sender, EventArgs e)
      
    }
}
