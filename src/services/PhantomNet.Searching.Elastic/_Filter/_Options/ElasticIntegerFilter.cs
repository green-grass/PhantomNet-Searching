using System.Collections.Generic;
using System.Linq;
using Nest;

namespace PhantomNet.Searching.Elastic
{
    public class ElasticIntegerFilter : Filter<IntegerFilterOption>, IElasticFilter
    {
        public void BuildOptions(BucketAggregate bucket)
        {
            Options = new List<IntegerFilterOption>();
            foreach (KeyedBucket item in bucket.Items.Where(x => ((KeyedBucket)x).DocCount > 0))
            {
                var option = new IntegerFilterOption {
                    Value = int.Parse(item.Key),
                    EntityCount = item.DocCount.Value
                };

                Options.Add(option);
            }
        }
    }
}
