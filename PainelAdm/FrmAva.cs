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
    public partial class FrmAva : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        public FrmAva()
        {
            InitializeComponent();
        }

        private void FrmAva_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                label1.Text = Convert.ToString(e.KeyValue);
                if (e.KeyValue == 68)
                {
                    label2.Text = "Avaliação Recebida!";

                    con.Abrircon();
                    sql = "INSERT INTO avaliacao (usuario, cargo, nota, data) VALUES (@usuario, @cargo, @nota, curDate())";
                    cmd = new MySqlCommand(sql, con.con);

                    cmd.Parameters.AddWithValue("@usuario", Program.nomeUsuario);
                    cmd.Parameters.AddWithValue("@cargo", Program.cargoUsuario);
                    cmd.Parameters.AddWithValue("@nota", "Ótimo");

                    cmd.ExecuteNonQuery();
                    con.Fecharcon();

                    Close();
                }

                else if (e.KeyValue == 67)
                {
                    label2.Text = "Avaliação Recebida!";

                    con.Abrircon();
                    sql = "INSERT INTO avaliacao (usuario, cargo, nota, data) VALUES (@usuario, @cargo, @nota, curDate())";
                    cmd = new MySqlCommand(sql, con.con);

                    cmd.Parameters.AddWithValue("@usuario", Program.nomeUsuario);
                    cmd.Parameters.AddWithValue("@cargo", Program.cargoUsuario);
                    cmd.Parameters.AddWithValue("@nota", "Bom");

                    cmd.ExecuteNonQuery();
                    con.Fecharcon();

                    Close();
                }

                else if (e.KeyValue == 66)
                {
                    label2.Text = "Avaliação Recebida!";

                    con.Abrircon();
                    sql = "INSERT INTO avaliacao (usuario, cargo, nota, data) VALUES (@usuario, @cargo, @nota, curDate())";
                    cmd = new MySqlCommand(sql, con.con);

                    cmd.Parameters.AddWithValue("@usuario", Program.nomeUsuario);
                    cmd.Parameters.AddWithValue("@cargo", Program.cargoUsuario);
                    cmd.Parameters.AddWithValue("@nota", "Regular");

                    cmd.ExecuteNonQuery();
                    con.Fecharcon();

                    Close();
                }

                else if (e.KeyValue == 65)
                {
                    label2.Text = "Avaliação Recebida!";

                    con.Abrircon();
                    sql = "INSERT INTO avaliacao (usuario, cargo, nota, data) VALUES (@usuario, @cargo, @nota, curDate())";
                    cmd = new MySqlCommand(sql, con.con);

                    cmd.Parameters.AddWithValue("@usuario", Program.nomeUsuario);
                    cmd.Parameters.AddWithValue("@cargo", Program.cargoUsuario);
                    cmd.Parameters.AddWithValue("@nota", "Péssimo");

                    cmd.ExecuteNonQuery();
                    con.Fecharcon();

                    Close();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Avaliação não enviada, Verifique sua conexão e tente novamente!", "Erro ao enviar avaliação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void FrmAva_Load(object sender, EventArgs e)
        {
            try
            {
                lblUsuario.Text = Program.nomeUsuario;
                lblCargo.Text = Program.cargoUsuario;
            }

            catch (Exception)
            {
                MessageBox.Show("Erro ao Identificar Atendente,\nVerifique sua conexão e tente novamente!", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
