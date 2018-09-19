using MyAwesomeDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyAwesomeDiary.ViewModel
{
    public class PasswordSecurityProvider
    {
        public string PlainPassword { get; set; }
        private byte[] Salt { get; set; }
        public PasswordSecurityProvider()
        {
            Salt = new byte[16];
        }
        private byte[] HashPass(byte[] salt, string pass) => new Rfc2898DeriveBytes(pass, salt, 1000).GetBytes(20);

        public string GetHashPassword(string password)
        {
            new RNGCryptoServiceProvider().GetBytes(Salt);
            byte[] hash = HashPass(Salt, password);
            byte[] HashedPassword = new byte[36];
            Array.Copy(Salt, 0, HashedPassword, 0, 16);
            Array.Copy(hash, 0, HashedPassword, 16, 20);
            return Convert.ToBase64String(HashedPassword);
        }
        public static User Find(string id)
        {
            using (var db = new MyContext())
            {
                var exist = from u in db.Users
                            where u.UserID == id
                            select u;
                try
                {
                    var result = exist.Single();
                    return result;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        public bool Validate(string id, string password)
        {
            User userMain = Find(id);
            if (userMain != null)
            {
                string rPW = userMain.Password;
                byte[] hashByte = Convert.FromBase64String(rPW);
                Array.Copy(hashByte, 0, Salt, 0, 16);

                byte[] hash = HashPass(Salt, password);

                for (int i = 0; i < 20; i++)        
                    if (hashByte[i + 16] != hash[i])
                        return false;
                return true;
            }
            return false;
        }
    }
}
