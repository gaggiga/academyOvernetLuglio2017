using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKata
{
    public class StringCalculator_Belli
    {
        public int Add(string numbers)
        {
            var delimiter = ',';

            if (String.IsNullOrEmpty(numbers)) return 0;

            if (numbers.StartsWith("//"))
            {

                var currentDelimiter = numbers[2].ToString();
                
                if (numbers.Contains("//[") && numbers.Contains("]\n"))
                {
                    Regex rgx = new Regex("]\\n-{0,1}[0-9]");

                    currentDelimiter = numbers.Remove(Regex.Match(numbers, "]\\n-{0,1}[0-9]").Index).Substring(3);

                    //currentDelimiter = numbers.Remove(numbers.IndexOf("]\n")).Substring(3);
                    numbers = numbers.Substring(2);
                }
                
                
                numbers = numbers.Replace(currentDelimiter, delimiter.ToString());
                numbers = numbers.Substring(4);

                if (currentDelimiter == "-")
                    numbers = ReplaceDash(numbers, delimiter);
            }
            else
            {
                numbers = numbers.Replace('\n', delimiter);
            }

            var values = numbers.Split(delimiter).Select(s => Int32.Parse(s));

            if (values.Any(v => v < 0))
            {
                //var list = values.Where(v => v < 0).Aggregate("", (acc, val) => acc += val.ToString() + ",");
                var list = String.Join(",", values.Where(v => v < 0).Select(v => v.ToString()).ToArray());
                throw new ArgumentOutOfRangeException("Negatives not allowed: " + list, null as Exception);
            }

            return values.Where(v => v <= 1000).Sum();
        }


        private string ReplaceDash(string numbers, char delimiter)
        {
            numbers = numbers.Replace(delimiter.ToString() + delimiter.ToString(), delimiter.ToString() + "-");

            if (numbers.StartsWith(","))
            {
                numbers = "-" + numbers.Substring(1);
            }

            return numbers;
        }
    }
}
