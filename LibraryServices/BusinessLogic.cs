using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.IO;


namespace LibraryServices
{
    public class BusinessLogic
    {

        public static string CryptographyMD5(string input) {

            var strb = new StringBuilder();
            using (var md5 = MD5.Create()) {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (var elem in data) {
                    strb.Append(elem.ToString("x2"));
                }
            }
            
            return strb.ToString();
        }

        public static void Notify(Person user, string newPassword) {
           var addresser = new MailAddress("alex.23.kovalov@gmail.com", "MusicMood");
            var addressee = new  MailAddress(user.Email, user.FirstName);
            using (MailMessage mailMessage = new MailMessage(addresser, addressee))
            using (SmtpClient smtpClient = new SmtpClient()) 
            {
                mailMessage.Subject = "Ввостановление пароля";
                mailMessage.Body = $"{user.FirstName}, ваш новый пароль:{newPassword}";
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(addresser.Address,"rjdfktd_1991");
                smtpClient.Send(mailMessage);
            }
            
        }

        public static void SaveInRootFolder(HttpPostedFileBase fileObj, string fileDirect) {
            string path = HttpContext.Current.Server.MapPath($"~/App_Data/{fileDirect}");
            string fileName = Path.GetFileName(fileObj.FileName);
            string fullPath = Path.Combine(path, fileName);
            fileObj.SaveAs(fullPath);

        }
        public static string GenerateStrongPassword() {
            const string setchars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@^_`.?!";
            StringBuilder strb =  new StringBuilder();
            Random rd = new Random();
            int length = rd.Next(15, 50);

            for (int i = 0; i < length; i++) {
                strb.Append(setchars[rd.Next(0,setchars.Length)]);
            }
            
            return strb.ToString();
        }
        private enum Geners { Ambient, Classical, Country, DeepHouse, Disco, Dubstep, Jazz };

        public static string[] FetchAllGeners() {
            return Enum.GetNames(typeof(Geners));
        }


    }
}
