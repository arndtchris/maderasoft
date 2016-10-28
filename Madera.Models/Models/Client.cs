using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madera.Model
{
    public class Client : Personne
    {
        public int ClientID { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public Client()
        {

        }

    }
}
