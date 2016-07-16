using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using SamplesApi.Models;
using User = SamplesApi.Models.User;

namespace SamplesApi.Controllers
{
    public class SamplesController : ApiController
    {
        private static IEnumerable<Status> _tempStatuses = new List<Status>
        {
            new Status { StatusId = 0, StatusName = "Received" },
            new Status { StatusId = 1, StatusName = "Accessioning" },
            new Status { StatusId = 2, StatusName = "In Lab" },
            new Status { StatusId = 3, StatusName = "Report Generation" }
        };

        private static IEnumerable<User> _tempUsers = new List<User>
        {
            new User { FirstName = "Clint", LastName = "Reid", UserId = 6 },
            new User { FirstName = "Kim", LastName = "Mullins", UserId = 7 }
        };

        private IEnumerable<Sample> _tempSamples = new List<Sample>
        {
            new Sample { SampleId = 1, Barcode = "129076", CreatedAt = new DateTime(2015, 01, 02), CreatedBy = _tempUsers.FirstOrDefault(x => x.UserId == 6), Status = _tempStatuses.FirstOrDefault(x => x.StatusId == 3) },
            new Sample { SampleId = 2, Barcode = "850314", CreatedAt = new DateTime(2015, 6, 15), CreatedBy = _tempUsers.FirstOrDefault(x => x.UserId == 7), Status = _tempStatuses.FirstOrDefault(x => x.StatusId == 3) },
            new Sample { SampleId = 3, Barcode = "176033", CreatedAt = new DateTime(2015, 7, 31), CreatedBy = _tempUsers.FirstOrDefault(x => x.UserId == 7), Status = _tempStatuses.FirstOrDefault(x => x.StatusId == 0) }
        };

        public IHttpActionResult GetAllSamples()
        {
            var filter = new Func<Sample, bool>(x => true);
            return FilterSamplesForResults(filter);
        }

        public IHttpActionResult GetSample(int id)
        {
            var filter = new Func<Sample, bool>(x => x.SampleId == id);
            return FilterSamplesForResults(filter);
        }

        [Route("api/samples/status/{status}")]
        public IHttpActionResult GetSampleByStatus(int status)
        {
            var filter = new Func<Sample, bool>(x => x.Status.StatusId == status);
            return FilterSamplesForResults(filter);
        }

        [Route("api/samples/createdbyname/{nameToSearch}")]
        public IHttpActionResult GetSampleByName(string nameToSearch)
        {
            var filter = new Func<Sample, bool>(x => Regex.IsMatch(x.CreatedBy.FirstName, nameToSearch, RegexOptions.IgnoreCase) || Regex.IsMatch(x.CreatedBy.LastName, nameToSearch, RegexOptions.IgnoreCase));
            return FilterSamplesForResults(filter);
        }

        private IHttpActionResult FilterSamplesForResults(Func<Sample, bool> filter)
        {
            var samples = _tempSamples.Where(filter).ToList();
            if (!samples.Any())
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(samples));
        }
    }
}
