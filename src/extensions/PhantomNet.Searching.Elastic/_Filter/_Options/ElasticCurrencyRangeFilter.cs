using System.Collections.Generic;
using System.Linq;
using Nest;

namespace PhantomNet.Searching.Elastic
{
    public class ElasticCurrencyRangeFilter : Filter<CurrencyRangeFilterOption>, IElasticFilter
    {
        public void BuildOptions(BucketAggregate bucket)
        {
            Options = new List<CurrencyRangeFilterOption>();
            foreach (KeyedBucket item in bucket.Items.Where(x => ((KeyedBucket)x).DocCount > 0))
            {
                var option = new CurrencyRangeFilterOption {
                    Start = double.Parse(item.Key),
                    EntityCount = item.DocCount.Value
                };

                Options.Add(option);
            }

            if (Options.Count > 0)
            {
                for (var i = 0; i < Options.Count - 1; i++)
                {
                    Options.Skip(i).First().End = Options.Skip(i + 1).First().Start - 1;
                }
                Options.First().Start = double.MinValue;
                Options.Last().End = double.MaxValue;
            }
        }
    }
}
