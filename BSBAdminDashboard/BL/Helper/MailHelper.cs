using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BSBAdminDashboard.BL.Helper
{
    public static class MailHelper
    {
        public static string sendMail(string Title, string Message)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //SmtpClient smtp = new SmtpClient("smtp.mail.yahoo.com", 465);

                smtp.EnableSsl = true;


                smtp.Credentials = new NetworkCredential("alagamymahmoud2@gmail.com", "alagamymahmoud11219951");
                smtp.Send("alagamymahmoud2@gmail.com", "mahmoudalagamy846@yahoo.com", Title, Message);

                return "Mail Sent Succesfully";

            }
            catch (Exception ex)
            {

                return "Send Mail Faild";

            }
        }
    }
}
