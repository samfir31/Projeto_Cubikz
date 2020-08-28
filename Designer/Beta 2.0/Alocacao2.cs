using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;

namespace Beta_2._0
{
    public partial class Alocacao2 : Form
    {
        //txtNome.Text, txtLarg.Text, txtAlt.Text, txtComp.Text, txtLimite.Text
        public String nomeCaminhao;
        public double larguraCaminhao = 40;
        public double comprimentoCaminhao = 40;
        public double alturaCaminhao = 40;
        public double limiteCaminhao = 0;
        public String tipoCaminhao;
        double volumeCaminha;

        Point pontoLabel = new Point(10, 80);
        Point pontoAltura = new Point(96, 80);
        Point pontoLargura = new Point(152, 80);
        Point pontoComprimento = new Point(208, 80);
        Point pontoPeso = new Point(264, 80);
        Point pontoAlterar = new Point(320, 79);
        Point pontoQuantidade = new Point(415, 80);
        Point pontoNome = new Point(40, 80);
        Point pontoPorcentagem = new Point(470, 80);
        Point pontoPesoTotal = new Point(575, 80);

        Size tamanhoTxt = new Size(50, 20);
        Size tamanhoBotao = new Size(90, 23);

        int[] vetorAltura = new int[100];
        int[] vetorLargura = new int[100];
        int[] vetorComprimento = new int[100];
        int[] vetorPeso = new int[100];
        int[] vetorPorcentagem = new int[100];

                

        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_alocacao;

        BindingSource bs_alocacao = new BindingSource();

        String _query;

        Label novoLabel = new Label();

        int altura;
        int largura;
        int comprimento;
        int volume;
        double porcentagem;
        double porcentagemVezesQuantidade;

        int pag = 1;
        int registro = 0;
        int linha = 0;
        int fim = 0;

        int cont = 0;


        public Alocacao2()
        {
            InitializeComponent();
        }

        private void Alocacao2_Load(object sender, EventArgs e)
        {
            zerar_registros();
            criar_registro();
            carregar_grid();
            CirculoPorcentagemVolume.Value = 0;
            CirculoPorcentagemVolume.MaxValue = 100;
            this.Location = new Point(140,20);
        }
        private void carregar_grid()
        {
            _query = "Select  Nome_Produto, Largura, Altura, Comprimento, Peso  from Produto";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_alocacao = _dataCommand.ExecuteReader();

            if (dr_alocacao.HasRows == true)
            {
                bs_alocacao.DataSource = dr_alocacao;
                dataGridView1.DataSource = bs_alocacao;
            }
        }

        public void zerar_registros()
        {
            _query = "DELETE * FROM Tabela";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
            _dataCommand.ExecuteNonQuery();
        }

        public void criar_registro()
        {

            _query = "INSERT INTO Tabela (Altura, Largura, Comprimento, Peso, Numero) VALUES (0, 0, 0, 0," + cont + ");";
            try
            {
                OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                _dataCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro!");
            }
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            cont++;

            NumericUpDown novoQuantidade = new NumericUpDown();
            novoQuantidade.Location = new Point(pontoQuantidade.X, pontoQuantidade.Y + (28 * (cont - 1)));
            novoQuantidade.Size = tamanhoTxt;
            novoQuantidade.Maximum = 400;

            Label novoLabel = new Label();
            novoLabel.Location = new Point(pontoLabel.X, pontoLabel.Y + (28 * (cont - 1)));
            novoLabel.ForeColor = System.Drawing.SystemColors.Control;
            novoLabel.Size = new Size(20, 13);
            novoLabel.Text = Convert.ToString(cont);

            TextBox novoAltura = new TextBox();
            novoAltura.Location = new Point(pontoAltura.X, pontoAltura.Y + (28 * (cont - 1)));
            novoAltura.Size = tamanhoTxt;

            TextBox novoNome = new TextBox();
            novoNome.Location = new Point(pontoNome.X, pontoNome.Y + (28 * (cont - 1)));
            novoNome.Size = tamanhoTxt;

            TextBox novoLargura = new TextBox();
            novoLargura.Location = new Point(pontoLargura.X, pontoLargura.Y + (28 * (cont - 1)));
            novoLargura.Size = tamanhoTxt;

            TextBox novoComprimento = new TextBox();
            novoComprimento.Location = new Point(pontoComprimento.X, pontoComprimento.Y + (28 * (cont - 1)));
            novoComprimento.Size = tamanhoTxt;

            TextBox novoPeso = new TextBox();
            novoPeso.Location = new Point(pontoPeso.X, pontoPeso.Y + (28 * (cont - 1)));
            novoPeso.Size = tamanhoTxt;

            TextBox novoPorcentagem = new TextBox();
            novoPorcentagem.MaxLength = 4;
            novoPorcentagem.Enabled = false;
            novoPorcentagem.Location = new Point(pontoPorcentagem.X + 10, pontoPorcentagem.Y + (28 * (cont - 1)));
            novoPorcentagem.Size = new Size(tamanhoTxt.Width - 11, tamanhoTxt.Height);

            Button novoAlterar = new Button();
            novoAlterar.Location = new Point(pontoAlterar.X, pontoAlterar.Y + (28 * (cont - 1)));
            novoAlterar.Size = tamanhoBotao;
            novoAlterar.Text = "Alterar";
            novoAlterar.BackColor = Color.White;

            TextBox novoPesoTotal = new TextBox();
            novoPesoTotal.Enabled = false;
            novoPesoTotal.Location = new Point(pontoPesoTotal.X - 40, pontoPesoTotal.Y + (28 * (cont - 1)));
            novoPesoTotal.Size = new Size(tamanhoTxt.Width + 10, tamanhoTxt.Height);

            novoAlterar.Click += delegate
            {
                novoLargura.DataBindings.Add("Text", bs_alocacao, "Largura");
                novoLargura.DataBindings.Clear();
                novoComprimento.DataBindings.Add("Text", bs_alocacao, "Comprimento");
                novoComprimento.DataBindings.Clear();
                novoPeso.DataBindings.Add("Text", bs_alocacao, "Peso");
                novoPeso.DataBindings.Clear();
                novoAltura.DataBindings.Add("Text", bs_alocacao, "Altura");
                novoAltura.DataBindings.Clear();
                novoNome.DataBindings.Add("Text", bs_alocacao, "Nome_Produto");
                novoNome.DataBindings.Clear();

                novoPesoTotal.Text = Convert.ToString(Convert.ToInt32(novoPeso.Text)*novoQuantidade.Value);

                altura = Convert.ToInt16(novoAltura.Text);
                largura = Convert.ToInt16(novoLargura.Text);
                comprimento = Convert.ToInt16(novoComprimento.Text);

                volume = altura * largura * comprimento;

                volumeCaminha = alturaCaminhao * larguraCaminhao * comprimentoCaminhao;

                porcentagem = ((100 * volume) / volumeCaminha);

                porcentagemVezesQuantidade = porcentagem * Convert.ToInt16(novoQuantidade.Value);

                novoPorcentagem.Text = Convert.ToString(porcentagemVezesQuantidade);


                _query = "Update Tabela set Porcentagem = '" + novoPorcentagem.Text + "' where Numero like '" + novoLabel.Text + "'";
                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    MessageBox.Show("Problemas com a Alteração  !!!!", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (porcentagem + CirculoPorcentagemVolume.Value > 100)
                {
                    MessageBox.Show("Você ultrapassou o volume máximo do caminhão. Por favor reduza a quantidade de produtos.");

                }
                else if (porcentagem + CirculoPorcentagemVolume.Value == 100)
                {
                    MessageBox.Show("Atenção! O volume máximo do caminhão foi atingido.");
                    calculo_porcentagem_volume();
                }
                else
                {
                    calculo_porcentagem_volume();
                }


            };

            this.Controls.Add(novoLabel);
            this.Controls.Add(novoAltura);
            this.Controls.Add(novoLargura);
            this.Controls.Add(novoComprimento);
            this.Controls.Add(novoPeso);
            this.Controls.Add(novoAlterar);
            this.Controls.Add(novoQuantidade);
            this.Controls.Add(novoNome);
            this.Controls.Add(novoPorcentagem);
            this.Controls.Add(novoPesoTotal);


            if (novoAltura.Text == "" || novoLargura.Text == "" || novoComprimento.Text == "" || novoPeso.Text == "")
            {
                Calcular.Enabled = false;
                Adicionar.Enabled = false;
            }
            else
            {
                Calcular.Enabled = true;
                Adicionar.Enabled = true;
            }

            

            novoAltura.TextChanged += (s, args) =>
            {
                if (novoAltura.Text == "" || novoLargura.Text == "" || novoComprimento.Text == "" || novoPeso.Text == "")
                {
                    Calcular.Enabled = false;
                    Adicionar.Enabled = false;
                }
                else
                {
                    Calcular.Enabled = true;
                    Adicionar.Enabled = true;
                }

                _query = "Update Tabela set Altura = '" + novoAltura.Text + "' where Numero like '" + novoLabel.Text + "'";

                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro!");
                }
            };

            novoLargura.TextChanged += (s, args) =>
            {
                if (novoAltura.Text == "" || novoLargura.Text == "" || novoComprimento.Text == "" || novoPeso.Text == "")
                {
                    Calcular.Enabled = false;
                    Adicionar.Enabled = false;
                }
                else
                {
                    Calcular.Enabled = true;
                    Adicionar.Enabled = true;
                }

                _query = "Update Tabela set Largura = '" + novoLargura.Text + "' where Numero like '" + novoLabel.Text + "'";

                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro!");
                }
            };

            novoComprimento.TextChanged += (s, args) =>
            {
                if (novoAltura.Text == "" || novoLargura.Text == "" || novoComprimento.Text == "" || novoPeso.Text == "")
                {
                    Calcular.Enabled = false;
                    Adicionar.Enabled = false;
                }
                else
                {
                    Calcular.Enabled = true;
                    Adicionar.Enabled = true;
                }

                _query = "Update Tabela set Comprimento = '" + novoComprimento.Text + "' where Numero like '" + novoLabel.Text + "'";

                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro!");
                }
            };
            novoNome.TextChanged += (s, args) =>
            {
                if (novoAltura.Text == "" || novoLargura.Text == "" || novoComprimento.Text == "" || novoPeso.Text == "")
                {
                    Calcular.Enabled = false;
                    Adicionar.Enabled = false;
                }
                else
                {
                    Calcular.Enabled = true;
                    Adicionar.Enabled = true;
                }

                _query = "Update Tabela set Nome = '" + novoNome.Text + "' where Numero like '" + novoLabel.Text + "'";

                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro!");
                }
            };
            novoQuantidade.TextChanged += (s, args) =>
            {
                _query = "Update Tabela set Quantidade = '" + novoQuantidade.Value + "' where Numero like '" + novoLabel.Text + "'";
                OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                _dataCommand.ExecuteReader();

                if (novoAltura.Text == "" || novoLargura.Text == "" || novoComprimento.Text == "" || novoPeso.Text == "")
                {
                    Calcular.Enabled = false;
                    Adicionar.Enabled = false;


                }
                else
                {
                    if (CirculoPorcentagemVolume.Value > 99)
                    {
                        MessageBox.Show("Volume máximo atingido!");
                        CirculoPorcentagemVolume.Value = 99;
                        novoQuantidade.Value = novoQuantidade.Value - 1;
                    }
                    else if (CirculoPorcentagemPeso.Value > 99)
                    {
                        MessageBox.Show("Peso máximo atingido!");
                        CirculoPorcentagemPeso.Value = 99;
                        novoQuantidade.Value = novoQuantidade.Value - 1;
                    }
                    else
                    {
                        
                        Calcular.Enabled = true;
                        Adicionar.Enabled = true;

                        novoPesoTotal.Text = Convert.ToString(Convert.ToInt32(novoPeso.Text) * novoQuantidade.Value);

                        altura = Convert.ToInt16(novoAltura.Text);
                        largura = Convert.ToInt16(novoLargura.Text);
                        comprimento = Convert.ToInt16(novoComprimento.Text);
                        

                        volume = altura * largura * comprimento;

                        volumeCaminha = alturaCaminhao * larguraCaminhao * comprimentoCaminhao;

                        porcentagem = ((100 * volume) / volumeCaminha);

                        porcentagemVezesQuantidade = porcentagem * Convert.ToInt16(novoQuantidade.Value);

                        novoPorcentagem.Text = Convert.ToString(porcentagemVezesQuantidade);

                        

                        _query = "Update Tabela set Porcentagem = '" + novoPorcentagem.Text + "' where Numero like '" + novoLabel.Text + "'";
                        OleDbCommand _dataCommand2 = new OleDbCommand(_query, conn);
                        
                        try
                        {
                            _dataCommand2.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ocorreu um erro!");
                        }

                        calculo_porcentagem_volume();
                        calculo_porcentagem_peso();
                        
                    }
                }


            };

            novoPeso.TextChanged += (s, args) =>
            {
                if (novoAltura.Text == "" || novoLargura.Text == "" || novoComprimento.Text == "" || novoPeso.Text == "")
                {
                    Calcular.Enabled = false;
                    Adicionar.Enabled = false;
                }
                else
                {
                    Calcular.Enabled = true;
                    Adicionar.Enabled = true;
                }

                _query = "Update Tabela set Peso = '" + novoPeso.Text + "' where Numero like '" + novoLabel.Text + "'";

                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro!");
                }

            };

            novoPesoTotal.TextChanged += (s, args) =>
            {
                _query = "Update Tabela set PesoTotal = '" + novoPesoTotal.Text + "' where Numero like '" + novoLabel.Text + "'";

                try
                {
                    OleDbCommand _dataCommand = new OleDbCommand(_query, conn);
                    _dataCommand.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro!");
                }

            };

            criar_registro();
        }

        private void Alocacao2_FormClosing(object sender, FormClosingEventArgs e)
        {
            zerar_registros();
        }

        private void calculo_porcentagem_peso()
        {
            int porcentagemPeso = 0;
            

            OleDbCommand _dataCommand;
            for (int i = 1; i <= cont; i++)
            {
                _query = "Select PesoTotal FROM Tabela WHERE Numero like '"+i+"%'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_alocacao = _dataCommand.ExecuteReader();


                if (dr_alocacao.Read())
                {
                    vetorPeso[i] = Convert.ToInt32(dr_alocacao.GetString(0));
                    

                }
            }

            for (int i = 0; i < vetorPorcentagem.Length; i++)
            {
                porcentagemPeso = porcentagemPeso + vetorPeso[i];
            }

            int porcentagemOcupada = Convert.ToInt16((Convert.ToDouble(porcentagemPeso * 100)) / limiteCaminhao);
            CirculoPorcentagemPeso.Value = porcentagemOcupada;
            /////////////////////////////////////////////////////////////////////////////////
        }

        private void calculo_porcentagem_volume()
        {
            int porcentagemTotal = 0;
            OleDbCommand _dataCommand;

            for (int i = 1; i <= cont; i++)
            {


                //===========================================

                _query = "Select Porcentagem FROM Tabela WHERE Numero like '" + i + "%'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_alocacao = _dataCommand.ExecuteReader();


                if (dr_alocacao.Read())
                {
                    vetorPorcentagem[i - 1] = Convert.ToInt16(dr_alocacao.GetInt32(0));


                }

            }

            for (int i = 0; i < vetorPorcentagem.Length; i++)
            {
                porcentagemTotal = porcentagemTotal + vetorPorcentagem[i];
            }

            CirculoPorcentagemVolume.Value = porcentagemTotal;
        }


        private void Calcular_Click(object sender, EventArgs e)
        {
            OleDbCommand _dataCommand;


            for (int i = 1; i <= cont; i++)
            {


                //===========================================

                _query = "Select Altura FROM Tabela WHERE Numero like '" + i + "%'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_alocacao = _dataCommand.ExecuteReader();


                if (dr_alocacao.Read())
                {
                    vetorAltura[i - 1] = Convert.ToInt16(dr_alocacao.GetInt32(0));


                }

                //=============================================



                _query = "Select Largura FROM Tabela WHERE Numero like '" + i + "%'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_alocacao = _dataCommand.ExecuteReader();

                if (dr_alocacao.Read())
                {
                    vetorLargura[i - 1] = Convert.ToInt16(dr_alocacao.GetInt32(0));


                }
                else
                {
                    MessageBox.Show("Moio");
                }



                //=============================================


                _query = "Select Comprimento FROM Tabela WHERE Numero like '" + i + "%'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_alocacao = _dataCommand.ExecuteReader();

                if (dr_alocacao.Read())
                {
                    vetorComprimento[i - 1] = Convert.ToInt16(dr_alocacao.GetInt32(0));


                }
                else
                {
                    MessageBox.Show("Ocorreu um erro!");
                }


                //=============================================


                _query = "Select Peso FROM Tabela WHERE Numero like '" + i + "%'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_alocacao = _dataCommand.ExecuteReader();

                if (dr_alocacao.Read())
                {
                    vetorPeso[i - 1] = Convert.ToInt32(dr_alocacao.GetInt32(0));


                }
                else
                {
                    MessageBox.Show("Ocorreu um erro!");
                }
                
            }
            calcular();

        }
        private void calcular()
        {
            OleDbCommand _dataCommand;

            //for para adicionar os valores no campo Densidade do BD
            for (int i = 0; i < cont; i++)
            {
                double volume = vetorAltura[i] * vetorLargura[i] * vetorComprimento[i];

                ////////////////////////////////////////////////////////////
                float densidade = (float)((vetorPeso[i] / volume));
                _query = "Update Tabela set Densidade = '" + densidade + "' where Numero like '" + (i + 1) + "'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_alocacao = _dataCommand.ExecuteReader();
            }

            armazenar_valores();
            /*for (int i = 0; i <= cont - 1; i++)
            {
                double s;
                
                vetorVolumeObjeto[i] = Convert.ToDouble(vetorAltura[i] * vetorAltura[i] * vetorComprimento[i]);
                MessageBox.Show("Volume do produto " + Convert.ToString(i + 1) + ": " + Convert.ToString(vetorVolumeObjeto[i]));
                volumeCaminha = Convert.ToDouble(alturaCaminhao * comprimentoCaminhao * larguraCaminhao);
                MessageBox.Show("Volume do caminhão é " + volumeCaminha);
                vetorVolumeOcupado[i] = (((100 * vetorVolumeObjeto[i]) / volumeCaminha) * vetorQuantidade[i]);
                s = vetorVolumeOcupado[i];
                MessageBox.Show("Volume Ocupado é " + Convert.ToString(i + 1) + ": " + vetorVolumeOcupado[i]);
                vetorDensidade[i] = Convert.ToDouble((vetorAltura[i] * vetorLargura[i] * vetorComprimento[i]) / vetorPeso[i]);
                MessageBox.Show("Densidade do produto " + Convert.ToString(i + 1) + ": " + Convert.ToString(vetorDensidade[i]));
                for (int VolumeOcupado = 1; VolumeOcupado <= 100; VolumeOcupado++)
                {
                    Thread.Sleep(5);
                    CirculoPorcentagem.Value = VolumeOcupado;
                    CirculoPorcentagem.Update();
                };*/

            /*int menor = 0, maior = 0, posicao_menor = 0, posicao_maior = 0;
            for (int t = 0; t < vetorDensidade[i]; t++) {
                MessageBox.Show((i + 1) + "º número:");
                vetorDensidade[i] = Convert.ToInt32(Console.ReadLine());
                if( i == 0 )
{
    menor = Convert.ToInt32(vetorDensidade[0]);
    maior = Convert.ToInt32(vetorDensidade[0]);
}

if( vetorDensidade[i] < menor )
{

    menor = Convert.ToInt32(vetorDensidade[i]);
    posicao_menor = i;

}
else if( vetorDensidade[i] > maior )
{
    maior = Convert.ToInt32(vetorDensidade[i]);
    posicao_maior = i;
}

}

MessageBox.Show( "O menor número é:" + menor );
MessageBox.Show("A posição do menor número é:" + posicao_menor);
MessageBox.Show("O maior número é:" + maior);
MessageBox.Show("A posição do maior número é:" + posicao_maior);

Console.Read();*/


            /*for (int t = 0; t < vetorDensidade[i]; t++)
        {
                
            if (vetorDensidade[t] > s)
                s= vetorDensidade[t];
            MessageBox.Show("ss" + s);

        }

    }
        Array.Sort(vetorVolumeObjeto);
        Array.Sort(vetorDensidade);
        Array.Sort(vetorVolumeOcupado);
    }
        */


            //double volumeOcupado = (volumeObjeto / volumeCaminhao) * 100 * Convert.ToDouble(Quantidade.Value);


        }

        private void armazenar_valores()
        {
            _query = "Delete * from Tabela where Peso like 0";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_alocacao = _dataCommand.ExecuteReader();

            

            _query = "Select Nome, Peso, Altura, Largura, Comprimento, Quantidade, Densidade, Porcentagem, PesoTotal from Tabela order by Densidade";

            OleDbCommand _dataCommand2 = new OleDbCommand(_query, conn);

            dr_alocacao = _dataCommand2.ExecuteReader();

            if (dr_alocacao.HasRows == true)
            {
                bs_alocacao.DataSource = dr_alocacao;
                dataGridView1.DataSource = bs_alocacao;
            }
            else
            {
                MessageBox.Show("Ocorreu um erro!");
            }
            

            printPreviewDialog1.Text = " Visualizando a impressão";
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printPreviewDialog1.PrintPreviewControl.Columns = 2;
            printPreviewDialog1.PrintPreviewControl.Zoom = 0.6;
            printPreviewDialog1.ShowDialog();

            /*OleDbCommand _dataCommand;
            for (int i = 0; i < cont; i++)
            {
                _query = "Select Densidade FROM Tabela WHERE Numero like '" + (i+1) + "%'";

                _dataCommand = new OleDbCommand(_query, conn);

                dr_produto = _dataCommand.ExecuteReader();

                if (dr_produto.Read())
                {

                    vetorDensidade[i] = Convert.ToString(dr_produto.GetString(0));
                    MessageBox.Show(vetorDensidade[i]);

                }
                else
                {
                    MessageBox.Show("Moio");
                }
            }*/

            /*_query = "Select " + destino + " FROM Frete WHERE Estados like '" + origem + "%'";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_Alunos = _dataCommand.ExecuteReader();


            if (dr_Alunos.Read())
            {
                porcentagemICMS = dr_Alunos.GetString(0);
                MessageBox.Show(Convert.ToString(porcentagemICMS));
                Calculo(Convert.ToInt16(porcentagemICMS), pesoMerc, pesoCubado, valor, fretePeso);
            }
            else
            {
                MessageBox.Show("Moio" + origem + destino);
            }   */
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Selecionar_caminhao f = new Selecionar_caminhao();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            String imagem = "";
            bs_alocacao.MoveLast();
            DataGridViewRow reg_grid;
            reg_grid = dataGridView1.CurrentRow;
            
            /*Veículo Urbano de Carga
Toco ou Semi-Pesado
Truck ou Pesado
Cavalo Mecânico ou Extra-Pesado
Cavalo Mecânico Trucado ou LS
Carreta 2 Eixos
Carreta 3 Eixos
Carreta ou Cavalo Trucado*/
            if (tipoCaminhao == "Veículo Urbano de Carga")
            {
                imagem = "Um.JPG";
            }
            else if (tipoCaminhao == "Toco ou Semi-Pesado")
            {
                imagem = "Dois.JPG";
            }
            else if (tipoCaminhao == "Truck ou Pesado")
            {
                imagem = "Tres.JPG";
            }
            else if (tipoCaminhao == "Cavalo Mecânico ou Extra-Pesado")
            {
                imagem = "Quatro.JPG";
            }
            else if (tipoCaminhao == "Cavalo Mecânico Trucado ou LS")
            {
                imagem = "Cinco.JPG";
            }
            else if (tipoCaminhao == "Carreta 2 Eixos")
            {
                imagem = "Seis.JPG";
            }
            else if (tipoCaminhao == "Carreta 3 Eixos")
            {
                imagem = "Sete.JPG";
            }
            else if (tipoCaminhao == "Carreta ou Cavalo Trucado")
            {
                imagem = "Oito.JPG";
            }

            //e.Graphics.DrawImage(Image.FromFile("logo_ete.JPG"), 50, 25);

            e.Graphics.DrawString("ALOCAÇÃO PARA VIAGENS", new System.Drawing.Font("Arial", 20, FontStyle.Bold), Brushes.Black, 230, 40);


            linha = 120;

            e.Graphics.DrawString("Caminhão", new System.Drawing.Font("Arial", 20, FontStyle.Bold), Brushes.Black, 50, linha);

            linha = 180;

            e.Graphics.DrawImage(Image.FromFile(imagem), 50, linha);

            linha = 250;
            e.Graphics.DrawString(tipoCaminhao, new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Gray, 50, linha);




            /*public String nomeCaminhao;
        public double larguraCaminhao = 40;
        public double comprimentoCaminhao = 40;
        public double alturaCaminhao = 40;
        public double limiteCaminhao = 0;
        public String tipoCaminhao;
        double volumeCaminha;*/


            linha = 290;


            e.Graphics.DrawString("Nome  ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 50, linha);
            e.Graphics.DrawString("Altura", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 160, linha);
            e.Graphics.DrawString("Largura", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 250, linha);
            e.Graphics.DrawString("Comprimento", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 350, linha);
            e.Graphics.DrawString("Limite de Peso", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 480, linha);
            e.Graphics.DrawString("Volume Disponível", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 600, linha);

            linha = 310;

            e.Graphics.DrawString("(cm)  ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 160, linha);
            e.Graphics.DrawString("(cm)", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 250, linha);
            e.Graphics.DrawString("(cm) ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 350, linha);
            e.Graphics.DrawString("(g)", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 480, linha);
            e.Graphics.DrawString("(cm³)", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 600, linha);


            linha = 340;

            e.Graphics.DrawString(nomeCaminhao, new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Gray, 50, linha);
            e.Graphics.DrawString(Convert.ToString(alturaCaminhao), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 160, linha);
            e.Graphics.DrawString(Convert.ToString(larguraCaminhao), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 250, linha);
            e.Graphics.DrawString(Convert.ToString(comprimentoCaminhao), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 350, linha);
            e.Graphics.DrawString(Convert.ToString(limiteCaminhao), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 480, linha);
            e.Graphics.DrawString(Convert.ToString(volumeCaminha), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 600, linha);
            
            ////

            linha = 420;

            e.Graphics.DrawString("Produtos", new System.Drawing.Font("Arial", 20, FontStyle.Bold), Brushes.Black, 50, linha);

            linha = 470;

            e.Graphics.DrawString("Os produtos abaixo estão organizados do mais denso para o menos denso. Para uma organização mais", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 50, linha);

            linha = 490;

            e.Graphics.DrawString("segura, recomenda-se que o peso seja distribuido próximo aos eixos do caminhão.", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 50, linha);


            linha = 530;


            e.Graphics.DrawString("Nome  ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 50, linha);
            e.Graphics.DrawString("Peso   ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 160, linha);
            e.Graphics.DrawString("Altura ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 230, linha);
            e.Graphics.DrawString("Largura  ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 300, linha);
            e.Graphics.DrawString("Comprimento   ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 370, linha);
            e.Graphics.DrawString("Quantidade ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 470, linha);
            e.Graphics.DrawString("Densidade", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 560, linha);
            e.Graphics.DrawString("Volume ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 640, linha);
            e.Graphics.DrawString("Peso Total ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 700, linha);

            linha = 550;

            e.Graphics.DrawString("(g) ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 160, linha);
            e.Graphics.DrawString("(cm)   ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 230, linha);
            e.Graphics.DrawString("(cm) ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 300, linha);
            e.Graphics.DrawString("(cm)  ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 370, linha);
            e.Graphics.DrawString("   ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 470, linha);
            e.Graphics.DrawString("(g/cm³)", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 560, linha);
            e.Graphics.DrawString("(%) ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 640, linha);
            e.Graphics.DrawString("(g) ", new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, 700, linha);


            linha = 580;

            e.Graphics.DrawLine(new Pen(Color.Orange, 2), 50, 75, 800, 75);

            //_query = "Select Nome, Peso, Altura, Largura, Comprimento, Quantidade, Densidade, Porcentagem from Tabela order by Densidade";

            //while ((linha < 1075) & (registro != fim))
            while ((linha < 1075) & (registro != cont))
            {
                
                e.Graphics.DrawString(reg_grid.Cells["Nome"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 50, linha);

                e.Graphics.DrawString(reg_grid.Cells["Peso"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 160, linha);

                e.Graphics.DrawString(reg_grid.Cells["Altura"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 230, linha);

                e.Graphics.DrawString(reg_grid.Cells["Largura"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 300, linha);

                e.Graphics.DrawString(reg_grid.Cells["Comprimento"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 370, linha);

                e.Graphics.DrawString(reg_grid.Cells["Quantidade"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 470, linha);

                e.Graphics.DrawString(reg_grid.Cells["Densidade"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 560, linha);

                e.Graphics.DrawString(reg_grid.Cells["Porcentagem"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 640, linha);

                e.Graphics.DrawString(reg_grid.Cells["PesoTotal"].Value.ToString(), new System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Gray, 700, linha);

                bs_alocacao.MovePrevious();

                reg_grid = dataGridView1.CurrentRow;

                registro += 1;

                linha += 20;

                e.Graphics.DrawString("Total de Registros: " + registro.ToString(), new System.Drawing.Font("Arial", 9, FontStyle.Bold), Brushes.Orange, 550, 1100);
                e.Graphics.DrawLine(new Pen(Color.Orange, 1), 50, 1115, 800, 1115);
                e.Graphics.DrawString("Data: " + System.DateTime.Now.ToString("dd/MM/yyyy"), new System.Drawing.Font("Arial", 9, FontStyle.Bold), Brushes.Orange, 50, 1120);
                e.Graphics.DrawString("Pág: " + pag.ToString(), new System.Drawing.Font("Arial", 9, FontStyle.Bold), Brushes.Orange, 550, 1120);

                pag += 1;


                if ((pag > 1) & (registro < fim))
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }

                
            }
            carregar_grid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        
    }
}




