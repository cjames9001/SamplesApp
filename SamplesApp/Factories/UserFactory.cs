﻿using SamplesApp.Data;
using SamplesApp.Models;
using System.Collections.Generic;
using System.Data;

namespace SamplesApp.Factories
{
    public class UserFactory
    {
        public IEnumerable<User> GetList(IDatabaseCommand databaseCommand)
        {
            var query = "SELECT * FROM dbo.[User]";
            var dataTable = databaseCommand.GetDatatableFromQuery(query);
            return dataTable.AsEnumerable().Cast<DataRow>().Select(x => new User(x));
        }
    }
}