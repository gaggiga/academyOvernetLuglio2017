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

            var delimiters = new char[] { ',', '\n' };

            if (numbers.StartsWith("//"))
            {
                var delimiter = numbers[2];
                numbers = numbers.Substring(4);

                if (delimiter == '-')
                {
                    if(numbers.StartsWith("-") || numbers.IndexOf("--") > -1)
                    {
                        var list = Regex.Matches(numbers, "-(-[0-9]+)").Cast<Match>().Select(m => m.Groups[1].Value).ToList();

                        if (numbers.StartsWith("-"))
                        {
                            list.Insert(0, numbers.Substring(0, numbers.IndexOf('-', 1)));
                        }

                        throw new ArgumentOutOfRangeException("Negatives not allowed: " + String.Join(",", list), null as Exception);
                    }
                }

                delimiters = new char[] { delimiter };
            }

            var values = numbers.Split(delimiters).Select(s => Int32.Parse(s));

            if(values.Any(v => v < 0))
            {
                //var list = values.Where(v => v < 0).Aggregate("", (acc, val) => acc += val.ToString() + ",");
                var list = String.Join(",", values.Where(v => v < 0).Select(v => v.ToString()).ToArray());
                throw new ArgumentOutOfRangeException("Negatives not allowed: " + list, null as Exception);
            }

            return values.Sum();
        }
    }
}
