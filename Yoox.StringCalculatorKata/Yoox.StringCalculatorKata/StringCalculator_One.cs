using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKataOne
{
    public class StringCalculator_One
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers)) return 0;

            var delimiter = ",";
            var match = Regex.Match(numbers, @"//\[(.+)\]\n|//(.)\n|//(\n)\n");

            if (match.Success)
            {
                var groups = new Group[] { match.Groups[1], match.Groups[2], match.Groups[3] };
                var currentDelimiter = groups.First(s => s.Success).ToString();

                numbers = numbers.Substring(match.Value.Length);
                numbers = numbers.Replace(currentDelimiter, delimiter);

                if (currentDelimiter.Equals("-"))
                    numbers = ReplaceDash(numbers, delimiter);
            }
            else
            {
                numbers = numbers.Replace("\n", delimiter);
            }

            var values = numbers.Split(new string[] { delimiter }, StringSplitOptions.None).Select(s => Int32.Parse(s));

            if (values.Any(v => v < 0))
            {
                var list = String.Join(delimiter, values.Where(v => v < 0).Select(v => v.ToString()).ToArray());
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
