using System.Collections.Generic;
using System.Linq;

namespace LottoWinner.Model
{
    public class Tripel<T>
    {

        public readonly List<T> Values;

        public Tripel(T value1, T value2, T value3)
        {
            Values = new List<T>(3);
            Values.Add(value1);
            Values.Add(value2);
            Values.Add(value3);
        }

        public override bool Equals(object obj)
        {
            if (obj is Tripel<T>)
            {
                var triperObj = obj as Tripel<T>;

                return Values.All(value => triperObj.Values.Contains(value));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Values != null ? Values.GetHashCode() : 0);
        }
    }
}