using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKata
{
    public class StringCalculator_Belli
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers)) return 0;

            var delimiters = new char[] { ',', '\n' };

            if (numbers.StartsWith("//"))
            {
                delimiters = new char[] { numbers[2] };
                numbers = numbers.Substring(4);
            }

            var result = numbers.Split(delimiters);
            int somma = 0;
            List<int> listaNegativi = new List<int>();

            foreach (var num in result)
            {
                var x = Int32.Parse(num);
                if (x>=0 && x <= 1000) somma += x;
                if (x < 0)
                {
                    listaNegativi.Add(x);
                }
            }

            if(listaNegativi.Count()!=0)
            {
                string messageError = "negatives not allowed: ";
                foreach (var num in listaNegativi)
                {
                    messageError += num.ToString();
                    if (num != listaNegativi.Last())
                    {
                        messageError += ',';
                    }
                }
                throw new ArgumentOutOfRangeException(messageError, null as Exception);
            }
            else if(somma != 0)
            {
                return somma;
            }

            return result.Sum(s => Int32.Parse(s));
        }
    }
}
