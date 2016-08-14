using System.Collections.Generic;

namespace PhantomNet.Searching.Mvc
{
    public interface IFilterViewModel<TOptionViewModel>
        where TOptionViewModel : FilterOptionViewModel
    {
        IEnumerable<string> ParameterNames { get; set; }

        string DisplayText { get; set; }

        IEnumerable<TOptionViewModel> Options { get; set; }

        IEnumerable<TOptionViewModel> ActiveOptions { get; set; }
    }

    public class FilterViewModel<TOptionViewModel> : IFilterViewModel<TOptionViewModel>
        where TOptionViewModel : FilterOptionViewModel
    {
        public IEnumerable<string> ParameterNames { get; set; }

        public string DisplayText { get; set; }

        public IEnumerable<TOptionViewModel> Options { get; set; }

        public IEnumerable<TOptionViewModel> ActiveOptions { get; set; }
    }
}
