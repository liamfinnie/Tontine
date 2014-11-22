using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Raven.Client;
using Raven.Client.Document;

namespace TontineService.ReferenceData.Controllers
{
    public abstract class RavenDbController : ApiController
    {
        public IDocumentStore Store
        {
            get { return _lazyDocStore.Value; }
        }

        private static readonly Lazy<IDocumentStore> _lazyDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "ReferenceData"
            };

            docStore.Initialize();
            return docStore;
        });

        public IAsyncDocumentSession Session { get; set; }

        public override async Task<HttpResponseMessage> ExecuteAsync(
            HttpControllerContext controllerContext,
            CancellationToken cancellationToken)
        {
            using (Session = Store.OpenAsyncSession())
            {
                var result = await base.ExecuteAsync(controllerContext, cancellationToken);
                await Session.SaveChangesAsync();

                return result;
            }
        }
    }
}
