using System.Data.SqlClient;
using System.Configuration;


namespace ProjetoVendas.br.com.projeto.conexao
{
    public class ConnectionFactory
    {
        public SqlConnection GetConnection()
        {
            string conexao = ConfigurationManager.ConnectionStrings["bdvendas"].ConnectionString;

            return new SqlConnection(conexao);
        }
    }
}
