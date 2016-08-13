using System;

namespace PhantomNet.Searching
{
    public class StringFilterOption : FilterOptionBase
    {
        public StringFilterOption(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            Value = value;

        }

        public string Value { get; set; }

        public override string DisplayText => Value;
    }
}
