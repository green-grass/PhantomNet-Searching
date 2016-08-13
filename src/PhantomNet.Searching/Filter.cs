using System;
using System.Collections.Generic;
using System.Linq;

namespace PhantomNet.Searching
{
    public abstract class Filter<TOption> : IFilter
        where TOption : IFilterOption
    {
        public IEnumerable<string> ParameterNames { get; set; }

        public string DisplayText { get; set; }

        public IEnumerable<TOption> Options { get; set; }

        public IEnumerable<TOption> ActiveOptions => Options?.Where(x => x.IsActive);

        public virtual void SetOptionsAvailability(IFilter postsearchFilter)
        {
            if (!(postsearchFilter is Filter<TOption>))
            {
                // TODO:: Message
                throw new InvalidOperationException();
            }

            SetOptionsAvailability((Filter<TOption>)postsearchFilter);
        }

        protected abstract void SetOptionsAvailability(Filter<TOption> postsearchFilter);
    }
}