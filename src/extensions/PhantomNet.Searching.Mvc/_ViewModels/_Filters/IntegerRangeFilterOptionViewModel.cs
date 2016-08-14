namespace PhantomNet.Searching.Mvc
{
    public interface IIntegerRangeFilterOptionViewModel : IFilterOptionViewModel
    {
        int Start { get; set; }

        int End { get; set; }
    }

    public class IntegerRangeFilterOptionViewModel
        : FilterOptionViewModel,
          IIntegerRangeFilterOptionViewModel
    {
        public int Start { get; set; }

        public int End { get; set; }
    }
}
