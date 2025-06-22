using Newtonsoft.Json;
using ProjetoVendas.br.com.projeto.dao;
using ProjetoVendas.br.com.projeto.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoVendas.br.com.projeto.view
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Digite o Nome do Cliente.");
                txtNome.Focus();
                return false;
            }

            string cnpj = mtbCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            if (string.IsNullOrWhiteSpace(cnpj) || (cnpj.Length != 11 && cnpj.Length != 14))
            {
                MessageBox.Show("Digite um CPF ou CNPJ válido.");
                mtbCpfCnpj.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text) && string.IsNullOrWhiteSpace(txtCelular.Text))
            {
                MessageBox.Show("Informe pelo menos um número de telefone.");
                txtTelefone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                MessageBox.Show("Digite o Endereço.");
                txtEndereco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Digite o Número do Endereço.");
                txtNumero.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("Digite o Bairro.");
                txtBairro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCidade.Text))
            {
                MessageBox.Show("Digite a Cidade.");
                txtCidade.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cbUf.Text))
            {
                MessageBox.Show("Selecione o Estado (UF).");
                cbUf.Focus();
                return false;
            }

            return true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            tabelaCliente.DefaultCellStyle.ForeColor = Color.Black;
            ClienteDAO dao = new ClienteDAO();
            tabelaCliente.DataSource = dao.ListarCliente();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void mtbCpfCnpj_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string digits = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (digits.Length <= 11)
            {
                if (digits.Length > 3)
                    digits = digits.Insert(3, ".");
                if (digits.Length > 7)
                    digits = digits.Insert(7, ".");
                if (digits.Length > 11)
                    digits = digits.Insert(11, "-");
            }
            else
            {
                if (digits.Length > 2)
                    digits = digits.Insert(2, ".");
                if (digits.Length > 6)
                    digits = digits.Insert(6, ".");
                if (digits.Length > 10)
                    digits = digits.Insert(10, "/");
                if (digits.Length > 15)
                    digits = digits.Insert(15, "-");
            }

            txt.TextChanged -= mtbCpfCnpj_TextChanged;
            txt.Text = digits;
            txt.SelectionStart = txt.Text.Length;
            txt.TextChanged += mtbCpfCnpj_TextChanged;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected_1(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btNovo_Click(object sender, EventArgs e)
        {

        }

        private void btSalvar_Click(object sender, EventArgs e)
        {

            if (!ValidarCampos())
                return;

            Cliente obj = new Cliente();

            obj.Nome = txtNome.Text;
            obj.CpfCnpj = mtbCpfCnpj.Text;
            obj.Email = txtEmail.Text;
            obj.Telefone = txtTelefone.Text;
            obj.Celular = txtCelular.Text;
            obj.Cep = txtCep.Text;
            obj.Endereco = txtEndereco.Text;
            obj.Numero = txtNumero.Text;
            obj.Complemento = txtComplemento.Text;
            obj.Bairro = txtBairro.Text;
            obj.Cidade = txtCidade.Text;
            obj.Estado = cbUf.Text;


            ClienteDAO dao = new ClienteDAO();
            dao.cadastrarCliente(obj);

            tabelaCliente.DataSource = dao.ListarCliente();

            Helppers.LimparCampos(this);
            txtNome.Focus();
        }

        private void tabelaCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btPesquisarCliente_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtCodigo.Text, out int id))
            {
                MessageBox.Show("Digite um código válido.");
                return;
            }

            try
            {
                Cliente obj = new Cliente();
                obj.Id = int.Parse(txtCodigo.Text); 

                ClienteDAO dao = new ClienteDAO();
                DataTable dt = dao.ClienteId(obj);

                if (dt.Rows.Count > 0)
                {
                    txtNome.Text = dt.Rows[0]["nome"].ToString();
                    mtbCpfCnpj.Text = dt.Rows[0]["cpf"].ToString();
                    txtEmail.Text = dt.Rows[0]["email"].ToString();
                    txtTelefone.Text = dt.Rows[0]["telefone"].ToString();
                    txtCelular.Text = dt.Rows[0]["celular"].ToString();
                    txtCep.Text = dt.Rows[0]["cep"].ToString();
                    txtEndereco.Text = dt.Rows[0]["endereco"].ToString();
                    txtNumero.Text = dt.Rows[0]["numero"].ToString();
                    txtComplemento.Text = dt.Rows[0]["complemento"].ToString();
                    txtBairro.Text = dt.Rows[0]["bairro"].ToString();
                    txtCidade.Text = dt.Rows[0]["cidade"].ToString();
                    cbUf.Text = dt.Rows[0]["estado"].ToString();
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar cliente: " + ex.Message);
            }
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(txtCodigo.Text, out int id))
            {
                MessageBox.Show("Digite um código válido para poder excluir");
                return;
            }

            Cliente obj = new Cliente();

            obj.Id = int.Parse(txtCodigo.Text);

            ClienteDAO dao = new ClienteDAO();

            dao.ExcluirCliente(obj);
            
            tabelaCliente.DataSource = dao.ListarCliente();
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {
        }

        private void btEditar_Click(object sender, EventArgs e)
        {
            Cliente obj = new Cliente();

            if (!int.TryParse(txtCodigo.Text, out int id))
            {
                MessageBox.Show("Digite um código válido.");
                return;
            }

            obj.Id = int.Parse(txtCodigo.Text);
            obj.Nome = txtNome.Text;
            obj.CpfCnpj = mtbCpfCnpj.Text;
            obj.Email = txtEmail.Text;
            obj.Telefone = txtTelefone.Text;
            obj.Celular = txtCelular.Text;
            obj.Cep = txtCep.Text;
            obj.Endereco = txtEndereco.Text;
            obj.Numero = txtNumero.Text;
            obj.Complemento = txtComplemento.Text;
            obj.Bairro = txtBairro.Text;
            obj.Cidade = txtCidade.Text;
            obj.Estado = cbUf.Text;

            if (txtNome.Text.Length <= 0)
            {
                MessageBox.Show("Digitie o Nome do Cliente");
            }

            ClienteDAO dao = new ClienteDAO();
            dao.EditarCliente(obj);

            tabelaCliente.DataSource = dao.ListarCliente();
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCodigo.Text, out int id))
            {
                MessageBox.Show("Digite um código válido.");
                return;
            }

            try
            {
                Cliente obj = new Cliente();
                obj.Id = int.Parse(txtCodigo.Text);

                ClienteDAO dao = new ClienteDAO();
                DataTable dt = dao.ClienteId(obj);

                if (dt.Rows.Count > 0)
                {
                    txtNome.Text = dt.Rows[0]["nome"].ToString();
                    mtbCpfCnpj.Text = dt.Rows[0]["cpf"].ToString();
                    txtEmail.Text = dt.Rows[0]["email"].ToString();
                    txtTelefone.Text = dt.Rows[0]["telefone"].ToString();
                    txtCelular.Text = dt.Rows[0]["celular"].ToString();
                    txtCep.Text = dt.Rows[0]["cep"].ToString();
                    txtEndereco.Text = dt.Rows[0]["endereco"].ToString();
                    txtNumero.Text = dt.Rows[0]["numero"].ToString();
                    txtComplemento.Text = dt.Rows[0]["complemento"].ToString();
                    txtBairro.Text = dt.Rows[0]["bairro"].ToString();
                    txtCidade.Text = dt.Rows[0]["cidade"].ToString();
                    cbUf.Text = dt.Rows[0]["estado"].ToString();
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar cliente: " + ex.Message);
            }
        }

        private void txtpesquisa_TextChanged(object sender, EventArgs e)
        {
            string PesquisaNome = txtpesquisa.Text;

            ClienteDAO dao = new ClienteDAO();

            tabelaCliente.DataSource = dao.ListarClienteNome(PesquisaNome);
        }

        private void txtpesquisa_Leave(object sender, EventArgs e)
        {
            string PesquisaNome = txtpesquisa.Text;

            ClienteDAO dao = new ClienteDAO();

            tabelaCliente.DataSource = dao.ListarClienteNome(PesquisaNome);
        }

        private void tabelaCliente_Enter(object sender, EventArgs e)
        {

        }

        private async void BuscarCnpj_Click(object sender, EventArgs e)
        {
            try
            {
                string cnpj = mtbCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "");
                string url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

                if (cnpj.Length != 14)
                {
                    MessageBox.Show("CNPJ inválido. Ele deve conter 14 dígitos.");
                    return;
                }

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "WindowsFormsApp");

                    string response = await client.GetStringAsync(url);

                    dynamic dados = JsonConvert.DeserializeObject(response);

                    txtNome.Text = dados.nome;
                    txtTelefone.Text = dados.telefone;
                    txtEmail.Text = dados.email;
                    txtEndereco.Text = dados.logradouro;
                    txtNumero.Text = dados.numero;
                    txtBairro.Text = dados.bairro;
                    txtCidade.Text = dados.municipio;
                    cbUf.Text = dados.uf;
                    txtComplemento.Text = dados.complemento;
                    txtCep.Text = dados.cep;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar CNPJ: " + ex.Message);
            }
        }
    }
}
