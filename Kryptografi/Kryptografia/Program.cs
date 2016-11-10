using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kryptografia
{
    class Program
    {
        static void Main(string[] args)
        {
         
            Decrypt[] x = new Decrypt[16];
            for(int i =0; i< 8; ++i)
            {
                x[i] = new Decrypt("000000005a35b0e9286b5586eda60362002026fdeb57afa6a1857215a6de9668", "50e0878541f40803b11eddbfa5bce033", 4, i%8, 8, "pkfUOpfsdIZ0nIagu2NQp/zhWtQUYXSZRk8nKZl6Czpy3N8zUw7XHdUYVLNjqPMvwC06EnXcqTPd4MmkV0k16Nfj1rPL7ybprzDZ6MLwAkIY83uXZP/19i0lVZvqBkeh");
                Thread oThread = new Thread(new ThreadStart(x[i].Run));
                oThread.Start();
            }
            Console.ReadKey();
        }
    }
}
