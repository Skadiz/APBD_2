using System;
using System.Collections.Generic;

namespace Zadanie2
{
    class Comparator : IEqualityComparer<Student>
    {

        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.firstName} {x.lastName} {x.indexNumber}",
                $"{y.firstName} {y.lastName} {y.indexNumber}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer
                .CurrentCultureIgnoreCase
                .GetHashCode($"{obj.firstName} {obj.lastName} {obj.indexNumber}");
        }

    }
}