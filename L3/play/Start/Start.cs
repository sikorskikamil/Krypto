using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace play.Start
{
    public class Start
    {
        string passfile = "files//pass.kamaz";
        string pass;
        byte[] key = crypto.CryptoHelper.GetByteArrayFromStringHex("b14f636c88ba3dad57d53a8fab09dd95512ce1b545bbb93362113487e73a13fe");
        byte[] iv = crypto.CryptoHelper.GetByteArrayFromStringHex("d9fe9356611591730627a97ea4107f85");
        int nKeys;
        List<Key.KeyIv> KeyIvList;
        
        public void InitStart()
        {
            crypto.Crypto.EncryptFile("files\\klucze", key, iv, CipherMode.CBC);
            if (File.Exists(passfile))
            {
                pass = crypto.Crypto.GetDecryptStreamReader(passfile, key, iv, System.Security.Cryptography.CipherMode.CBC).ReadLine();
                while (!IsCorrectPassword()) ;
            }
            else
            {
                Console.WriteLine("Podaj haslo:");
                pass = GetPassword();
                crypto.Crypto.EncryptToFile(passfile,pass, key, iv, System.Security.Cryptography.CipherMode.CBC);
            }
            do
            {
                string keystore = GetCorrectLocalization("keystora");
                KeyIvList = Key.KeyStore.GetKeyIvList(crypto.Crypto.GetDecryptStreamReader(keystore, key, iv, System.Security.Cryptography.CipherMode.CBC));
                nKeys = KeyIvList.Count;
            } while (nKeys == 0);
        }
        public void PlayerVoid()
        {
            Console.WriteLine("\nOdwarzacz mp3 z plikow AES");
            int nkey = GetNumberOfKey();
            CipherMode mode = GetCipherMode();
            string filetodecode = GetCorrectLocalization("pliku ktory chcesz odtwarzac");
            var stream = crypto.Crypto.GetDecryptMemoryStream(filetodecode, KeyIvList[nkey].Key, KeyIvList[nkey].Iv, mode);
            //var x = Player.Player.
            Thread play = new Thread(()=>Player.Player.PlayMp3FromUrl(stream));
            play.Start();
            Console.ReadLine();
            Player.Player.br = true;
            Console.WriteLine("koniec piosenki :D");
        }
        public void EncodeFiles()
        {
            Console.WriteLine("\nKodowanie plikow AES");
            int nkey = GetNumberOfKey();
            CipherMode mode = GetCipherMode();
            string filetoencode = GetCorrectLocalization("pliku ktory chcesz zaszyfrowac");
            crypto.Crypto.EncryptFile(filetoencode,KeyIvList[nkey].Key, KeyIvList[nkey].Iv, mode);
            Console.WriteLine("Zakodowano plik " + filetoencode);
        }
        public void DecodeFiles()
        {
            Console.WriteLine("\nRozkodowanie plikow AES");
            int nkey = GetNumberOfKey();
            CipherMode mode = GetCipherMode();
            string filetodecode = GetCorrectLocalization("pliku ktory chcesz rozszyfrowac");
            crypto.Crypto.DecryptFile(filetodecode, KeyIvList[nkey].Key, KeyIvList[nkey].Iv, mode);
            Console.WriteLine("Rozkodowano plik " + filetodecode);
        }
        public int GetNumberOfKey()
        {
            Console.WriteLine("Podaj numer klucza, maksymalna ilosc to " + nKeys);
            int n = GetNumber()-1;
            Console.WriteLine("Wybrales klucz " + (n % nKeys + 1));
            return n;
        }
        public int GetNumber()
        {
            int n;
            while (true)
            {
                Console.WriteLine("Podaj liczbe");
                try
                {
                    n = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception) { }
            }
            return n;
        }
        public bool IsCorrectPassword()
        {
            Console.WriteLine("Podaj swoje haslo:");
            string x = GetPassword();
            return x.Equals(pass);
        }
        public string GetCorrectLocalization(string info)
        {
            string dir;
            do
            {
                Console.WriteLine("Podaj poprawna sciezke do " + info + " z bierzacej lokalizacji(\\..\\) - cofa");
                dir = Directory.GetCurrentDirectory() + "\\" + Console.ReadLine();
            }
            while (!File.Exists(dir));
            return dir;
        }
        public CipherMode GetCipherMode()
        {
            Console.WriteLine("Podaj jaki tryb(numer):\n1. CBC\n2. CFB\n3. CTS\n4. ECB\n5. OFB");
            int n = (GetNumber() - 1) % 5 + 1;
            Console.WriteLine("Wybrales tryb nr " + n);
            switch (n)
            {
                case 1:
                    return CipherMode.CBC;
                case 2:
                    return CipherMode.CFB;
                case 3:
                    return CipherMode.CTS;
                case 4:
                    return CipherMode.ECB;
                case 5:
                    return CipherMode.OFB;
                default:  return CipherMode.CBC;
            }
        }
        public string GetPassword()
        {
            string pass = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    Console.Write("\b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }
    }
}
