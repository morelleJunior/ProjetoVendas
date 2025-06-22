using ProjetoVendas.br.com.projeto.conexao;
using ProjetoVendas.br.com.projeto.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoVendas.br.com.projeto.dao
{
    public class ClienteDAO
    {

        private SqlConnection conexao;
        public ClienteDAO()
        {
            this.conexao = new ConnectionFactory().GetConnection();
        }

        #region Cadastrar Cliente

        public void cadastrarCliente(Cliente obj)
        {
            try
            {

                string sql = @"
                    INSERT INTO [dbo].[CAD_clientes]
                    ([nome], [rg], [cpf], [email], [telefone], [celular], [cep], [endereco], [numero], [complemento], [bairro], [cidade], [estado])
                    VALUES
                    (@nome, '', @cpf, @email, @telefone, @celular, @cep, @endereco, @numero, @complemento, @bairro, @cidade, @estado)";


                SqlCommand executaSql = new SqlCommand(sql, conexao);

                executaSql.Parameters.AddWithValue("@nome", obj.Nome);
                executaSql.Parameters.AddWithValue("@cpf", obj.CpfCnpj);
                executaSql.Parameters.AddWithValue("@email", obj.Email);
                executaSql.Parameters.AddWithValue("@telefone", obj.Telefone);
                executaSql.Parameters.AddWithValue("@celular", obj.Celular);
                executaSql.Parameters.AddWithValue("@cep", obj.Cep);
                executaSql.Parameters.AddWithValue("@endereco", obj.Endereco);
                executaSql.Parameters.AddWithValue("@numero", obj.Numero);
                executaSql.Parameters.AddWithValue("@complemento", obj.Complemento);
                executaSql.Parameters.AddWithValue("@bairro", obj.Bairro);
                executaSql.Parameters.AddWithValue("@cidade", obj.Cidade);
                executaSql.Parameters.AddWithValue("@estado", obj.Estado);


                conexao.Open();
                executaSql.ExecuteNonQuery();

                MessageBox.Show("Cliente Cadastrado!");

                conexao.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao Cadastrar Cliente:" + erro);
            }
        }

        #endregion

        #region Listar Cliente

        public DataTable ListarCliente()
        {
            try
            {
                DataTable tabelaCliente = new DataTable();

                string sql = @"
                                SELECT 
                                    [id] AS 'Código',
                                    [nome] AS 'Nome',
                                    [cpf] AS 'CPF / CNPJ',
                                    [telefone] AS 'Telefone',
                                    [celular] AS 'Celular',
                                    [endereco] + ', ' + CAST([numero] AS VARCHAR) AS 'Endereço',
                                    [cidade] + ' - ' + [estado] AS 'Cidade'
                                FROM 
                                    [dbo].[CAD_clientes];";

                SqlCommand executaSql = new SqlCommand(sql, conexao);

                conexao.Open();
                executaSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(executaSql);

                da.Fill(tabelaCliente);

                conexao.Close();

                return tabelaCliente;



            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao Listar Clientes" + erro);
                return null;
            }
        }

        #endregion

        #region ClienteId

        public DataTable ClienteId(Cliente obj)
        {
            try
            {
                DataTable tabelaCliente = new DataTable();

                string sql = @"
                                SELECT
                                    *
                                FROM 
                                    [dbo].[CAD_clientes]
                                WHERE [id] = @Id;";

                SqlCommand executaSql = new SqlCommand(sql, conexao);

                executaSql.Parameters.AddWithValue("@id", obj.Id);

                conexao.Open();
                executaSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(executaSql);

                da.Fill(tabelaCliente);

                conexao.Close();

                return tabelaCliente;

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao Listar Clientes" + erro);
                return null;
            }
        }

        #endregion

        #region Editar Cliente

        public void EditarCliente(Cliente obj)
        {
            try
            {

                string sql = @"
                                UPDATE [dbo].[CAD_clientes]
                                SET 
                                    [nome] = @nome,
                                    [rg] = '', 
                                    [cpf] = @cpf,
                                    [email] = @email,
                                    [telefone] = @telefone,
                                    [celular] = @celular,
                                    [cep] = @cep,
                                    [endereco] = @endereco,
                                    [numero] = @numero,
                                    [complemento] = @complemento,
                                    [bairro] = @bairro,
                                    [cidade] = @cidade,
                                    [estado] = @estado
                                WHERE 
                                    [id] = @id
                              ";

                SqlCommand executaSql = new SqlCommand(sql, conexao);

                executaSql.Parameters.AddWithValue("@id", obj.Id);
                executaSql.Parameters.AddWithValue("@nome", obj.Nome);
                executaSql.Parameters.AddWithValue("@cpf", obj.CpfCnpj);
                executaSql.Parameters.AddWithValue("@email", obj.Email);
                executaSql.Parameters.AddWithValue("@telefone", obj.Telefone);
                executaSql.Parameters.AddWithValue("@celular", obj.Celular);
                executaSql.Parameters.AddWithValue("@cep", obj.Cep);
                executaSql.Parameters.AddWithValue("@endereco", obj.Endereco);
                executaSql.Parameters.AddWithValue("@numero", obj.Numero);
                executaSql.Parameters.AddWithValue("@complemento", obj.Complemento);
                executaSql.Parameters.AddWithValue("@bairro", obj.Bairro);
                executaSql.Parameters.AddWithValue("@cidade", obj.Cidade);
                executaSql.Parameters.AddWithValue("@estado", obj.Estado);



                conexao.Open();
                executaSql.ExecuteNonQuery();

                MessageBox.Show("Cliente Alterado!");

                conexao.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao Alterar Cliente:" + erro);
            }
        }

        #endregion

        #region Excluir Cliente

        public void ExcluirCliente(Cliente obj)
        {
            try
            {

                string sql = @"
                                DELETE FROM [dbo].[CAD_clientes]                               
                                WHERE 
                                    [id] = @id
                              ";

                SqlCommand executaSql = new SqlCommand(sql, conexao);

                executaSql.Parameters.AddWithValue("@id", obj.Id);

                conexao.Open();
                executaSql.ExecuteNonQuery();

                MessageBox.Show("Cliente Excluido!");

                conexao.Close();
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao Excluir Cliente:" + erro);
            }
        }

        #endregion

        #region Buscar Cliente Nome

        public DataTable ListarClienteNome(string PesquisaNome)
        {
            try
            {
                DataTable tabelaCliente = new DataTable();

                string sql = @"
                                SELECT 
                                    [id] AS 'Código',
                                    [nome] AS 'Nome',
                                    [cpf] AS 'CPF / CNPJ',
                                    [telefone] AS 'Telefone',
                                    [celular] AS 'Celular',
                                    [endereco] + ', ' + CAST([numero] AS VARCHAR) AS 'Endereço',
                                    [cidade] + ' - ' + [estado] AS 'Cidade'
                                FROM 
                                    [dbo].[CAD_clientes]
                                WHERE [nome] LIKE @PesquisaNome;";

                SqlCommand executaSql = new SqlCommand(sql, conexao);

                conexao.Open();

                executaSql.Parameters.AddWithValue("@PesquisaNome", "%" + PesquisaNome + "%");

                executaSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(executaSql);

                da.Fill(tabelaCliente);

                conexao.Close();

                return tabelaCliente;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao Listar Clientes" + erro);
                return null;
            }
        }

        #endregion

    }
}

