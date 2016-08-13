using System;

namespace PhantomNet.Searching
{
    public class IntegerFilterOption : FilterOptionBase
    {
        public IntegerFilterOption(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public string ValueFormat { get; set; } = "N0";

        public Func<int, string, string> Formater { get; set; } = (value, format) => value.ToString(format);

        public override string DisplayText => Formater(Value, ValueFormat);
    }
}
