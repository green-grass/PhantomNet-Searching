using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PhantomNet.Searching.Entities.AspNetCore
{
    public class AspNetCoreEntitiesSearchManager<TEntity, TParameters, TStore>
        : EntitiesSearchManager<TEntity, TParameters, TStore>
        where TEntity : class
        where TParameters : class
        where TStore : IDisposable
    {
        private readonly HttpContext _context;

        public AspNetCoreEntitiesSearchManager(
            TStore store,
            ILogger<EntitiesSearchManager<TEntity, TParameters, TStore>> logger,
            IHttpContextAccessor contextAccessor)
            : base(store, logger)
        {
            _context = contextAccessor?.HttpContext;
        }

        protected override CancellationToken CancellationToken => _context?.RequestAborted ?? base.CancellationToken;
    }
}
