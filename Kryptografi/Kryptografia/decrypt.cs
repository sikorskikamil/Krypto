using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Kryptografia
{
    public class Decrypt
    {
        byte[] key;
        byte[] iv;
        byte[] input;
        int length;
        int level;
        int nThread;
        AesManaged myAes;
        StreamWriter File;
        StreamWriter File2;
        public static byte[] GetByteArrayFromStringHex(string HexString)
        {
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            return bytes;
        }

        public static string getHexString(int value)
        {
            if (value > 9)
            {
                return Char.ToString((char)('a'+value-10));
            }
            else
            {
                return value.ToString();
            }
        }
        public static string GetStringHexFromByteArray(byte[] bytearray)
        {
            string temp = string.Empty;
            for (int i = 0; i < bytearray.Length; ++i)
            {
                temp += getHexString(bytearray[i] / 16) + getHexString(bytearray[i] % 16);
            }
            return temp;
        }


        public Decrypt(string key, string iv, int len, int level, int thread, string mess)
        {
            this.length = len;
            this.level = level;
            this.key = GetByteArrayFromStringHex(key);
            this.iv = GetByteArrayFromStringHex(iv);
            this.input = Convert.FromBase64String(mess);
            myAes = new AesManaged();
            myAes.KeySize = 256;
            myAes.BlockSize = 128;
            myAes.Padding = PaddingMode.PKCS7;
            myAes.Mode = CipherMode.CBC;
            File = new StreamWriter("plik" + level + ".txt");
            File2 = new StreamWriter("wszystko" + level + ".txt");

        }

        public void Run()
        {
            File.WriteLine("test");
            File.Flush();
            for (long a0 = level * 32; a0 < (level + 1) * 32; ++a0)
            {
                Console.WriteLine(a0);
                for (int a1 = 0; a1 < 256; ++a1)
                {
                    var start = DateTime.Now;
                    for (int a2 = 0; a2 < 256; ++a2)
                    {
                        for (int a3 = 0; a3 < 256; ++a3)
                        {
                            key[0] = (byte)a0;
                            key[1] = (byte)a1;
                            key[2] = (byte)a2;
                            key[3] = (byte)a3;
                            try
                            {
                                var decrypt = myAes.CreateDecryptor(key, iv);
                                var ha = decrypt.TransformFinalBlock(input, 0, input.Length);
                                var bytearr = decrypt.TransformFinalBlock(input, input.Length, input.Length);
                                Console.WriteLine("ha;");
                                var mes = Encoding.UTF8.GetString(bytearr);
                                if (mes.All(x => (x >= 32 && x <= 125) || (x >= 260 && x <= 263) || (x >= 280 && x <= 282) || (x >= 321 && x <= 322) || (x == 211) || x == 243 || x == 346 || x == 347 || x == 211 || x == 246 || x == 379 || x == 380 || x == 377 || x == 378 || x == 324 || x == 325))
                                {
                                    Console.WriteLine(mes);
                                    File.WriteLine(mes);
                                    File.WriteLine("Key: " + GetStringHexFromByteArray(key));
                                    File.WriteLine();
                                    File.Flush();
                                }
                                File2.WriteLine(mes);
                                File2.WriteLine("Key: " + GetStringHexFromByteArray(key));
                                File.WriteLine();
                            }
                            catch { }


                        }

                    }
                    Console.WriteLine(DateTime.Now - start);
                }
            }
            File.Close();
            File2.Close();
            Console.WriteLine("koniec " + level);
        }
    }
}
