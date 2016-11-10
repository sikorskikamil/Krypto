using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace play.crypto
{
    public class Crypto
    {
        public static void EncryptFile(string inputFile, byte[] key, byte[] iv, CipherMode mode)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = mode;
                FileStream fsCrypt = new FileStream(inputFile + ".kamaz", FileMode.Create);
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write);
                FileStream fsIn = new FileStream(inputFile, FileMode.Open);
                int data;
                while ((data = fsIn.ReadByte()) != -1)
                {
                    cs.WriteByte((byte)data);
                }
                cs.Close();
                fsIn.Close();
                fsCrypt.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Blad pliku");
            }
        }
        public static void DecryptFile(string inputFile, byte[] key, byte[] iv, CipherMode mode)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = mode;
                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
                FileStream fsOut = new FileStream(inputFile.Substring(0,inputFile.LastIndexOf('.')), FileMode.Create);
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read);
                int data;
                while ((data = cs.ReadByte()) != -1)
                {
                    fsOut.WriteByte((byte)data);
                    fsCrypt.Flush();
                }
                fsOut.Close();
                fsCrypt.Close();
            }
            
            catch (Exception)
            {
                Console.WriteLine("Blad pliku");
            }
        }
        public static StreamReader GetDecryptStreamReader(string inputFile, byte[] key, byte[] iv, CipherMode mode)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = mode;
                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read);
                string str = new StreamReader(cs).ReadToEnd();
                byte[] byteArray = Encoding.UTF8.GetBytes(str);
                MemoryStream stream = new MemoryStream(byteArray);
                fsCrypt.Close();
                return new StreamReader(stream);
            }
            catch (Exception)
            {
                Console.WriteLine("Blad pliku");
            }
            return null;
        }
        public static MemoryStream GetDecryptMemoryStream(string inputFile, byte[] key, byte[] iv, CipherMode mode)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = mode;
                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                //CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read);
                //var strreader = new StreamReader(cs);
                //fsCrypt.Close();
                //byte[] byteArray = Encoding.ASCII.GetBytes(str);

                //return new MemoryStream(byteArray);

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    decryptor, CryptoStreamMode.Write);

                int data;
                while ((data = fsCrypt.ReadByte()) != -1)
                {
                    cryptoStream.WriteByte((byte)data);
                }
                cryptoStream.FlushFinalBlock();
                memoryStream.Position = 0;

                return memoryStream;
            }
            catch (Exception)
            {
                Console.WriteLine("Blad pliku");
            }
            return null;
        }


        public static void EncryptToFile(string outputFile, string mess, byte[] key, byte[] iv, CipherMode mode)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = mode;
                FileStream fsCrypt = new FileStream(outputFile, FileMode.Create);
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write);
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(mess);
                writer.Flush();
                stream.Position = 0;
                int data;
                while ((data = stream.ReadByte()) != -1)
                {
                    cs.WriteByte((byte)data);
                }
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Blad pliku");
            }
        }
    }
}
