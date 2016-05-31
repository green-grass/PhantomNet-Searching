namespace PhantomNet.Searching
{
    public class StringFilterOption : FilterOptionBase
    {
        public string Value { get; set; }

        public override string DisplayText => Value;
    }
}
