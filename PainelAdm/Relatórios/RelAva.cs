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

namespace PainelAdm.Relatórios
{
    public partial class RelAva : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        public RelAva()
        {
            InitializeComponent();
        }

        private void RelAva_Load(object sender, EventArgs e)
        {
            DateTime data = DateTime.Today;
            try
            {
                dtInicial.Value = data = data.AddMonths(-1);
                dtFinal.Value = DateTime.Today;
                //cbTipo.SelectedIndex = 1;
                BuscarData();
                CarregarCombobox();
                this.avaliacaoPorFuncionarioTableAdapter.Fill(this.dataSet1.avaliacaoPorFuncionario, Convert.ToDateTime(dtInicial.Text), Convert.ToDateTime(dtFinal.Text), cbTipo.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarData()
        {
            try
            {
                this.avaliacaoPorFuncionarioTableAdapter.Fill(this.dataSet1.avaliacaoPorFuncionario, Convert.ToDateTime(dtInicial.Text), Convert.ToDateTime(dtFinal.Text), cbTipo.Text);

                this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataInicial", dtInicial.Text));
                this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataFinal", dtFinal.Text));

                this.reportViewer1.RefreshReport();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarCombobox()
        {
            try
            {
                con.Abrircon();
                sql = "SELECT * FROM usuarios order by nome asc";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbTipo.DataSource = dt;
                cbTipo.ValueMember = "id";
                cbTipo.DisplayMember = "nome";

                con.Fecharcon();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dtFinal_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BuscarData();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtInicial_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BuscarData();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BuscarData();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
