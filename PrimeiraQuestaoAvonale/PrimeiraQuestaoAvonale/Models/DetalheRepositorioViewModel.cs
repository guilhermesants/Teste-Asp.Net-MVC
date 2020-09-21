using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeiraQuestaoAvonale.Models
{
    public class DetalheRepositorioViewModel
    {

        public string name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string updated_at { get; set; }

        public Proprietario owner { get; set; }
        
        public class Proprietario
        {
            public string login { get; set; }
        }
    }
}