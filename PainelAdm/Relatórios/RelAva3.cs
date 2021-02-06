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
    public partial class RelAva3 : Form
    {
        public RelAva3()
        {
            InitializeComponent();
        }

        private void RelAva3_Load(object sender, EventArgs e)
        {
            DateTime data = DateTime.Today;
            try
            {
                dtInicial.Value = data = data.AddMonths(-1);
                dtFinal.Value = DateTime.Today;
                BuscarData();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.reportViewer1.RefreshReport();
        }

        private void BuscarData()
        {
            try
            {
                this.notaPorDataTableAdapter.Fill(this.dataSet1.notaPorData, Convert.ToDateTime(dtInicial.Text), Convert.ToDateTime(dtFinal.Text));

                this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dtInicial", dtInicial.Text));
                this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dtFinal", dtFinal.Text));
                this.reportViewer1.RefreshReport();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
