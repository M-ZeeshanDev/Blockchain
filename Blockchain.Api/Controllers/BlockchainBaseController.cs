using System.Net;
using Blockchain.WebContracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blockchain.Api.Controllers
{
    public class BlockchainBaseController : ControllerBase
    {
        protected readonly ILogger Logger;
        public BlockchainBaseController(ILogger logger) : base()
        {
            Logger = logger;
        }

        protected virtual IActionResult Respond(HttpStatusCode status)
        {
            if (status == HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
            else if (status == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }

            return new StatusCodeResult((int)status);
        }

        protected virtual IActionResult Respond(HttpStatusCode status, List<ServiceError> errors)
        {
            string content = JsonConvert.SerializeObject(errors);
            return new ContentResult()
            {
                StatusCode = (int)status,
                Content = content,
                ContentType = "application/json"
            };
        }

        protected virtual IActionResult Respond(ServiceResult serviceResult)
        {
            IActionResult result;
            if (serviceResult.ErrorMessages.Any())
            {
                result = Respond(serviceResult.Status, serviceResult.ErrorMessages);
            }
            else
            {
                result = Respond(serviceResult.Status);
            }

            return result;
        }
    }
}



