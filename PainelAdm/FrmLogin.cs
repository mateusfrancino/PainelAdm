using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PainelAdm
{
    public partial class FrmLogin : Form
    {
        Conexao con = new Conexao();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }


        
        private void ChamarLogin()
        {
            if (txtusuario.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o Usuário", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtusuario.Text = "";
                txtusuario.Focus();
                return;
            }

            if (txtsenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha a Senha", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsenha.Text = "";
                txtsenha.Focus();
                return;
            }

            // AQUI INICIA O CÓDIGO PARA O LOGIN

            MySqlCommand cmdVerificar;
            MySqlDataReader reader;


            con.Abrircon();
            cmdVerificar = new MySqlCommand("SELECT * FROM usuarios where usuario = @usuario and senha = @senha", con.con);
            cmdVerificar.Parameters.AddWithValue("@usuario", txtusuario.Text);
            cmdVerificar.Parameters.AddWithValue("@senha", txtsenha.Text);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                //EXTRAINDO INFORMAÇÕES DO LOGIN
                while (reader.Read())
                {
                    Program.nomeUsuario = Convert.ToString(reader["nome"]);
                    Program.cargoUsuario = Convert.ToString(reader["funcao"]);

                }

                FrmMenu form = new FrmMenu();
                this.Hide();
                form.Show();
                txtusuario.Text = "";
            }

            else
            {
                MessageBox.Show("Poxa você errou seu usuário ou sua senha, Vamos tentar novamente?", "Dados Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusuario.Text = "";
                txtsenha.Text = "";
                txtusuario.Focus();
            }

            con.Fecharcon();


        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChamarLogin();
            }
        }

        private void txtsenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChamarLogin();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtusuario.Text = "";
            txtusuario.Focus();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                ChamarLogin();

            }

            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
