using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Models
{
    
    public class IDToken
    {
        private string nbf { get; set; }
        private string exp { get; set; }
        private string iss { get; set; }
        private string aud { get; set; }
        private string nonce { get; set; }
        private string iat { get; set; }
        private string at_hash { get; set; }
        private string sid { get; set; }
        private string idp { get; set; }
        private string role { get; set; }
        //public string[] amr { get; set; }
        public IDToken(string nbf, string exp, string iss, string aud, string nonce, string iat, string at_hash, string sid, string idp, string role)
        {
            this.nbf = nbf;
            this.exp = exp;
            this.iss = iss;
            this.aud = aud;
            this.nonce = nonce;
            this.iat = iat;
            this.at_hash = at_hash;
            this.sid = sid;
            this.idp = idp;
            this.role = role;
        }
    }
}
