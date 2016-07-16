using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using SamplesApi.Models;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SamplesApi.Controllers
{
    public class StatusController : ApiController
    {
        private static IEnumerable<Status> _tempStatuses = new List<Status>
        {
            new Status { StatusId = 0, StatusName = "Received" },
            new Status { StatusId = 1, StatusName = "Accessioning" },
            new Status { StatusId = 2, StatusName = "In Lab" },
            new Status { StatusId = 3, StatusName = "Report Generation" }
        };

        public HttpResponseMessage GetAllStatuses()
        {
            try
            {
                //Do some db call
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_tempStatuses), Encoding.UTF8, "application/json");
            return response;
        }
    }
}
