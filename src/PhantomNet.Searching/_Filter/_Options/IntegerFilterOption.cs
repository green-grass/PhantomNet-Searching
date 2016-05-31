namespace PhantomNet.Searching
{
    public class IntegerFilterOption : FilterOptionBase
    {
        public int Value { get; set; }

        public override string DisplayText => Value.ToString();
    }
}
