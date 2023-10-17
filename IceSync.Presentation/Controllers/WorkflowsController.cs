using IceSync.Domain.Constants;
using IceSync.Domain.Models.Caching;
using IceSync.Infrastructure.ExternalApis;
using Microsoft.AspNetCore.Mvc;

namespace IceSync.Presentation.Controllers
{
    public class WorkflowsController : Controller
    {
        private readonly UniversalLoaderClient _universalLoaderClient;

        public WorkflowsController(UniversalLoaderClient universalLoaderClient)
        {
            _universalLoaderClient = universalLoaderClient;
        }

        public async Task<IActionResult> Index()
        {
            var bearer = HttpContext.Items[Constants.BearerKey] as BearerCacheData;
            var data = await _universalLoaderClient.GetWorkflowsList(bearer.Token);

            return View(data);
        }

        [Route("/Workflows/{id}/Run")]
        public async Task<bool> Run(int id)
        {
            var bearer = Request.Cookies[Constants.BearerKey];
            return await _universalLoaderClient.Run(bearer, id);
        }
    }
}