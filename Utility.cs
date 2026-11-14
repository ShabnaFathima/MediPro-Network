using System.Security.Cryptography;
using System.Text;

namespace MediProApi
{ 

    public class Utility
    {
        public static string LicenceName = "";
        public static string LicenceValue = "";
        public static string LicenceError = "Licence expired";
        public static string Baseurl = "";


        private const string initVector = "pemgail9uzpgzl88";
        private static string pass = "MediProShabna"; //Properties.Settings.Default.salt;
                                             // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        public static void WriteLog(string sErrMsg)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory;
            if (!System.IO.Directory.Exists(filepath + @"\Logs"))
            {
                System.IO.Directory.CreateDirectory(filepath + @"\Logs");
            }

            using (TextWriter tw = new StreamWriter(filepath + @"Logs\Log-" + DateTime.Now.ToString("ddMMMMyyyy") + ".txt", true))
            {
                tw.WriteLine(DateTime.Now + " " + sErrMsg);
            }
            Console.WriteLine(sErrMsg);
        }

        public static void WriteErrorLog(string sErrMsg)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory;
            if (!System.IO.Directory.Exists(filepath + @"\ErrorLogs"))
            {
                System.IO.Directory.CreateDirectory(filepath + @"\ErrorLogs");
            }

            using TextWriter tw = new StreamWriter(filepath + @"ErrorLogs\ErroLog-" + DateTime.Now.ToString("ddMMMMyyyy") + ".txt", true);
            tw.WriteLine(DateTime.Now + " " + sErrMsg);
        }

        public static bool IsLicenseValidated()
        {
            bool result = true;
            try
            {
                if (LicenceName.Trim() == "" || LicenceValue.Trim() == "") { return false; }
                string[] words;
                string DecryptKey = DecryptString(LicenceValue);
                if (DecryptKey.Contains("-"))
                {
                    words = DecryptKey.Split('-');
                    if (words[0] == "Free" && words[2] == LicenceName)
                    {
                        DateTime dt = DateTime.Parse(words[1].ToString());
                        if (dt >= DateTime.Today)
                        {
                            result = true;
                        }
                    }
                }
                else if (DecryptKey == LicenceName)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                WriteErrorLog("License validation : " + ex.ToString());
            }
            return result;
        }

        public static string DecryptString(string cipherText)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(pass, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }


}
