using System;
using System.Linq;

namespace PhantomNet.Searching
{
    public class CurrencyFilter : Filter<CurrencyFilterOption>
    {
        public void SetValueFormat(string format)
        {
            foreach (var option in Options)
            {
                option.ValueFormat = format;
            }
        }

        public void SetFormatter(Func<double, string, string> formater)
        {
            foreach (var option in Options)
            {
                option.Formater = formater;
            }
        }

        public void ActivateOption(double value)
        {
            var activeOptions = Options.Where(x => x.Value == value);
            foreach (var option in activeOptions)
            {
                option.IsActive = true;
            }
        }

        protected override void SetOptionsAvailability(Filter<CurrencyFilterOption> postsearchFilter)
        {
            foreach (var option in Options)
            {
                option.IsAvailable = postsearchFilter.Options.Any(x => x.Value == option.Value);
            }
        }
    }
}
