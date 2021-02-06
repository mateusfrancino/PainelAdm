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

namespace PainelAdm.Forms_Menu
{
    public partial class FrmNovoUsuario : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;

        string usuarioAntigo;

        public FrmNovoUsuario()
        {
            InitializeComponent();
        }

        private void BuscarNome()
        {
            con.Abrircon();
            sql = "SELECT * FROM usuarios where nome LIKE @nome order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscarNome.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.Fecharcon();

            FormatarDG();
        }

        private void FormatarDG()
        {
            grid.Columns[0].HeaderText = "";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "CPF";
            grid.Columns[3].HeaderText = "Celular";
            grid.Columns[4].HeaderText = "Endereço";
            grid.Columns[5].HeaderText = "Bairro";
            grid.Columns[6].HeaderText = "Cidade";
            grid.Columns[7].HeaderText = "UF";
            grid.Columns[8].HeaderText = "Usuario";
            grid.Columns[9].HeaderText = "Senha";
            grid.Columns[10].HeaderText = "Função";
            grid.Columns[11].HeaderText = "Data";


            grid.Columns[0].Visible = false;
            //grid.Columns[1].Width = 352;
        }
        private void Listar()
        {
            con.Abrircon();
            sql = "SELECT * FROM usuarios order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.Fecharcon();
            FormatarDG();
        }


        private void CarregarCombobox()
        {
            con.Abrircon();
            sql = "SELECT * FROM cargos order by cargo asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbFuncao.DataSource = dt;
            //cbFuncao.ValueMember = "id";
            cbFuncao.DisplayMember = "cargo";
            con.Fecharcon();
        }
        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
            txtCpf.Enabled = true;
            txtCelular.Enabled = true;
            txtEndereco.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            cbUf.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            cbFuncao.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = true;
            txtNome.Focus();

        }

        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtCpf.Enabled = false;
            txtCelular.Enabled = false;
            txtEndereco.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            cbUf.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            cbFuncao.Enabled = false;

        }

        private void limparCampos()
        {
            txtNome.Text = "";
            txtCpf.Text = "";
            txtCelular.Text = "";
            txtEndereco.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";

        }

        private void FrmNovoUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                txtBuscarNome.Enabled = true;
                Listar();
                CarregarCombobox();
            }

            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Text = "";
                    txtNome.Focus();
                    return;
                }

                if (txtUsuario.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha o Usuário", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                    return;
                }

                if (txtSenha.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha a Senha", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Text = "";
                    txtSenha.Focus();
                    return;
                }

                //CODIGO DO BOTÃO SALVAR
                con.Abrircon();
                sql = "INSERT INTO usuarios (nome, cpf, celular, endereco, bairro, cidade, uf, usuario, senha, funcao, data) VALUES (@nome, @cpf, @celular, @endereco, @bairro, @cidade, @uf, @usuario, @senha, @funcao, curDate())";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
                cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
                cmd.Parameters.AddWithValue("@uf", cbUf.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                cmd.Parameters.AddWithValue("@funcao", cbFuncao.Text);

                //VERIFICAÇÃO SE EXISTE USUARIO CADASTRADO!
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM usuarios where usuario = @usuario", con.con);
                cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Já Existe Um Funcionário Com Este Usuário", "Dados Iguais", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                    return;
                }


                //LINHA DE EXCUÇÃO LOGO ABAIXO

                cmd.ExecuteNonQuery();
                con.Fecharcon();

                MessageBox.Show("Usuário Salvo Com Sucesso", "Dados Salvos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                limparCampos();
                desabilitarCampos();
                cbFuncao.Text = "";
                cbUf.Text = "";
                Listar();
            }

            catch (Exception)
            {
                MessageBox.Show("Não foi possível salvar seu registro, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

            HabilitarCampos();
            limparCampos();
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            cbFuncao.Text = "";
            cbUf.Text = "";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                HabilitarCampos();
                if (txtNome.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha o Nome", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Text = "";
                    txtNome.Focus();
                    return;
                }

                if (txtUsuario.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha o Usuário", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                    return;
                }

                if (txtSenha.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha a Senha", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Text = "";
                    txtSenha.Focus();
                    return;
                }

                //CODIGO PARA O BOTÃO EDITAR

                con.Abrircon();
                sql = "UPDATE usuarios SET nome = @nome, cpf = @cpf, celular = @celular, endereco = @endereco, bairro = @bairro, cidade = @cidade, uf = @uf, usuario = @usuario, senha = @senha, funcao = @funcao where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
                cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
                cmd.Parameters.AddWithValue("@uf", cbUf.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
                cmd.Parameters.AddWithValue("@funcao", cbFuncao.Text);
                cmd.Parameters.AddWithValue("@id", id);

                //VERIFICAÇÃO SE EXISTE USUARIO CADASTRADO!
                if (txtUsuario.Text != usuarioAntigo)
                {
                    MySqlCommand cmdVerificar;

                    cmdVerificar = new MySqlCommand("SELECT * FROM usuarios where usuario = @usuario", con.con);
                    cmdVerificar.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmdVerificar;
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Já Existe Um Funcionário Com Este Usuário", "Dados Iguais", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsuario.Text = "";
                        txtUsuario.Focus();
                        return;
                    }
                }


                cmd.ExecuteNonQuery();
                con.Fecharcon();

                MessageBox.Show("Usuário Editado Com Sucesso", "Dados Editados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnSalvar.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
            }

            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (resultado == DialogResult.Yes)
                {
                    //CODIGO BOTAO EXCLUIR
                    con.Abrircon();
                    sql = "DELETE FROM usuarios where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.Fecharcon();

                    MessageBox.Show("Usuário Excluido Com Sucesso", "Dados Excluidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnNovo.Enabled = true;
                    limparCampos();
                    desabilitarCampos();
                    Listar();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Não foi possível excluir, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        { try { 
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            HabilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtCpf.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtCelular.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtBairro.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtCidade.Text = grid.CurrentRow.Cells[6].Value.ToString();
            cbUf.Text = grid.CurrentRow.Cells[7].Value.ToString();
            txtUsuario.Text = grid.CurrentRow.Cells[8].Value.ToString();
            txtSenha.Text = grid.CurrentRow.Cells[9].Value.ToString();
            cbFuncao.Text = grid.CurrentRow.Cells[10].Value.ToString();

            usuarioAntigo = grid.CurrentRow.Cells[8].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados OFFILINE, Verifique sua conexão e tente novamente!", "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscarNome_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BuscarNome();
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
    }
}
