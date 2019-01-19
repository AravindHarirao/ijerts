using IJERTS.Objects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.DAL
{
    public class HomeRepository : IHomeRepository
    {
        public int PostQuery(Queries query)
        {
            string queryText = "INSERT INTO `ijerts`.Queries (FirstName, LastName, Email, QueryText, QueryStatus, IsActive, CreatedBy, CreatedOn) " +
                                " VALUES" +
                                " (?FirstName, ?LastName, ?Email, ?QueryText, 'New', 1, 'System', now() ); ";
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?FirstName", query.FirstName));
                cmd.Parameters.Add(new MySqlParameter("?LastName", query.LastName));
                cmd.Parameters.Add(new MySqlParameter("?Email", query.Email));
                cmd.Parameters.Add(new MySqlParameter("?QueryText", query.QueryText));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryText;
                    cmd.ExecuteNonQuery();
                }

                result = 0;
            }
            catch (Exception ex)
            {
                result = 1;
                throw ex;
            }

            return result;
        }

    }
}
