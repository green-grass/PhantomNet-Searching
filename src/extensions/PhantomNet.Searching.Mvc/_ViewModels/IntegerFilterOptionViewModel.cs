namespace PhantomNet.Searching.Mvc
{
    public interface IIntegerFilterOptionViewModel : IFilterOptionViewModel
    {
        int Value { get; set; }
    }

    public class IntegerFilterOptionViewModel
        : FilterOptionViewModelBase,
          IIntegerFilterOptionViewModel
    {
        public int Value { get; set; }
    }
}
