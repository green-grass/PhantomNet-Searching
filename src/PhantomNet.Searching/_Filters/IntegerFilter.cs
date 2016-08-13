using System;
using System.Linq;

namespace PhantomNet.Searching
{
    public class IntegerFilter : Filter<IntegerFilterOption>
    {
        public void SetValueFormat(string format)
        {
            foreach(var option in Options)
            {
                option.ValueFormat = format;
            }
        }

        public void SetFormatter(Func<int, string, string> formater)
        {
            foreach (var option in Options)
            {
                option.Formater = formater;
            }
        }

        public void ActivateOption(int value)
        {
            var activeOptions = Options.Where(x => x.Value == value);
            foreach (var option in activeOptions)
            {
                option.IsActive = true;
            }
        }

        protected override void SetOptionsAvailability(Filter<IntegerFilterOption> postsearchFilter)
        {
            foreach(var option in Options)
            {
                option.IsAvailable = postsearchFilter.Options.Any(x => x.Value == option.Value);
            }
        }
    }
}
