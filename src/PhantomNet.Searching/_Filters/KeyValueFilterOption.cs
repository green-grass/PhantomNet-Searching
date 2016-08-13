using System;
using System.Collections.Generic;

namespace PhantomNet.Searching
{
    public class KeyValueFilterOption<TKey> : FilterOptionBase
        where TKey : IEquatable<TKey>
    {
        public KeyValueFilterOption(TKey key, string value)
        {
            Data = new KeyValuePair<TKey, string>(key, value);
        }

        protected KeyValuePair<TKey, string> Data { get; set; }

        public TKey Key => Data.Key;

        public override string DisplayText => Data.Value;
    }
}
