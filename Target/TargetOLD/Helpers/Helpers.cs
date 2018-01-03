using Newtonsoft.Json;
using Plugin.GoogleAnalytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;
using Target.Models;
using Xamarin.Forms;

namespace Target.Helpers
{
    public static class Helpers
    {
        public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }
        public static object GetPayload(string payloadstr)
        {
            String[] substrings = payloadstr.Split('.');
            if (substrings.Count() != 3)
            {
                GoogleAnalytics.Current.Tracker.SendException("ClayCustomError: too many token segments", false);
            }
            var headerSeg = substrings[0];
            var payloadSeg = substrings[1];
            var signatureSeg = substrings[2];


            var newBytes = Convert.FromBase64String(payloadSeg);
            //var newString = BitConverter.ToString(newBytes);

            //return JsonConvert.DeserializeObject(str);
            //return DependencyService.Get<IPlatformStuff>().ByteArrayToObject(newBytes);
            return Desserialize(newBytes);
        }
        public static IDToken Desserialize(byte[] data)
        {


            string nbf;
        string exp;
        string iss;
        string aud;
        string nonce;
        string iat;
        string at_hash;
        string sid;
        string idp;
        string role;
        //string[] amr;

            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    nbf = reader.ReadString();
                    exp = reader.ReadString();
                    iss = reader.ReadString();
                    aud = reader.ReadString();
                    nonce = reader.ReadString();
                    iat = reader.ReadString();
                    at_hash = reader.ReadString();
                    sid = reader.ReadString();
                    idp = reader.ReadString();
                    exp = reader.ReadString();
                    role = reader.ReadString();
                }
            }

            return new IDToken(nbf, exp, iss, aud, nonce, iat, at_hash, sid, idp, role);
        }
    }
}
