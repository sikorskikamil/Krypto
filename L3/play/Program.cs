using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace play
{
    public class Program
    {
        static void Main(string[] args)
        {
            var start = new Start.Start();
            start.InitStart();
            int x;
            do
            {
                Console.WriteLine("Co chesz zrobic:\n1. zakodowac plik\n2. rozkodowac plik\n3. odtwarzac muzyke\n0. wyjsc");
                x = int.Parse(Console.ReadLine());
                switch (x)
                {
                    case 1:
                        start.EncodeFiles();
                        break;
                    case 2:
                        start.DecodeFiles();
                        break;
                    case 3:
                        start.PlayerVoid();
                        break;
                }
            } while (x != 0);
        }
    }
}
