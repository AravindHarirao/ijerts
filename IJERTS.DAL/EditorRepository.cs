using IJERTS.Objects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.DAL
{
    public class EditorRepository : IEditorRepository
    {

        public void Register(Users user)
        {
            string query = "INSERT  INTO users (FirstName, LastName, Email, Password, Phone, Organisation, Qualification, Position, SpecializationGroupId, SpecializationId, UserType, UserActivationValue, IsActive, CreatedDateTime, CreatedBy, UpdatedDatetime, UpdatedBy) "
                                + " Values "
                                + " (?FirstName, ?LastName, ?Email, ?Password, ?Phone, ?Organisation, ?Qualification, ?Position, ?SpecializationGroupId, ?SpecializationId, ?UserType, ?UserActivationValue, 1, Now(), 'System', Now(), 'System')";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?FirstName", user.FirstName));
                cmd.Parameters.Add(new MySqlParameter("?LastName", user.LastName));
                cmd.Parameters.Add(new MySqlParameter("?Email", user.Email));
                cmd.Parameters.Add(new MySqlParameter("?Password", user.Password));
                cmd.Parameters.Add(new MySqlParameter("?Phone", user.Phone));
                cmd.Parameters.Add(new MySqlParameter("?Organisation", user.Organisation));
                cmd.Parameters.Add(new MySqlParameter("?Qualification", user.Qualification));
                cmd.Parameters.Add(new MySqlParameter("?Position", user.Position));
                cmd.Parameters.Add(new MySqlParameter("?SpecializationGroupId", (int)CommonDataGroup.SPECIALIZATION));
                cmd.Parameters.Add(new MySqlParameter("?SpecializationId", user.SpecializationId));
                cmd.Parameters.Add(new MySqlParameter("?UserType", "E"));
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

        public List<Users> GetAllUsers(Users users)
        {
            return null;
        }

        public List<Tuple<int, string>> GetSpecialization(CommonCode commonCode)
        {
            List<Tuple<int, string>> lstSpecialization = new List<Tuple<int, string>>();
            string sqlQuery = "SELECT * FROM CommonCode WHERE IsActive = 1 AND GroupId = ?GroupId ORDER BY SortOrder";
            commonCode.GroupId = 2;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.Add(new MySqlParameter("?GroupId", commonCode.GroupId));
            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = sqlQuery;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    commonCode.Id = Convert.ToInt32(reader.GetString("Id"));
                    commonCode.GroupId = Convert.ToInt32(reader.GetString("GroupId"));
                    commonCode.GroupName = reader.GetString("GroupName");
                    commonCode.CodeName = reader.GetString("CodeName");
                    commonCode.SortOrder = Convert.ToInt32(reader.GetString("SortOrder"));
                    commonCode.IsActive = Convert.ToInt32(reader.GetString("IsActive"));
                    lstSpecialization.Add(new Tuple<int, string>(commonCode.Id, commonCode.CodeName));
                }
            }
            return lstSpecialization;
        }

        public List<Paper> GetAllPapers()
        {
            List<Paper> lstPaperCollection = new List<Paper>();

            string queryPaper = "select PAP.PaperId, MainTitle, ShortDesc, CreatedBy, CreatedDateTime from ijerts.Papers PAP "
                                    + "INNER JOIN ijerts.authors AUT ON "
                                    + "PAP.PaperId = AUT.PaperID";
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = queryPaper;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Paper objPaper = new Paper();
                    objPaper.PaperId = Convert.ToInt32(reader["PaperId"]);
                    objPaper.MainTitle = reader["MainTitle"].ToString();
                    objPaper.ShortDesc = reader["ShortDesc"].ToString();
                    objPaper.CreatedBy = reader["CreatedBy"].ToString();
                    objPaper.CreatedDateTime = Convert.ToDateTime(reader["CreatedDateTime"].ToString());
                    lstPaperCollection.Add(objPaper);
                }

                return lstPaperCollection;

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
    }
}
