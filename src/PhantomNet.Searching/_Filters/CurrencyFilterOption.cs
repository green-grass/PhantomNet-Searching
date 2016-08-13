using System;

namespace PhantomNet.Searching
{
    public class CurrencyFilterOption : FilterOptionBase
    {
        public CurrencyFilterOption(double value)
        {
            Value = value;
        }

        public double Value { get; set; }

        public string ValueFormat { get; set; } = "C0";

        public Func<double, string, string> Formater { get; set; } = (value, format) => value.ToString(format);

        public override string DisplayText => Formater(Value, ValueFormat);
    }
}
