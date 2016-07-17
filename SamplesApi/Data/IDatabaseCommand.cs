using System.Collections.Generic;
using System.Data;

namespace SamplesApi.Data
{
    public interface IDatabaseCommand
    {
        DataTable GetDatatableFromQuery(string query);
        bool ExecuteStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters);
    }
}