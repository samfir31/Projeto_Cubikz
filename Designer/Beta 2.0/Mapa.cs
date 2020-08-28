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
    public partial class Mapa : Form
    {
        public Mapa()
        {
            InitializeComponent();
        }

              
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Combustivel tela = new Combustivel();
            tela.Closed += (s, args) => this.Close();
            tela.Show();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            String Rua = txtRua.Text;
            String Cidade = txtCidade.Text;
            String Estado = txtEstado.Text;
            String Cep = txtCep.Text;
            try
            {
                StringBuilder queryendereço = new StringBuilder();

                queryendereço.Append("https://www.maps.google.com/maps?q=");

                if (Rua != string.Empty)
                {
                    queryendereço.Append(Rua + "," + "+");
                }
                if (Cidade != string.Empty)
                {
                    queryendereço.Append(Cidade + "," + "+");
                }
                if (Estado != string.Empty)
                {
                    queryendereço.Append(Estado + "," + "+");
                }
                if (Cep != string.Empty)
                {
                    queryendereço.Append(Cep + "," + "+");
                }
                webBrowser1.Navigate(queryendereço.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Erro");
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        }

         }

