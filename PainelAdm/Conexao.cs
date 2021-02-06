using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PainelAdm
{
    
    class Conexao
    {
        //CONEXÃO COM O BANCO DE DADOS LOCAL
        string conec = "SERVER=132.255.155.151; DATABASE=gerenciador; UID=inet; PWD=f1r3w@ll; PORT=5306;";
        public MySqlConnection con = null;


        public void Abrircon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Fecharcon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            } 
            
        }
    }


}

