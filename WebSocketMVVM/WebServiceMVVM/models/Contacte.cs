using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceMVVM.models
{
    public class Contacte
    {
        public int contacteId;
        public string nom, cognoms;
        private List<Telefon> telefons = new List<Telefon>();

        private Contacte(int contacteId, string nom, string cognoms, List<Telefon> list)
        {
            this.contacteId = contacteId;
            this.nom = nom;
            this.cognoms = cognoms;
            this.telefons = list;
        }

        [JsonConstructor]
        public Contacte(int contacteId, string nom, string cognoms)
        {
            this.contacteId = contacteId;
            this.nom = nom;
            this.cognoms = cognoms;
        }
    }
}
