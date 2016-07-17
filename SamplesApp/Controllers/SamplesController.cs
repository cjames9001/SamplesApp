using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using SamplesApp.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Data;
using SamplesApp.Factories;
using SamplesApp.Data;

namespace SamplesApp.Controllers
{
    public class SamplesController : ApiController
    {
        public HttpResponseMessage GetAllSamples()
        {
            var filter = new Func<Sample, bool>(x => true);
            return FilterSamplesForResults(filter);
        }

        public HttpResponseMessage GetSample(int id)
        {
            var filter = new Func<Sample, bool>(x => x.SampleId == id);
            return FilterSamplesForResults(filter);
        }

        [Route("api/samples/status/{status}")]
        public HttpResponseMessage GetSampleByStatus(int status)
        {
            var filter = new Func<Sample, bool>(x => x.Status.StatusId == status);
            return FilterSamplesForResults(filter);
        }

        [Route("api/samples/createdbyname/{nameToSearch}")]
        public HttpResponseMessage GetSampleByName(string nameToSearch)
        {
            var filter = new Func<Sample, bool>(x => Regex.IsMatch($"{x.CreatedBy.FirstName} {x.CreatedBy.LastName}", nameToSearch, RegexOptions.IgnoreCase));
            return FilterSamplesForResults(filter);
        }

        [Route("api/samples/create/{barcode}/{createdBy}/{status}")]
        public IHttpActionResult Create(string barcode, int createdBy, int status)
        {
            try
            {
                var parameters = new Dictionary<string, object> {
                    { "@Barcode", barcode },
                    { "@CreatedBy", createdBy },
                    { "@Status", status }
                };

                var databaseCommand = new DatabaseCommand();

                if (databaseCommand.ExecuteStoredProcedure("AddSample", parameters))
                    return Ok();
                return Content(HttpStatusCode.InternalServerError, "Sample was not added.");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private HttpResponseMessage FilterSamplesForResults(Func<Sample, bool> filter)
        {
            try
            {
                var sampleFactory = new SampleFactory();
                var samples = sampleFactory.GetList(new DatabaseCommand());
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(samples.Where(filter)), Encoding.UTF8, "application/json");
                return response;
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
