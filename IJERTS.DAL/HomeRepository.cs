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
            string queryText = "INSERT INTO Queries (FirstName, LastName, Email, QueryText, QueryStatus, IsActive, CreatedBy, CreatedOn) " +
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

        public List<Queries> GetQueries()
        {
            string queryText = " select QueryId, FirstName, LastName, Email, QueryText, QueryAnswer, QueryStatus, CreatedOn from queries "
                                        + "WHERE QueryStatus = 'New' Order by CreatedOn ASC ";
            List<Queries> queries = new List<Queries>();
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryText;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Queries query = new Queries();
                        query.QueryId = reader.GetInt32("QueryId");
                        query.FirstName = reader.GetString("FirstName");
                        query.LastName= reader.GetString("LastName");
                        query.QueryStatus = reader.GetString("QueryStatus");
                        query.QueryAnswer = reader.GetString("QueryAnswer");
                        query.Email = reader.GetString("Email");
                        query.QueryText = reader.GetString("QueryText");

                        queries.Add(query);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return queries;
        }

        public Queries GetQueriesForId(int id)
        {
            string queryText = " select QueryId, FirstName, LastName, Email, QueryText, QueryAnswer, QueryStatus, CreatedOn from queries " +
                                         " WHERE QueryId = ?queryId ";
            Queries queries = new Queries();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?queryId", id));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryText;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        queries.QueryId = reader.GetInt32("QueryId");
                        queries.FirstName = reader.GetString("FirstName");
                        queries.LastName = reader.GetString("LastName");
                        queries.QueryStatus = reader.GetString("QueryStatus");
                        queries.QueryAnswer = reader.GetString("QueryAnswer");
                        queries.Email = reader.GetString("Email");
                        queries.QueryText = reader.GetString("QueryText");

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return queries;
        }


        public int UpdateQuery(int queryId, string response)
        {
            string queryText = "UPDATE `queries` SET QueryAnswer = ?response, QueryStatus = 'Closed', UpdatedBy = 'System', UpdatedOn = now() "
                                    + " WHERE QueryId = ?queryId";

            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?queryId", queryId));
                cmd.Parameters.Add(new MySqlParameter("?response", response));

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
