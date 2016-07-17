using SamplesApi.Data;
using SamplesApi.Models;
using System.Collections.Generic;
using System.Data;

namespace SamplesApi.Factories
{
    public class SampleFactory
    {
        public IEnumerable<Sample> GetList(IDatabaseCommand databaseCommand)
        {
            var query = "SELECT * FROM dbo.[Sample] " +
                    "JOIN dbo.[User] " +
                    "ON dbo.[User].UserId = Sample.CreatedBy " +
                    "JOIN dbo.[Status] " +
                    "ON Status.StatusId = Sample.StatusId";

            var dataTable = databaseCommand.GetDatatableFromQuery(query);
            return dataTable.AsEnumerable().Cast<DataRow>().Select(x => new Sample(x));
        }
    }
}