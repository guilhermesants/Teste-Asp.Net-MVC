using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PrimeiraQuestaoAvonale.Service
{
    public class Arquivo
    {
  
        static string caminho = PegarDiretorio();
        static string arquivo = @"\App_Data\RepositorioDados.txt";
        static string caminhoArquivo = caminho + arquivo;

        public static IEnumerable<string> Dados()
        {

            var dados = new Collection<string>();

            try
            {
                using (StreamReader arquivo = File.OpenText(caminhoArquivo))
                {
                    string linha;

                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        dados.Add(linha);
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }

            return dados;
        }

        public static void InserirDados(string dado)
        {
            using (StreamWriter insere = File.AppendText(caminhoArquivo))
            {
                insere.WriteLine(dado);
                insere.Close();
            }
        }

        // retorna caminho do diretório
        private static string PegarDiretorio()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

            var indice = assemblyPath.IndexOf('C');
            var caminho = assemblyPath.Remove(0, indice);
            var ultimoIndice = caminho.LastIndexOf('\\');

            caminho = caminho.Remove(ultimoIndice, (caminho.Length - ultimoIndice));

            return caminho;
        }

    }
}