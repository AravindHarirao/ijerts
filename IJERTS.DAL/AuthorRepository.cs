using IJERTS.Objects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.DAL
{
    public class AuthorRepository : IAuthorRepository
    {
        string conString = "server=localhost;user id=root;database=ijerts;password=Oracle2018!";

        public void Register(Users user)
        {
            string query = "INSERT  INTO users (FirstName, LastName, Email, Password, Phone, UserType, UserActivationValue, IsActive, CreatedDateTime, CreatedBy, UpdatedDatetime, UpdatedBy) "
                                + " Values "
                                + " (?FirstName, ?LastName, ?Email, ?Password, ?Phone, ?UserType, ?UserActivationValue, 1, Now(), 'System', Now(), 'System')";
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.Add(new MySqlParameter("?FirstName", user.FirstName));
                cmd.Parameters.Add(new MySqlParameter("?LastName", user.LastName));
                cmd.Parameters.Add(new MySqlParameter("?Email", user.Email));
                cmd.Parameters.Add(new MySqlParameter("?Phone", user.Phone));
                cmd.Parameters.Add(new MySqlParameter("?Password", user.Password));
                cmd.Parameters.Add(new MySqlParameter("?UserType", "A"));
                cmd.Parameters.Add(new MySqlParameter("?UserActivationValue", user.UserActivationValue));

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

        public Users AuthorLogin(LoginRequest loginReq)
        {
            string queryLogin = "SELECT UserId, FirstName, LastName, Email, Phone, Password, IsActive FROM users where IsActive = 0 AND Email = ?Email and Password = ?Password";
            Users loggedInUser = new Users();
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.Add(new MySqlParameter("?Email", loginReq.Username));
                cmd.Parameters.Add(new MySqlParameter("?Password", loginReq.Password));

                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryLogin;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        loggedInUser.UserId = Convert.ToInt64(reader.GetString("UserId"));
                        loggedInUser.FirstName = reader.GetString("FirstName");
                        loggedInUser.LastName = reader.GetString("LastName");
                        loggedInUser.Email = reader.GetString("Email");
                        loggedInUser.Phone = reader.GetString("Phone");
                        loggedInUser.Password = reader.GetString("Password");
                        loggedInUser.IsActive = Convert.ToInt32(reader.GetString("IsActive"));
                    }
                }
                return loggedInUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetAllUsers(List<Users> users)
        {
            throw new NotImplementedException();
        }

        public int PostPapers(Paper paper)
        {
            int errorCount = 0;
            string queryPaper = "INSERT INTO Papers(UserId, MainTitle, ShortDesc, Subject, Tags, CompleteFilePath, FileName,"
                                + "CreatedBy, CreatedDateTime, UpdatedBy, UpdatedDateTime)"
                                + "Values"
                                + "("
                                + "?UserId, ?MainTitle, ?ShortDesc, ?Subject, ?Tags, ?CompleteFilePath,"
                                + "?FileName, ?CreatedBy, Now(), ?UpdatedBy, Now()"
                                + ");";

            string queryMaxPaperId = "SELECT MAX(Paperid) from Papers";

            string queryAuthors = "INSERT INTO Authors (PaperId, AuthorFirstName, AuthorLastName, "
                                        + "AuthorDepartment, AuthorOrganisation, AuthorSubject)"
                                        + "values"
                                        + "(?PaperId, ?AuthorFirstName, ?AuthorLastName,"
                                        + "?AuthorDepartment, ?AuthorOrganisation, ?AuthorSubject);";
            string queryPaperStatus = "INSERT INTO PaperStatus(PaperId, UserId, Status, CreatedBy, CreatedDatetime)"
                                        + " VALUES "
                                        + "(?PaperId, ?UserID, 'NEW', 'System', Now())";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?UserID", paper.UserId));
                cmd.Parameters.Add(new MySqlParameter("?MainTitle", paper.MainTitle));
                cmd.Parameters.Add(new MySqlParameter("?ShortDesc", paper.ShortDesc));
                cmd.Parameters.Add(new MySqlParameter("?Subject", paper.Subject));
                cmd.Parameters.Add(new MySqlParameter("?Tags", paper.Tags));
                cmd.Parameters.Add(new MySqlParameter("?CompleteFilePath", paper.PaperPath));
                cmd.Parameters.Add(new MySqlParameter("?FileName", paper.FileName));
                cmd.Parameters.Add(new MySqlParameter("?CreatedBy", paper.CreatedBy));
                cmd.Parameters.Add(new MySqlParameter("?UpdatedBy", paper.UpdatedBy));



                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryPaper;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = queryMaxPaperId;
                    var paperId = cmd.ExecuteScalar();

                    foreach (PaperAuthors papAuthor in paper.Authors)
                    {
                        cmd.CommandText = queryAuthors;
                        cmd.Parameters.Add(new MySqlParameter("?PaperId", paperId));
                        cmd.Parameters.Add(new MySqlParameter("?AuthorFirstName", papAuthor.AuthorFirstName));
                        cmd.Parameters.Add(new MySqlParameter("?AuthorLastName", papAuthor.AuthorLastName));
                        cmd.Parameters.Add(new MySqlParameter("?AuthorDepartment", papAuthor.Department));
                        cmd.Parameters.Add(new MySqlParameter("?AuthorOrganisation", papAuthor.Organisation));
                        cmd.Parameters.Add(new MySqlParameter("?AuthorSubject", papAuthor.Subject));
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    if (con.State != System.Data.ConnectionState.Closed)
                        con.Close();
                    con.Open();

                    cmd.CommandText = queryPaperStatus;
                    cmd.Parameters.Add(new MySqlParameter("?PaperId", paperId));
                    cmd.Parameters.Add(new MySqlParameter("?UserID", paper.UserId));
                    cmd.ExecuteNonQuery();

                }



                return errorCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Paper> TrackMyPaper(int userId)
        {
            List<Paper> papers = new List<Paper>();
            string queryPaper = "SELECT * FROM ijerts.papers inner join "
                + " (SELECT PaperId, StatusId, Max(StatusId), UserID, `Status`, CreatedBy, CreatedDatetime FROM PaperStatus "
                + " GROUP BY PaperId, UserID "
                + " ) AA ON AA.PaperID = AA.PaperId "
                + " WHERE AA.StatusID = ( "
                + "SELECT MAX(StatusID) FROM PaperStatus) AND ijerts.papers.UserId = ?UserId";


            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.Add(new MySqlParameter("?UserID", userId));

            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = queryPaper;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Paper objPaper = new Paper();
                    PaperStatus status = new PaperStatus();
                    objPaper.PaperId = Convert.ToInt32(reader["PaperId"]);
                    objPaper.MainTitle = reader["MainTitle"].ToString();
                    objPaper.ShortDesc = reader["ShortDesc"].ToString();
                    objPaper.CreatedBy = reader["CreatedBy"].ToString();
                    objPaper.CreatedDateTime = Convert.ToDateTime(reader["CreatedDateTime"].ToString());

                    status.PaperId = objPaper.PaperId;
                    status.StatusID = Convert.ToInt32(reader["StatusId"].ToString());
                    status.Status = reader["Status"].ToString();

                    objPaper.Status = status;

                    papers.Add(objPaper);
                }

                return papers;
            }

        }

        public Paper GetPaperDetails(int id)
        {
            Paper paper = new Paper();
            List<PaperAuthors> lstPaperAuth = new List<PaperAuthors>();
            string queryPaper = "SELECT PAP.PaperId, MainTitle, ShortDesc, Subject, Tags,CreatedBy, CreatedDateTime,  CompleteFilePath, FileName, "
                                + " AuthorFirstName, AuthorLastName, AuthorDepartment, AuthorOrganisation, AuthorSubject from ijerts.Papers PAP "
                                + " INNER JOIN ijerts.authors AUT ON "
                                + " PAP.PaperId = AUT.PaperID "
                                + " WHERE PAP.PaperId = ?PaperId ";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.Add(new MySqlParameter("?PaperId", id));

            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = queryPaper;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PaperAuthors auth = new PaperAuthors();
                    paper.PaperId = Convert.ToInt32(reader["PaperId"]);
                    paper.MainTitle = reader["MainTitle"].ToString();
                    paper.ShortDesc = reader["ShortDesc"].ToString();
                    paper.CreatedBy = reader["CreatedBy"].ToString();
                    paper.Subject = Convert.ToString(reader["Subject"]);
                    paper.Tags = Convert.ToString(reader["Tags"]);
                    paper.PaperPath = Convert.ToString(reader["CompleteFilePath"]);
                    paper.FileName = Convert.ToString(reader["FileName"]);
                    paper.PaperPath = string.Format("{0}\\{1}", paper.PaperPath, paper.FileName);
                    paper.CreatedDateTime = Convert.ToDateTime(reader["CreatedDateTime"].ToString());

                    auth.AuthorFirstName = Convert.ToString(reader["AuthorFirstName"]);
                    auth.AuthorLastName = Convert.ToString(reader["AuthorLastName"]);
                    auth.Department = Convert.ToString(reader["AuthorDepartment"]);
                    auth.Organisation = Convert.ToString(reader["AuthorOrganisation"]);
                    auth.Subject = Convert.ToString(reader["AuthorSubject"]);

                    lstPaperAuth.Add(auth);
                }

                paper.Authors = lstPaperAuth;
            }

            return paper;
        }

        public List<Specialisation> GetSpecialisation()
        {
            string queryPaper = "SELECT * FROM ijerts.specialisation where IsActive = 1";
            List<Specialisation> specialisationList = new List<Specialisation>();

            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = queryPaper;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Specialisation objSpecialiation = new Specialisation();
                    objSpecialiation.specialisationId = Convert.ToInt32(reader["specialisationId"]);
                    objSpecialiation.specialisation = reader["specialisation"].ToString();
                    objSpecialiation.IsActive = Convert.ToInt32(reader["IsActive"]);
                    specialisationList.Add(objSpecialiation);
                }
            }

            return specialisationList;
        }
    }
}