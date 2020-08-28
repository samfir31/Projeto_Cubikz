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
    public partial class Alocacao : Form
    {
        //txtNome.Text, txtLarg.Text, txtAlt.Text, txtComp.Text, txtLimite.Text
        public String nomeCaminhao;
        public double larguraCaminhao = 40;
        public double comprimentoCaminhao = 40;
        public double alturaCaminhao = 40;
        public double limiteCaminhao;

        OleDbConnection conn = Conexao.obterConexao();

        OleDbDataReader dr_produto;

        BindingSource bs_produto = new BindingSource();

        String _query;
        
            
        int cont = 1;       
        
        public Alocacao()
        {
            InitializeComponent();
        }

        private void Alocacao_Load(object sender, EventArgs e)
        {
            MessageBox.Show(nomeCaminhao);
            carregar_grid();
        }
        private void carregar_grid()
        {
            _query = "Select * from Produto";

            OleDbCommand _dataCommand = new OleDbCommand(_query, conn);

            dr_produto = _dataCommand.ExecuteReader();

            if (dr_produto.HasRows == true)
            {
                bs_produto.DataSource = dr_produto;
                dgvBanco.DataSource = bs_produto;
            }
        }
        
    

        private void I_Click(Object sender, EventArgs e)
        {
            
        }

        private void Escolher_Click(object sender, EventArgs e)
        {
            
        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            
            Nome.DataBindings.Add("Text", bs_produto, "Nome_produto");
            Nome.DataBindings.Clear();
            Xis.DataBindings.Add("Text", bs_produto, "Largura");
            Xis.DataBindings.Clear();
            Ypsolon.DataBindings.Add("Text", bs_produto, "Comprimento");
            Ypsolon.DataBindings.Clear();
            Ze.DataBindings.Add("Text", bs_produto, "Altura");
            Ze.DataBindings.Clear();

            double volumeObjeto = Convert.ToDouble(Xis.Text) * Convert.ToDouble(Ypsolon.Text) * Convert.ToDouble(Ze.Text);
            double volumeCaminhao = alturaCaminhao * comprimentoCaminhao * larguraCaminhao;
            double volumeOcupado = (volumeObjeto / volumeCaminhao) * 100 * Convert.ToDouble(Quantidade.Value);

            Porcentagem.Text = Convert.ToString(Convert.ToInt32(volumeOcupado))+"%";

            MessageBox.Show(Convert.ToString(volumeObjeto));
            MessageBox.Show(Convert.ToString(volumeCaminhao));
            MessageBox.Show(Convert.ToString(volumeOcupado));

            //Porcentagem.Text = Convert.ToString(Convert.ToInt16(volumeOcupado)) + "%";
            

        }

        private void Cadastrar_Click(object sender, EventArgs e)
        {
            CadastroProd tela = new CadastroProd();
            tela.receber_valores(Nome.Text, Xis.Text, Ypsolon.Text, Ze.Text);
            tela.Show();
        }

    
        private void Porcentagem_TextChanged(object sender, EventArgs e)
        {

        }

        private void Excluir_Click(object sender, EventArgs e)
        {

        }

        private void Quantidade_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Ze_TextChanged(object sender, EventArgs e)
        {

        }

        private void Ypsolon_TextChanged(object sender, EventArgs e)
        {

        }

        private void Xis_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Nome_TextChanged(object sender, EventArgs e)
        {

        }

        private void Produto_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            cont++;

            if (groupBox1.Visible == true) {
                groupBox2.Visible = true;
                 }
            if (groupBox2.Visible == true)
            {
                groupBox3.Visible = true;
            }                       
            
        }

        

        
    }
}
