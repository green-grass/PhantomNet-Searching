using System;

namespace PhantomNet.Searching.Mvc
{
    public interface IKeyValueFilterOptionViewModel<TKey> : IFilterOptionViewModel
        where TKey : IEquatable<TKey>
    {
        TKey Key { get; set; }
    }

    public class KeyValueFilterOptionViewModel<TKey>
        : FilterOptionViewModel,
          IKeyValueFilterOptionViewModel<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Key { get; set; }
    }
}
