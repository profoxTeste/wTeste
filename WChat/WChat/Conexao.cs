using System;
using System.Data;
using System.Data.SqlClient;

namespace WChat
{
    public class Conexao  {

        public string stringConexao = "Data Source=.\\SQLEXPRESS;Initial Catalog =WChat;Integrated Security=SSPI";
        
        private IDbConnection conn;
        public Conexao() {
            try {
                if (conn == null)
                    conn = new SqlConnection(stringConexao);
                if (conn.State == ConnectionState.Closed) {
                    conn.Open();
                }
            }
            catch (SqlException sqlEx) {
                throw new Exception(sqlEx.Number + " - " + sqlEx.Message);
            }
            catch (Exception Ex) {
                throw new Exception(Ex.Message);
            }
        }

        public void FechaConexao() {
            try {
                if (conn == null)
                    return;
                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }
            }
            catch (SqlException sqlEx) {
                throw new Exception(sqlEx.Number + " - " + sqlEx.Message);
            }
            catch (Exception Ex) {
                throw new Exception(Ex.Message);
            }
        }

        public IDbConnection PegaConexao {
            get {
                if (conn == null)
                    conn = new SqlConnection();
                return conn;
            }
        }

        public IDbTransaction PegaTransacao {
            get {
                return conn.BeginTransaction();
            }
        }



        public IDataReader Ler(string comandoValue) {
            try {
                Conexao con = new Conexao();
                IDbConnection conexao = con.PegaConexao;
                IDbCommand comando = conexao.CreateCommand();
                comando.CommandText = comandoValue;
                IDataReader cursor = comando.ExecuteReader();
                conexao.Dispose();
                return cursor;
            }
            catch (Exception ex) {
                string mensagem = "\nMensagem: " + ex.Message;
                mensagem += "\nClasse: Conexao";
                mensagem += "\nMétodo: Ler";
                throw new Exception(mensagem);
            }
        }


    }
}
