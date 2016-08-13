using System;
using System.Collections.Generic;
using System.Linq;

namespace PhantomNet.Searching
{
    public class KeyValueFilter<TKey> : Filter<KeyValueFilterOption<TKey>>
        where TKey : IEquatable<TKey>
    {
        public void ActivateOption(TKey key)
        {
            var activeOptions = Options.Where(x => x.Key.Equals(key));
            foreach (var option in activeOptions)
            {
                option.IsActive = true;
            }
        }

        public void ActivateOptions(IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                ActivateOption(key);
            }
        }

        protected override void SetOptionsAvailability(Filter<KeyValueFilterOption<TKey>> postsearchFilter)
        {
            foreach (var option in Options)
            {
                option.IsAvailable = postsearchFilter.Options.Any(x => x.Key.Equals(option.Key));
            }
        }
    }
}
