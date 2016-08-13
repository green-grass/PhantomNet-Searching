using System.Collections.Generic;

namespace PhantomNet.Searching
{
    public interface IFilter
    {
        IEnumerable<string> ParameterNames { get; set; }

        string DisplayText { get; set; }

        void SetOptionsAvailability(IFilter postsearchFilter);
    }
}
