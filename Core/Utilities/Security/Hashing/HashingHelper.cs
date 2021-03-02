using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    //Hashing-Salting => Kullanıcının girdiği bir parolayı şifreleme algoritması ile geridönüşü olmayacak şekilde yeni veri oluşturma işlemi.
    
    public class HashingHelper
    {
        //Verdiğimiz passwordun hash'ini oluşturacak.Aynı zamanda salt'ı oluşturacak. 
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        //Sonradan sisteme girmek isteyen kullanıcının verdiği şifrenin bizim veritabanımızdaki hash ile eşleşip eşleşmediğini kontrol ediyoruz.
        public static bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
