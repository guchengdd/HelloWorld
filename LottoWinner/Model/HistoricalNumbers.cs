using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LottoWinner.Model
{
    public class HistoricalNumbers
    {
        private List<LottoNumber> _numbers = new List<LottoNumber>();

        public void Load()
        {
            List<string> lines = File.ReadLines("data\\lotto.txt").ToList();
            lines.RemoveAt(0);


            for (int index = 0; index < lines.Count; index++)
            {
                string s = lines[index];
                string[] numbers = s.Split('\t');
                LottoNumber number = new LottoNumber();
                for (int i = 0; i < 6; i++)
                {
                    number.Numbers[i] = int.Parse(numbers[i + 3]);
                }
                number.SuperNumber = int.Parse(numbers[10]);

                if (index > 0)
                {
                    number.SameNumbersWithLast = GetNumberOfSameNumbers(number, _numbers.Last());
                    number.IsSuperNumberTheSameAsLastTime = number.SuperNumber == _numbers.Last().SuperNumber;
                }
                if (index > 1)
                {
                    number.SameNumberWithForelast = GetNumberOfSameNumbers(number, _numbers[index - 2]);
                }

                _numbers.Add(number);
            }

            Evoluate1();
            Evoluate2();
            Evo3();
        }

        public static int GetNumberOfSameNumbers(LottoNumber n1, LottoNumber n2)
        {
            int count = 0;
            foreach (int num in n1.Numbers)
            {
                if (n2.Numbers.Contains(num))
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Sees how often the number repeats from last time.
        /// </summary>
        public void Evoluate1()
        {
            int count0 = _numbers.Count(lottoNumber => lottoNumber.SameNumbersWithLast == 0);
            int count1 = _numbers.Count(lottoNumber => lottoNumber.SameNumbersWithLast == 1);
            int count2 = _numbers.Count(lottoNumber => lottoNumber.SameNumbersWithLast == 2);
            int count3 = _numbers.Count(lottoNumber => lottoNumber.SameNumbersWithLast == 3);
            int count4 = _numbers.Count(lottoNumber => lottoNumber.SameNumbersWithLast == 4);
            int count5 = _numbers.Count(lottoNumber => lottoNumber.SameNumbersWithLast == 5);
            int count6 = _numbers.Count(lottoNumber => lottoNumber.SameNumbersWithLast == 6);

            int sum = _numbers.Count;

            Console.Out.WriteLine("Results for repeats from last time:");
            Console.Out.WriteLine("--------------------------------------------------------");
            Console.Out.WriteLine("Total loses: " + sum);
            Console.Out.WriteLine("No number repeats: " + count0 + ". Percentage: " + (double)count0/sum);
            Console.Out.WriteLine("The calculated percentage should be: ");
            Console.Out.WriteLine("One number repeats: " + count1 + ". Percentage: " + (double)count1 / sum);
            Console.Out.WriteLine("Two number repeats: " + count2 + ". Percentage: " + (double)count2 / sum);
            Console.Out.WriteLine("Three number repeats: " + count3 + ". Percentage: " + (double)count3 / sum);
            Console.Out.WriteLine("Four number repeats: " + count4 + ". Percentage: " + (double)count4 / sum);
            Console.Out.WriteLine("Five number repeats: " + count5 + ". Percentage: " + (double)count5 / sum);
            Console.Out.WriteLine("Six number repeats: " + count6 + ". Percentage: " + (double)count6 / sum);
            Console.Out.WriteLine();
        }

        /// <summary>
        /// Sees how often the number repeats from forelast time.
        /// </summary>
        public void Evoluate2()
        {
            int count0 = _numbers.Count(lottoNumber => lottoNumber.SameNumberWithForelast == 0);
            int count1 = _numbers.Count(lottoNumber => lottoNumber.SameNumberWithForelast == 1);
            int count2 = _numbers.Count(lottoNumber => lottoNumber.SameNumberWithForelast == 2);
            int count3 = _numbers.Count(lottoNumber => lottoNumber.SameNumberWithForelast == 3);
            int count4 = _numbers.Count(lottoNumber => lottoNumber.SameNumberWithForelast == 4);
            int count5 = _numbers.Count(lottoNumber => lottoNumber.SameNumberWithForelast == 5);
            int count6 = _numbers.Count(lottoNumber => lottoNumber.SameNumberWithForelast == 6);

            int sum = _numbers.Count;

            Console.Out.WriteLine("Results for repeats from forelast time:");
            Console.Out.WriteLine("--------------------------------------------------------");
            Console.Out.WriteLine("Total loses: " + sum);
            Console.Out.WriteLine("No number repeats: " + count0 + ". Percentage: " + (double)count0 / sum);
            Console.Out.WriteLine("One number repeats: " + count1 + ". Percentage: " + (double)count1 / sum);
            Console.Out.WriteLine("Two number repeats: " + count2 + ". Percentage: " + (double)count2 / sum);
            Console.Out.WriteLine("Three number repeats: " + count3 + ". Percentage: " + (double)count3 / sum);
            Console.Out.WriteLine("Four number repeats: " + count4 + ". Percentage: " + (double)count4 / sum);
            Console.Out.WriteLine("Five number repeats: " + count5 + ". Percentage: " + (double)count5 / sum);
            Console.Out.WriteLine("Six number repeats: " + count6 + ". Percentage: " + (double)count6 / sum);
            Console.Out.WriteLine();
        }

        public void Evo3()
        {
            int countSame = _numbers.Count(lottoNumber => lottoNumber.IsSuperNumberTheSameAsLastTime);
            int sum = _numbers.Count;
            int countDifferent = sum - countSame;

            Console.Out.WriteLine("Results for repeats of super number from last time:");
            Console.Out.WriteLine("--------------------------------------------------------");
            Console.Out.WriteLine("Total loses: " + sum);
            Console.Out.WriteLine("Number repeats: " + countSame + ". Percentage: " + (double)countSame / sum);
            Console.Out.WriteLine("Number does not repeat: " + countDifferent + ". Percentage: " + (double)countDifferent / sum);
        }
    }
}