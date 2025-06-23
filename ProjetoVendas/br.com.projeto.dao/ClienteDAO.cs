using Dapper;
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
                    (@Nome, '', @CpfCnpj, @Email, @Telefone, @Celular, @Cep, @Endereco, @Numero, @Complemento, @Bairro, @Cidade, @Estado)";

                conexao.Open();
                conexao.Execute(sql, obj);
                conexao.Close();

                MessageBox.Show("Cliente Cadastrado!");
            }
            catch (Exception erro)
            {

                MessageBox.Show("Erro ao Cadastrar Cliente:" + erro);
            }
        }

        #endregion

        #region Listar Cliente

        public List<ClienteResumo> ListarCliente()
        {
            try
            {
                string sql = @"
                                SELECT 
                                    id,
                                    nome,
                                    cpf AS CpfCnpj,
                                    email,
                                    telefone,
                                    celular,
                                    endereco + ', ' + CAST(numero AS VARCHAR) AS Endereco,
                                    cidade + ' - ' + estado AS Cidade
                                FROM 
                                    [dbo].[CAD_clientes];";

                conexao.Open();
                var lista = conexao.Query<ClienteResumo>(sql).ToList();
                conexao.Close();

                return lista;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao Listar Clientes" + erro);
                return null;
            }
        }

        #endregion

        #region ClienteId

        public Cliente ClienteId(int id)
        {
            try
            {
                string sql = @"
                                SELECT [id]
                                      ,[nome]
                                      ,[cpf] AS 'CpfCnpj'
                                      ,[email]
                                      ,[telefone]
                                      ,[celular]
                                      ,[cep]
                                      ,[endereco]
                                      ,[numero]
                                      ,[complemento]
                                      ,[bairro]
                                      ,[cidade]
                                      ,[estado]
                                  FROM [dbo].[CAD_clientes]
                                WHERE [id] = @Id;";
                conexao.Open();
                var cliente = conexao.QueryFirstOrDefault<Cliente>(sql, new { Id = id });
                conexao.Close();
                return cliente;

            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao buscar cliente por ID: " + erro.Message);
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
                                    [nome] = @Nome,
                                    [rg] = '', 
                                    [cpf] = @CpfCnpj,
                                    [email] = @Email,
                                    [telefone] = @Telefone,
                                    [celular] = @Celular,
                                    [cep] = @Cep,
                                    [endereco] = @Endereco,
                                    [numero] = @Numero,
                                    [complemento] = @Complemento,
                                    [bairro] = @Bairro,
                                    [cidade] = @Cidade,
                                    [estado] = @Estado
                                WHERE 
                                    [id] = @id
                              ";


                conexao.Open();
                conexao.Execute(sql, obj);
                conexao.Close();

                MessageBox.Show("Cliente Alterado!");

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

