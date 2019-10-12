using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;

namespace LibraryServices
{
    public class BusinessLogic
    {

        private  const string setchars ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@^_`.?!";
        
        public static string CryptographyMD5(string input) {

            StringBuilder strb = new StringBuilder();
            using (var md5 = MD5.Create()) {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (var elem in data) {
                    strb.Append(elem.ToString("x2"));
                }
            }
            
            return strb.ToString();
        }
        

        public static void Notify(Person user) {
           MailAddress addresser = new MailAddress("yehor.shamrai@nure.ua", "MusicMood");
            MailAddress addressee = new  MailAddress(user.Email, user.FirstName);
            using (MailMessage mailMessage = new MailMessage(addresser, addressee))
            using (SmtpClient smtpClient = new SmtpClient()) 
            {
                mailMessage.Subject = "Ввостановление пароля";
                mailMessage.Body = $"{user.FirstName}, ваш новый пароль:{user.Password}";
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(addresser.Address,"mustangPegas065@");
                smtpClient.Send(mailMessage);
            }
            
        }
        public static string GenerateStrongPassword() {
           
            StringBuilder strb =  new StringBuilder();
            Random rd = new Random();
            int length = rd.Next(15, 50);
            for (int i = 0; i < length; i++) {
                strb.Append(setchars[rd.Next(0,setchars.Length)]);
            }
            return strb.ToString();
        }
    }
}
