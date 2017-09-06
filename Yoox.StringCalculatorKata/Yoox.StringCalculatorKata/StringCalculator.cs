using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKata
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers)) return 0;
            var delimiter = ",";

            if (numbers.StartsWith("//"))
            {
                var match = Regex.Match(numbers, @"//\[(.+)\]\n|//(.|\n)\n", RegexOptions.Singleline);

                var currentDelimiter = match.Groups.Cast<Group>().Skip(1).First(g => g.Success).Value;
                numbers = numbers.Substring(match.Value.Length);
                numbers = numbers.Replace(currentDelimiter, delimiter);

                if (currentDelimiter == "-")
                    numbers = ReplaceDash(numbers, delimiter);
            }
            else
            {
                numbers = numbers.Replace("\n", delimiter);
            }

            var values = numbers.Split(delimiter[0]).Select(s => Int32.Parse(s));

            if (values.Any(v => v < 0))
            {
                var list = String.Join(",", values.Where(v => v < 0).Select(v => v.ToString()).ToArray());
                throw new ArgumentOutOfRangeException("Negatives not allowed: " + list, null as Exception);
            }

            return values.Where(v => v <= 1000).Sum();
        }

        private string ReplaceDash(string numbers, string delimiter)
        {
            numbers = numbers.Replace(delimiter + delimiter, delimiter + "-");

            if (numbers.StartsWith(","))
            {
                numbers = "-" + numbers.Substring(1);
            }

            return numbers;
        }
    }
}
