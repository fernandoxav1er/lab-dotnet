using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.ConsoleApp
{
    internal class LinqClass
    {
        private List<string> frutas = new List<string>() { "Banana", "Maça", "Pera", "Laranja", "Uva"};
    
        public void QuerySintax()
        {
            var resultado = from f in frutas where f.Contains('r') select f;

            Console.WriteLine(String.Join(" ", resultado));
            Console.ReadKey();
        }

        public void MethodSintax()
        {
            var resultado = frutas.Where(f => f.Contains('r'));

            Console.WriteLine(String.Join(" ", resultado));
            Console.ReadKey();
        }

        public void ConvertendoArrayEListas()
        {
            //partindo de uma lista para array
            string[] arrayFrutas = frutas.ToArray();

            //partindo de um array para lista
            List<string> list = arrayFrutas.ToList();
        }
    }
}
