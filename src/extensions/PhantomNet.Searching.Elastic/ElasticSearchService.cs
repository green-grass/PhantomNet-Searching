//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Options;
//using Nest;

//namespace PhantomNet.Searching.Elastic
//{
//    public class ElasticSearchService<TEntity, TSearchParameters, TSearchResult>
//        : ISearchService<TEntity, TSearchParameters, TSearchResult>
//        where TEntity : class
//        where TSearchParameters : class
//        where TSearchResult : SearchResult<TEntity>, new()
//    {
//        public const int DefaultPageSize = 10;

//        public ElasticSearchService(
//            IElasticSearchProvider<TEntity, TSearchParameters> searchProvider,
//            IOptions<ElasticSearchOptions> optionsAccessor)
//        {
//            if (searchProvider == null)
//            {
//                throw new ArgumentNullException(nameof(searchProvider));
//            }
//            if (optionsAccessor == null)
//            {
//                throw new ArgumentNullException(nameof(optionsAccessor));
//            }
//            if (optionsAccessor?.Value?.Connections == null || optionsAccessor.Value.Connections.Count() == 0)
//            {
//                throw new ArgumentException(nameof(optionsAccessor));
//            }
//            if (string.IsNullOrWhiteSpace(optionsAccessor?.Value?.IndexName))
//            {
//                throw new ArgumentException(nameof(optionsAccessor));
//            }

//            Connections = optionsAccessor.Value.Connections.Select(x => new Uri(x));
//            IndexName = optionsAccessor.Value.IndexName;
//            SearchProvider = searchProvider;
//        }

//        protected IEnumerable<Uri> Connections { get; }

//        protected string IndexName { get; }

//        protected IElasticSearchProvider<TEntity, TSearchParameters> SearchProvider { get; }

//        public virtual async Task<TSearchResult> SearchAsync(TSearchParameters parameters)
//        {
//            // TODO:: Check
//            //var connectionPool = new SniffingConnectionPool(Connections);
//            //var settings = new ConnectionSettings(connectionPool)
//            //    .DefaultIndex(IndexName)
//            //    .DefaultFieldNameInferrer(x => x.ToLower())
//            //    .DisableDirectStreaming();
//            //var client = new ElasticClient(settings);

//            var uri = Connections.FirstOrDefault();
//            var settings = new ConnectionSettings(uri)
//                .DefaultIndex(IndexName)
//                .DefaultFieldNameInferrer(x => x.ToLower())
//                .DisableDirectStreaming();
//            var client = new ElasticClient(settings);

//            var result = await client.SearchAsync<TEntity>(s => SearchProvider.BuildRequest(s, parameters));

//            if (result?.CallDetails?.RequestBodyInBytes != null)
//            {
//                var rawRequest = Encoding.UTF8.GetString(result.CallDetails.RequestBodyInBytes, 0, result.CallDetails.RequestBodyInBytes.Length);
//                // TODO:: Log request
//            }

//            if (!result.IsValid)
//            {
//                // TODO:: Log error
//                return null;
//            }

//            var filters = new List<IFilter>();
//            foreach (var item in result.Aggregations)
//            {
//                var filter = SearchProvider.SelectFilter(item.Key);
//                if (filter is IElasticFilter)
//                {
//                    var bucket = item.Value as BucketAggregate;
//                    if (bucket == null)
//                    {
//                        filter = null;
//                    }
//                    else
//                    {
//                        ((IElasticFilter)filter).BuildOptions(bucket);
//                    }
//                }
//                if (filter != null)
//                {
//                    filter.ParameterNames = SearchProvider.MapParameterNames(item.Key);
//                    filter.DisplayText = item.Key;
//                    SearchProvider.ActivateFilterOption(filter, parameters);
//                    filters.Add(filter);
//                }
//            }

//            return new TSearchResult {
//                FilterredCount = result.Total,
//                PageNumber = SearchProvider.GetPageNumber(parameters),
//                PageSize = SearchProvider.GetPageSize(parameters),
//                Entities = result.Documents,
//                Filters = filters
//            };
//        }

//        public virtual Task<SuggestResult<TEntity, TSearchResult>> SuggestAsync(TSearchParameters parameters)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
