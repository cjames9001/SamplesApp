using Newtonsoft.Json;
using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Text;
using SamplesApp.Factories;
using SamplesApp.Data;

namespace SamplesApi.Controllers
{
    public class StatusController : ApiController
    {
        public HttpResponseMessage GetAllStatuses()
        {
            try
            {
                var statusFactory = new StatusFactory();
                var statuses = statusFactory.GetList(new DatabaseCommand());
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(statuses), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
