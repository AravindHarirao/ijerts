using IJERTS.Objects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.DAL
{
    public class ReviewRepository : IReviewRepository
    {
        public Users Register(Users user)
        {
            Int32 iUserCount = 0;            
            try
            {
                //Check if Reviewer already exists
                string qryCheck = "SELECT COUNT(*) FROM `Users` WHERE Email = ?Email AND UserType = ?UserType ";
                using (MySqlConnection conCheck = new MySqlConnection(DBConnection.ConnectionString))
                {
                    MySqlCommand cmdCheck = new MySqlCommand();
                    cmdCheck.Parameters.Add(new MySqlParameter("?Email", user.Email));
                    cmdCheck.Parameters.Add(new MySqlParameter("?UserType", user.UserType));

                    cmdCheck.Connection = conCheck;
                    conCheck.Open();
                    cmdCheck.CommandText = qryCheck;
                    iUserCount = (Int32)(long)(cmdCheck.ExecuteScalar());
                    cmdCheck.Dispose();
                    conCheck.Close();
                }

                if (iUserCount == 0)
                {
                    string query = "INSERT  INTO users (FirstName, LastName, Email, Password, Phone, Organisation, Qualification, Position, Department, SpecializationId, UserType, ResumeFileName, IsActive, CreatedDateTime, CreatedBy, UpdatedDatetime, UpdatedBy) "
                                    + " Values "
                                    + " (?FirstName, ?LastName, ?Email, ?Password, ?Phone, ?Organisation, ?Qualification, ?Position, ?Department, ?SpecializationId, ?UserType, ?ResumeFileName, 1, Now(), 'System', Now(), 'System')";

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Parameters.Add(new MySqlParameter("?FirstName", user.FirstName));
                    cmd.Parameters.Add(new MySqlParameter("?LastName", user.LastName));
                    cmd.Parameters.Add(new MySqlParameter("?Email", user.Email));
                    cmd.Parameters.Add(new MySqlParameter("?Password", user.Password));
                    cmd.Parameters.Add(new MySqlParameter("?Phone", user.Phone));
                    cmd.Parameters.Add(new MySqlParameter("?Organisation", user.Organisation));
                    cmd.Parameters.Add(new MySqlParameter("?Qualification", user.Qualification));
                    cmd.Parameters.Add(new MySqlParameter("?Position", user.Position));
                    cmd.Parameters.Add(new MySqlParameter("?Department", user.Department));
                    cmd.Parameters.Add(new MySqlParameter("?SpecializationId", user.SpecializationId));
                    cmd.Parameters.Add(new MySqlParameter("?UserType", user.UserType));
                    cmd.Parameters.Add(new MySqlParameter("?UserActivationValue", user.UserActivationValue));
                    cmd.Parameters.Add(new MySqlParameter("?ResumeFileName", user.ResumeFileName));

                    using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();
                        con.Close();

                        user.ResultCode = 1;
                        user.ResultMessage = "SUCCESS";
                    }
                }
                else
                {
                    user.ResultCode = -1;
                    user.ResultMessage = "EXISTS";
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tuple<int, string>> GetSpecialization(CommonCode commonCode)
        {
            List<Tuple<int, string>> lstSpecialization = new List<Tuple<int, string>>();
            string sqlQuery = "SELECT * FROM specialisation WHERE IsActive = 1 ORDER BY specialisation";
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = sqlQuery;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    commonCode.Id = Convert.ToInt32(reader.GetString("specialisationId"));
                    commonCode.CodeName = reader.GetString("specialisation");
                    commonCode.IsActive = Convert.ToInt32(reader.GetString("IsActive"));
                    lstSpecialization.Add(new Tuple<int, string>(commonCode.Id, commonCode.CodeName));
                }
            }
            return lstSpecialization;
        }

        public List<Users> GetAllReviewers()
        {
            List<Users> lstReviewers = new List<Users>();

            string queryPaper = "select us.UserId, us.FirstName, us.LastName, us.Email, us.Phone, us.Organisation, us.Qualification, "
                                + " us.Position, us.Department, us.UserActivated, us.UserActivationValue, sp.specialisation "
                                + " from Users us INNER JOIN specialisation sp on sp.specialisationId = us.SpecializationId "
                                + " WHERE UserType = 'R' ";
            //+ " WHERE UserType = 'R' AND (us.UserActivationValue is null OR us.UserActivationValue != 'False') ";

            //string queryPaper = "select us.UserId, us.FirstName, us.LastName, us.Email, us.Phone, us.Organisation, us.Qualification, "
            //                            + " us.Position, us.Department, us.UserActivated, sp.specialisation "
            //                            + "from Users us INNER JOIN specialisation sp on sp.specialisationId = us.SpecializationId "
            //                            + "inner join papers PAP on pap.subject = sp.specialisation "
            //                            + "WHERE UserActivated = 1 AND UserType = 'R' AND(us.UserActivationValue is null OR us.UserActivationValue != 'False') "
            //                            + "AND PAP.PaperId = ?paperId";



            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = queryPaper;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Users objUsers = new Users();
                    objUsers.UserId = Convert.ToInt32(reader["UserId"]);
                    objUsers.FirstName = reader["FirstName"].ToString();
                    objUsers.LastName = reader["LastName"].ToString();
                    objUsers.Email = reader["Email"].ToString();
                    objUsers.Phone = reader["Phone"].ToString();
                    objUsers.Organisation = reader["Organisation"].ToString();
                    objUsers.Qualification = reader["Qualification"].ToString();
                    objUsers.Position = reader["Position"].ToString();
                    objUsers.Department = reader["Department"].ToString();
                    objUsers.UserActivationValue = reader["UserActivationValue"].ToString();
                    objUsers.Specialisation = reader["specialisation"].ToString();
                    lstReviewers.Add(objUsers);
                }

                return lstReviewers;

            }
        }

        public List<Users> GetAllReviewersForPaper(int paperId)
        {
            List<Users> lstReviewers = new List<Users>();

            string queryPaper = "select us.UserId, us.FirstName, us.LastName, us.Email, us.Phone, us.Organisation, us.Qualification, "
                                         + " us.Position, us.Department, us.UserActivated, us.UserActivationValue, sp.specialisation "
                                         + "from Users us INNER JOIN specialisation sp on sp.specialisationId = us.SpecializationId "
                                         + "inner join papers PAP on pap.subject = sp.specialisation "
                                         + "WHERE UserActivated = 1 AND UserType = 'R' "
                                         //+ "WHERE UserActivated = 1 AND UserType = 'R' AND(us.UserActivationValue is null OR us.UserActivationValue != 'False') "
                                         + "AND PAP.PaperId = ?paperId";
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.Add(new MySqlParameter("?PaperId", paperId));

            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = queryPaper;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Users objUsers = new Users();
                    objUsers.UserId = Convert.ToInt32(reader["UserId"]);
                    objUsers.FirstName = reader["FirstName"].ToString();
                    objUsers.LastName = reader["LastName"].ToString();
                    objUsers.Email = reader["Email"].ToString();
                    objUsers.Phone = reader["Phone"].ToString();
                    objUsers.Organisation = reader["Organisation"].ToString();
                    objUsers.Qualification = reader["Qualification"].ToString();
                    objUsers.Position = reader["Position"].ToString();
                    objUsers.Department = reader["Department"].ToString();
                    objUsers.UserActivationValue = reader["UserActivationValue"].ToString();
                    objUsers.Specialisation = reader["specialisation"].ToString();
                    lstReviewers.Add(objUsers);
                }

                return lstReviewers;

            }
        }


        public void ActivateDeActivateReviewer(Users user)
        {
            string query = "UPDATE Users SET Password = ?Password, UserActivated = ?UserActivated, UserActivationValue = ?UserActivationValue, "
                + " UpdatedDatetime = NOW(), UpdatedBy = ?UpdatedBy "
                + " WHERE UserId = ?UserId";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?UserId", user.UserId));
                cmd.Parameters.Add(new MySqlParameter("?Password", user.Password));
                cmd.Parameters.Add(new MySqlParameter("?UserActivated", user.UserActivated));
                cmd.Parameters.Add(new MySqlParameter("?UserActivationValue", user.UserActivationValue));
                cmd.Parameters.Add(new MySqlParameter("?UpdatedBy", user.UpdatedBy));
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
        
        public Users GetReviewerDetails(Int32 userId)
        {
            Users objUsers = new Users();
            string queryUser = "SELECT us.UserId, us.FirstName, us.LastName, us.Email, us.Phone, us.Organisation, us.Qualification, "
                                + " us.Position, us.Department, us.UserActivated, us.UserActivationValue, us.ResumeFileName, us.CreatedDateTime, us.CreatedBy, "
                                + " us.UpdatedDatetime, us.UpdatedBy, sp.specialisation "
                                + " FROM Users us INNER JOIN specialisation sp on sp.specialisationId = us.SpecializationId "
                                + " WHERE us.UserId = ?UserId ";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.Add(new MySqlParameter("?UserId", userId));

            using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = queryUser;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    objUsers.UserId = Convert.ToInt32(reader["UserId"]);
                    objUsers.FirstName = reader["FirstName"].ToString();
                    objUsers.LastName = reader["LastName"].ToString();
                    objUsers.Email = reader["Email"].ToString();
                    objUsers.Phone = reader["Phone"].ToString();
                    objUsers.Organisation = reader["Organisation"].ToString();
                    objUsers.Qualification = reader["Qualification"].ToString();
                    objUsers.Position = reader["Position"].ToString();
                    objUsers.Department = reader["Department"].ToString();
                    objUsers.UserActivationValue = reader["UserActivationValue"].ToString();                    
                    objUsers.ResumeFileName = reader["ResumeFileName"].ToString();
                    objUsers.CreatedDateTime = Convert.ToDateTime(reader["CreatedDateTime"]);
                    objUsers.CreatedBy = reader["CreatedBy"].ToString();
                    objUsers.UpdatedDatetime = Convert.ToDateTime(reader["UpdatedDatetime"]);
                    objUsers.UpdatedBy = reader["UpdatedBy"].ToString();
                    objUsers.Specialisation = reader["specialisation"].ToString();
                }
            }
            return objUsers;
        }

        public List<Paper> GetAssignedPaper(int UserId)
        {
            List<Paper> lstPaperCollection = new List<Paper>();

            //string queryPaper = "select PAP.PaperId, MainTitle, ShortDesc, CreatedBy, CreatedDateTime from Papers PAP "
            //                        + "INNER JOIN authors AUT ON "
            //                        + "PAP.PaperId = AUT.PaperID";
            string queryPaper = "SELECT * FROM `ijerts`.`papers` PAP INNER JOIN papersapprovers APP" +
                                    " ON PAP.PaperID = APP.PaperId " +
                                    " WHERE ApproverID = ?UserId ";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.Add(new MySqlParameter("?UserId", UserId));


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

        public int UpdatePaperStatus(int userId, int paperId, string Comments, string Approve)
        {
            int result = 0;

            if (!string.IsNullOrEmpty(Approve))
            {
                string queryPaperStatus = "UPDATE `ijerts`.paperStatus SET `status` = ?Approve WHERE paperID = ?PaperID";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?PaperId", paperId));
                cmd.Parameters.Add(new MySqlParameter("?Approve", Approve));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryPaperStatus;
                    cmd.ExecuteNonQuery();
                }

            }
            if (!string.IsNullOrEmpty(Comments.Trim()))
            {
                string queryPaper = " INSERT INTO `ijerts`.papercomments(PaperId, Comments, IsEditorComments, IsActive, CreatedBy, CreatedDateTime) "
                                + " VALUES(?paperId, ?comments, 1, 1, ?userId, now()); ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.Add(new MySqlParameter("?UserId", userId));
                cmd.Parameters.Add(new MySqlParameter("?PaperId", paperId));
                cmd.Parameters.Add(new MySqlParameter("?comments", Comments));

                using (MySqlConnection con = new MySqlConnection(DBConnection.ConnectionString))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = queryPaper;
                    cmd.ExecuteNonQuery();
                }

            }

            return result;

        }

    }
}
