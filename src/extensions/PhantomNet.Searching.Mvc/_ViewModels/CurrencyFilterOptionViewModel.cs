﻿namespace PhantomNet.Searching.Mvc
{
    public interface ICurrencyFilterOptionViewModel : IFilterOptionViewModel
    {
        double Value { get; set; }
    }

    public class CurrencyFilterOptionViewModel
        : FilterOptionViewModelBase,
          ICurrencyFilterOptionViewModel
    {
        public double Value { get; set; }
    }
}
