using System;

namespace LottoWinner.Model
{
    public class LottoNumber
    {
        public DateTime LosTime { get; set; }
        public int[] Numbers = new int[6];
        public int SuperNumber;

        public int SameNumbersWithLast;

        public int SameNumberWithForelast;

        public bool IsSuperNumberTheSameAsLastTime;
    }
}