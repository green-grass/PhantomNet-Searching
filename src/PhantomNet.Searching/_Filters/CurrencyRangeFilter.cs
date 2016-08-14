using System;
using System.Linq;

namespace PhantomNet.Searching
{
    public class CurrencyRangeFilter : Filter<CurrencyRangeFilterOption>
    {
        public void SetValueFormat(string format)
        {
            foreach (var option in Options)
            {
                option.ValueFormat = format;
            }
        }

        public void SetFormatter(Func<double, double, string, string> formater)
        {
            foreach (var option in Options)
            {
                option.Formater = formater;
            }
        }

        public void ActivateOption(double start, double end)
        {
            var activeOptions = Options.Where(x => x.Start <= start && x.End >= end);
            foreach (var option in activeOptions)
            {
                option.IsActive = true;
            }
        }

        protected override void SetOptionsAvailability(Filter<CurrencyRangeFilterOption> postsearchFilter)
        {
            foreach (var option in Options)
            {
                option.IsAvailable = postsearchFilter.Options.Any(x => x.Start == option.Start && x.End == option.End);
            }
        }
    }
}
