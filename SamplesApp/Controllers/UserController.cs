using Newtonsoft.Json;
using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Text;
using SamplesApp.Factories;
using SamplesApp.Data;

namespace SamplesApp.Controllers
{
    public class UserController : ApiController
    {
        public HttpResponseMessage GetAllUsers()
        {
            try
            {
                var userFactory = new UserFactory();
                var users = userFactory.GetList(new DatabaseCommand());
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
