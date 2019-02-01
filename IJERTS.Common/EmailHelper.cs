using IJERTS.Objects;
using System;
using System.Net.Mail;
using System.Text;

namespace IJERTS.Common
{
    public static class EmailHelper
    {
        static string sFromEmailAddress = "ijerts@gmail.com";
        static string sSmtpHost = "smtp.gmail.com";
        static string sSmtpPort = "587";
        static string sSmtpUsername = "ijerts@gmail.com";
        static string sSmtpPassword = "ijerts@123";

        public static string SendWelcomeEmailtoUser(Users user)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress(sFromEmailAddress);
                mail.Subject = "New Editor Registration - Activation";

                StringBuilder sbEmailMsg = new StringBuilder();
                sbEmailMsg.Append("<table>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("Dear " + user.FirstName + " " + user.LastName + ",");
                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("Thank you for signing up with us.");

                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("To verify your account with us, Please click on the activation link given below to activate your account");

                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("<span style = 'background:#eaeae1;padding:15px;font-size:15px;font-family:verdana, tahoma, Arial, Helvetica, sans-serif;color:#000;font-weight:bold;border-radius:15px;'>");

                sbEmailMsg.Append("<a rel='nofollow' target='_blank' href='https://www.ijerts.org/Login/AuthenticateActivation?Id=" + user.UserActivationValue + "' style='color:#000;line-height:50px;'>Click here to activate your account</a>");

                sbEmailMsg.Append("</span>");

                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("Your account has been setup in IJERTS application and you can login using the following details after you have activated your account with us.");

                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Login Url");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append("<a rel='nofollow' target='_blank' href='https://www.ijerts.org/Login/Authentication'>https://www.ijerts.org/Login/Authentication</a>");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Username");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append(user.Email);
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Password");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append(IJERTSEncryptioncs.Decrypt(user.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey));
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("Once you have logged in, please ensure that you visit the Change Password screen and change your password.");

                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("Thank You,");
                sbEmailMsg.Append("</td></tr>");
                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("IJERTS Admin Team.");
                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("Please note this is a system generated message and reply to the above email address is not monitored. For any queries, please contact our Admin team. ");
                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("</table>");

                mail.Body = sbEmailMsg.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = sSmtpHost;
                smtp.Port = Convert.ToInt32(sSmtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(sSmtpUsername, sSmtpPassword); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static string SendWelcomeEmailtoReviewer(Users user)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress(sFromEmailAddress);
                mail.Subject = "Reviewer Activation";

                StringBuilder sbEmailMsg = new StringBuilder();
                sbEmailMsg.Append("<table>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("Dear " + user.FirstName + " " + user.LastName + ",");
                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("Thank you for signing up with us.");

                sbEmailMsg.Append("</td></tr>");

                
                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("Your account has been setup in IJERTS application and you can login using the following details.");

                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Login Url");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append("<a rel='nofollow' target='_blank' href='https://www.ijerts.org/Login/ReviewerLogin'>https://www.ijerts.org/Login/ReviewerLogin</a>");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Username");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append(user.Email);
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Password");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append(IJERTSEncryptioncs.Decrypt(user.Password, CommonHelper.SaltPassword, CommonHelper.EncryptKey));
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");

                sbEmailMsg.Append("Once you have logged in, please ensure that you visit the Change Password screen and change your password.");

                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("Thank You,");
                sbEmailMsg.Append("</td></tr>");
                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("IJERTS Admin Team.");
                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr><td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' colspan='3' align='left'>");
                sbEmailMsg.Append("Please note this is a system generated message and reply to the above email address is not monitored. For any queries, please contact our Admin team. ");
                sbEmailMsg.Append("</td></tr>");

                sbEmailMsg.Append("</table>");

                mail.Body = sbEmailMsg.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = sSmtpHost;
                smtp.Port = Convert.ToInt32(sSmtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(sSmtpUsername, sSmtpPassword); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static string SendContactUsEmail(ContactUs contactUs)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("support@ijerts.org");
                //mail.To.Add("senthilavs@gmail.com");
                mail.From = new MailAddress(sFromEmailAddress);
                mail.Subject = "Enquiry from ContactUs page";

                StringBuilder sbEmailMsg = new StringBuilder();
                sbEmailMsg.Append("<table>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Full Name");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append(contactUs.FullName);
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Email");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append(contactUs.EmailAddress);
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("<tr>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='15%' align='left'>");
                sbEmailMsg.Append("Subject");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:bold; color: #06489c;' width='2%' align='center'>");
                sbEmailMsg.Append(" : ");
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("<td style='family:verdana, tahoma, Arial, Helvetica, sans-serif; font-size:15px; font-weight:normal; color: #06489c;' align='left'>");
                sbEmailMsg.Append(contactUs.EmailMessage);
                sbEmailMsg.Append("</td>");
                sbEmailMsg.Append("</tr>");

                sbEmailMsg.Append("<tr><td colspan='3'><br></td></tr>");

                sbEmailMsg.Append("</table>");

                mail.Body = sbEmailMsg.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = sSmtpHost;
                smtp.Port = Convert.ToInt32(sSmtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(sSmtpUsername, sSmtpPassword); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return "Success";
            }
            catch (Exception)
            {
                return "Error"; //ex.Message.ToString();
            }
        }

        public static string AnswerQuery(Queries Query)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(Query.Email);
                //mail.To.Add("senthilavs@gmail.com");
                mail.From = new MailAddress(sFromEmailAddress);
                mail.Subject = "Regd your recent query.";

                StringBuilder sbEmailMsg = new StringBuilder();
                sbEmailMsg.Append("<p>");
                sbEmailMsg.Append("Thank you for contacting us. Please find our response below.");
                sbEmailMsg.Append("</p>");

                sbEmailMsg.Append("<br><p>");
                sbEmailMsg.Append(Query.QueryAnswer);
                sbEmailMsg.Append("</p>");


                mail.Body = sbEmailMsg.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = sSmtpHost;
                smtp.Port = Convert.ToInt32(sSmtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(sSmtpUsername, sSmtpPassword); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return "Success";
            }
            catch (Exception)
            {
                return "Error"; //ex.Message.ToString();
            }
        }
    }
}
