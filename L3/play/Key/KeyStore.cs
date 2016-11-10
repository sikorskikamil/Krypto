using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace play.Key
{
    public class KeyIv
    {
        public byte[] Key { get; set; }
        public byte[] Iv { get; set; }
    }
    public static class KeyStore
    {
        public static List<KeyIv> GetKeyIvList(StreamReader keyStream) {
            var list = new List<KeyIv>();
            try
            {
                string key , iv;
                while ((key = keyStream.ReadLine()) != null && (iv = keyStream.ReadLine()) != null)
                {
                    if (key.Length != 64 && iv.Length != 32)
                        continue;
                    list.Add(new KeyIv
                    {
                        Key = crypto.CryptoHelper.GetByteArrayFromStringHex(key),
                        Iv = crypto.CryptoHelper.GetByteArrayFromStringHex(iv)
                    });
                }
            }
            catch (Exception){}
            return list;
        }
    }
}
