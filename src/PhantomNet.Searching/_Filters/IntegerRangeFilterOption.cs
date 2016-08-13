using System;

namespace PhantomNet.Searching
{
    public class IntegerRangeFilterOption : FilterOptionBase
    {
        public int Start { get; set; }

        public int End { get; set; }

        public string ValueFormat { get; set; } = "N0";

        public Func<int, int, string, string> Formater { get; set; }
            = (start, end, format) => $"{start.ToString(format)} - {end.ToString(format)}";

        public override string DisplayText => Formater(Start, End, ValueFormat);
    }
}
