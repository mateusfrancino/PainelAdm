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
    public partial class FrmMenu : Form
    {
        private Form _objForm;

        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        public FrmMenu()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Forms_Menu.FrmNovoUsuario
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();

        }

        private void VerificarNivelUsuario()
        {
            if (lblCargo.Text == "Gerente")
            {
                pictureBox4.Enabled = true;
                pictureBox5.Enabled = true;
                pictureBox10.Enabled = true;
                pictureBox12.Enabled = true;
                grid.Visible = true;
                picAcesso.Visible = false;

            }
            if (lblCargo.Text == "Administrador")
            {
                pictureBox4.Enabled = true;
                pictureBox5.Enabled = true;
                pictureBox10.Enabled = true;
                pictureBox12.Enabled = true;
                grid.Visible = true;
                picAcesso.Visible = false;
            }
            else
            {
                pictureBox4.Enabled = false;
                pictureBox5.Enabled = false;
                pictureBox10.Enabled = false;
                pictureBox12.Enabled = false;
                grid.Visible = false;
                picAcesso.Visible = true;
            }
        }

        private void FormatarDG()
        {
            try
            {
                grid.Columns[0].HeaderText = "";
                grid.Columns[1].HeaderText = "Nome";
                grid.Columns[2].HeaderText = "Cargo";
                grid.Columns[3].HeaderText = "Nota";
                grid.Columns[4].HeaderText = "Data";




                grid.Columns[0].Visible = false;


                grid.Columns[1].Width = 300;
                grid.Columns[2].Width = 300;
                grid.Columns[3].Width = 300;
                grid.Columns[4].Width = 300;
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um problema ao organizar tabela ultimas avaliações,\nverifique sua conexão e tente novamente em alguns instantes", "Últimas avaliações falhou", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void Listar()
        {
            try
            {
                con.Abrircon();
                sql = "SELECT * FROM avaliacao order by data DESC";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                con.Fecharcon();
                FormatarDG();
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um problema ao obter dados atualizados,\nverifique sua conexão e tente novamente em alguns instantes", "Conexão com BD falhou", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            {
                try
                {
                    // TODO: esta linha de código carrega dados na tabela 'gerenciadorDataSet.avaliacaoPorUser'. Você pode movê-la ou removê-la conforme necessário.
                    //this.avaliacaoPorUserTableAdapter.Fill(this.gerenciadorDataSet.avaliacaoPorUser);

                    lblUsuario.Text = Program.nomeUsuario;
                    lblCargo.Text = Program.cargoUsuario;
                    Listar();
                    FormatarDG();
                    
                    VerificarNivelUsuario();
                        
                }
                catch (Exception)
                {
                    MessageBox.Show("Houve um problema ao obter dados atualizados,\nverifique sua conexão e tente novamente em alguns instantes", "Conexão com BD falhou", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmAva>().Count() > 0)
            {
                Application.OpenForms.OfType<FrmAva>().First().Focus();
            }
            else
            {
                FrmAva form = new FrmAva();
                form.Show();
            }
         
        }

        private void FrmMenu_Activated(object sender, EventArgs e)
        {
            try { 
            Listar();
            }
            catch(Exception)
            {
                MessageBox.Show("Houve um problema ao obter dados atualizados,\nverifique sua conexão e tente novamente em alguns instantes", "Conexão com BD falhou", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Relatórios.RelAva2()
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();

        }

        private void pnlBaseForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Relatórios.RelAva()
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    Listar();
                }
                catch (Exception)
                {
                    MessageBox.Show("Houve um problema ao obter dados atualizados,\nverifique sua conexão e tente novamente em alguns instantes", "Conexão com BD falhou", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            PicAtx.Visible = false;
            _objForm?.Close();

            _objForm = new Relatórios.RelAva3()
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            pnlBaseForm.Controls.Add(_objForm);
            _objForm.Show();
        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            Listar();
            PicAtx.Visible = true;
        }
    }
}
