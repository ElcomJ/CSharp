using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatosTreinaWeb
{
    public class ManipuladorDeArquivos
    {
        private static string EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "contatos.txt";  //Acessa a raiz do projeto

        public static List<Contato> LerArquivo()
        {
            List<Contato> contatosList = new List<Contato>();
            if (File.Exists(@EnderecoArquivo))
            {
                using (StreamReader sr = File.OpenText(@EnderecoArquivo))
                {
                    while (sr.Peek() >= 0)                                  //Peek faz a leitura enquanto tiver caractere para ler, quando acabar ele retorn -1
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';');          //O split particiona uma linha a partir de um caractere chave que tem nela
                        if (linhaComSplit.Count() == 3)
                        {
                            Contato contato = new Contato();
                            contato.Nome = linhaComSplit[0];
                            contato.Email = linhaComSplit[1];
                            contato.NumeroTelefone = linhaComSplit[2];
                            contatosList.Add(contato);
                        }
                    }
                }
            }
            return contatosList;
        }

        public static void EscreverAquivo(List<Contato> contatosList)
        {
            using (StreamWriter sw = new StreamWriter(@EnderecoArquivo, false)) //O @ é usada antes da constante que direciona para onde o arquivo esta.
            {
                foreach (Contato contato in contatosList)
                {
                    string linha = string.Format("{0}; {1}; {2}", contato.Nome, contato.Email, contato.NumeroTelefone);
                    sw.WriteLine(linha);
                }
                sw.Flush();  //Limpas od buffers do StringWriter.
            }
        }
    }
}

#region Coments
    //Os statics nos membros são usadas para não precisar intanciar a classe para acessa-los.
    //sw.Close();  Fecha o StringWriter liberando o ponteiro que ficava apontando para ele.
    
    //Usar o using faz com que o sw.Close(); seja chamando automaticamente ao chegar no final do bloco de instrução
#endregion
