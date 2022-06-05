using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebAppBlazorLogin.Shared
{
    public class UserDetailDto
    {
        public string UserName { get; set; }
        public byte[] Password { get; set; }

        public static UserDetailDto FromLogin(string userName = "", string password = "")
        {
            // Hash password before transfer
            var hashPassword = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(password));

            return new UserDetailDto
            {
                UserName = userName,
                Password = hashPassword
            };
        }
    }
}
