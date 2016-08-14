namespace PhantomNet.Searching.Mvc
{
    public interface ICurrencyRangeFilterOptionViewModel : IFilterOptionViewModel
    {
        double Start { get; set; }

        double End { get; set; }
    }

    public class CurrencyRangeFilterOptionViewModel
        : FilterOptionViewModel,
          ICurrencyRangeFilterOptionViewModel
    {
        public double Start { get; set; }

        public double End { get; set; }
    }
}
