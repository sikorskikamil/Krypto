using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace play.Player
{
    public class Player
    {
        public static bool br = false;
        public static void PlayMp3FromUrl(MemoryStream stream)
        {
            var reader = new Mp3FileReader(stream);
            var waveOut = new WaveOutEvent();
            waveOut.Init(reader);
            waveOut.Play();
            while (true)
            {
                if (br)
                    break;
                Thread.Sleep(1000);
            }
            waveOut.Stop();
            br = false;
        }
    }
}
