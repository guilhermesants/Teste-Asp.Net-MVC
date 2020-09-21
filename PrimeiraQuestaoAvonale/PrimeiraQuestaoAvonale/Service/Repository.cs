using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimeiraQuestaoAvonale.Service
{
    public class Repository
    {
        public static IEnumerable<string> GetAll()
        {
            return Arquivo.Dados().ToList();
        }

        public static void Add(string nome)
        {
            if (nome != null)
            {
                Arquivo.InserirDados(nome);
            }
        }
        
        public static bool ExistElement(string nome)
        {
            if (Arquivo.Dados().Contains(nome))
            {
                return true;
            }
            return false;
        }
    }
}