using System.Security.Cryptography;
using System.Text;

namespace Models
{
    class PasswordEncryption
    {
        public string GetHashString(string s)
        {
            if (s != null)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(s);

                //создаем объект для получения средст шифрования
                MD5CryptoServiceProvider CSP =
                    new MD5CryptoServiceProvider();

                //вычисляем хеш-представление в байтах
                byte[] byteHash = CSP.ComputeHash(bytes);

                string hash = string.Empty;

                //формируем одну цельную строку из массива
                foreach (byte b in byteHash)
                    hash += string.Format("{0:x2}", b);

                return hash;
            }
            else
                return null;
        }
    }
}