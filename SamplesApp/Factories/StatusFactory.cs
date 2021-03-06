﻿using SamplesApp.Data;
using SamplesApp.Models;
using System.Collections.Generic;
using System.Data;

namespace SamplesApp.Factories
{
    public class StatusFactory
    {
        public IEnumerable<Status> GetList(IDatabaseCommand databaseCommand)
        {
            var query = "SELECT * FROM dbo.[Status]";
            var dataTable = databaseCommand.GetDatatableFromQuery(query);
            return dataTable.AsEnumerable().Cast<DataRow>().Select(x => new Status(x));
        }
    }
}