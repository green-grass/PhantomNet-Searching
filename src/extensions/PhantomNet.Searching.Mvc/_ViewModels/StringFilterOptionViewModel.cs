namespace PhantomNet.Searching.Mvc
{
    public interface IStringFilterOptionViewModel : IFilterOptionViewModel
    {
        string Value { get; set; }
    }

    public class StringFilterOptionViewModel
        : FilterOptionViewModelBase,
          IStringFilterOptionViewModel
    {
        public string Value { get; set; }
    }
}
