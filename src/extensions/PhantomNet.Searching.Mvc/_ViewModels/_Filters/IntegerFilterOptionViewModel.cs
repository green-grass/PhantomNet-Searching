namespace PhantomNet.Searching.Mvc
{
    public interface IIntegerFilterOptionViewModel : IFilterOptionViewModel
    {
        int Value { get; set; }
    }

    public class IntegerFilterOptionViewModel
        : FilterOptionViewModel,
          IIntegerFilterOptionViewModel
    {
        public int Value { get; set; }
    }
}
