using System.Collections.Generic;
using System.Linq;

namespace PhantomNet.Searching
{
    public class StringFilter : Filter<StringFilterOption>
    {
        public void ActivateOption(string value)
        {
            var activeOptions = Options.Where(x => x.Value == value);
            foreach (var option in activeOptions)
            {
                option.IsActive = true;
            }
        }

        public void ActivateOptions(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                ActivateOption(value);
            }
        }

        protected override void SetOptionsAvailability(Filter<StringFilterOption> postsearchFilter)
        {
            foreach (var option in Options)
            {
                option.IsAvailable = postsearchFilter.Options.Any(x => x.Value == option.Value);
            }
        }
    }
}
