using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace play.crypto
{
    public class CryptoHelper
    {
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
                return Char.ToString((char)('a' + value - 10));
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
        public static string GetPass()
        {
            string pass = "";
            Console.Write("Enter your password: ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace)
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
            return pass;
        } 
    }
}
