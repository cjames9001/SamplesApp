using Converter = SamplesApi.Converters.Converter;
using System.Data;

namespace SamplesApi.Models
{
    public class Status
    {
        public Status(DataRow row)
        {
            StatusId = new Converter().ConvertTo<int>(row["StatusId"]);
            StatusName = row["Status"].ToString();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}