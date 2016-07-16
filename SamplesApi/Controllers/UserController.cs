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
    public class UserController : ApiController
    {
        private static IEnumerable<User> _tempUsers = new List<User>
        {
            new User { FirstName = "Clint", LastName = "Reid", UserId = 6 },
            new User { FirstName = "Kim", LastName = "Mullins", UserId = 7 }
        };

        public HttpResponseMessage GetAllUsers()
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
            response.Content = new StringContent(JsonConvert.SerializeObject(_tempUsers), Encoding.UTF8, "application/json");
            return response;
        }
    }
}
