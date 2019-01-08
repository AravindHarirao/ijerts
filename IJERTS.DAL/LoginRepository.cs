using IJERTS.Objects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.DAL
{
    public class LoginRepository : ILoginRepository
    {

        public Users ValidateLogin(Users users)
        {
            string queryLogin = "SELECT UserId, FirstName, LastName, Email, Phone, Password, IsActive FROM users where IsActive = 1 AND Email = ?Email";
            Users user = new Users();
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.Add(new MySqlParameter("?Email", users.Email));
                cmd.Parameters.Add(new MySqlParameter("?Password", users.Password));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryLogin;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserId = Convert.ToInt64(reader.GetString("UserId"));
                        user.FirstName = reader.GetString("FirstName");
                        user.LastName = reader.GetString("LastName");
                        user.Email = reader.GetString("Email");
                        user.Phone = reader.GetString("Phone");
                        user.Password = reader.GetString("Password");
                        user.IsActive = Convert.ToInt32(reader.GetString("IsActive"));
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Users ValidateEditorLogin(Users users)
        {
            string queryLogin = "SELECT UserId, FirstName, LastName, Email, Phone, Password, IsActive FROM users where IsActive = 1 AND Email = ?Email AND UserType = 'E'";
            Users user = new Users();
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.Add(new MySqlParameter("?Email", users.Email));
                cmd.Parameters.Add(new MySqlParameter("?Password", users.Password));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryLogin;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserId = Convert.ToInt64(reader.GetString("UserId"));
                        user.FirstName = reader.GetString("FirstName");
                        user.LastName = reader.GetString("LastName");
                        user.Email = reader.GetString("Email");
                        user.Phone = reader.GetString("Phone");
                        user.Password = reader.GetString("Password");
                        user.IsActive = Convert.ToInt32(reader.GetString("IsActive"));
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertLoginHistory(UserLoginHistory userLoginHistory)
        {
            string query = "INSERT  INTO userloginhistory (UserId, SessionId, LoggedInTime, IsActive) "
                         + "Values (?UserId, ?SessionId, NOW(), ?IsActive)";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?UserId", userLoginHistory.UserId));
                cmd.Parameters.Add(new MySqlParameter("?SessionId", userLoginHistory.SessionId));
                cmd.Parameters.Add(new MySqlParameter("?IsActive", true));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateUserSession(UserLoginHistory userLoginHistory)
        {
            string query = "UPDATE userloginhistory SET LoggedOutTime = NOW(), IsActive = ?IsActive WHERE UserId = ?UserId AND SessionId = ?SessionId AND IsActive = 1 ";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?UserId", userLoginHistory.UserId));
                cmd.Parameters.Add(new MySqlParameter("?SessionId", userLoginHistory.SessionId));
                cmd.Parameters.Add(new MySqlParameter("?IsActive", false));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Users ValidateUserActivation(string sActivateLink)
        {
            string queryLogin = "SELECT UserId, FirstName, LastName, Email, Phone, Password, IsActive FROM users where IsActive = 1 AND UserActivationValue = ?ActivateLink";
            Users user = new Users();
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.Add(new MySqlParameter("@ActivateLink", sActivateLink));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryLogin;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserId = Convert.ToInt64(reader.GetString("UserId"));
                        user.FirstName = reader.GetString("FirstName");
                        user.LastName = reader.GetString("LastName");
                        user.Email = reader.GetString("Email");
                        user.Phone = reader.GetString("Phone");
                        user.Password = reader.GetString("Password");
                        user.IsActive = Convert.ToInt32(reader.GetString("IsActive"));

                        ActivateUser(user);
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActivateUser(Users user)
        {
            string query = "UPDATE users SET UserActivated = ?UserActivated, UpdatedDatetime = Now(), UpdatedBy = 'System' WHERE UserId = ?UserId AND IsActive = 1 ";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?UserId", user.UserId));
                cmd.Parameters.Add(new MySqlParameter("?UserActivated", true));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ChangePassword(Users users)
        {
            string query = "UPDATE users SET Password = ?Password, UpdatedDatetime = Now() WHERE UserId = ?UserId ";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?UserId", users.UserId));
                cmd.Parameters.Add(new MySqlParameter("?Password", users.Password));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
