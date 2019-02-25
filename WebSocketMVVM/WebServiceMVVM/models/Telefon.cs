using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceMVVM.models
{
    class Telefon
    {
        public int telId, contacteId;
        public string telefon, tipus;

        public Telefon(int telId, string telefon, string tipus, int contacteId)
        {
            this.telId = telId;
            this.telefon = telefon;
            this.tipus = tipus;
            this.contacteId = contacteId;
        }
    }
}
