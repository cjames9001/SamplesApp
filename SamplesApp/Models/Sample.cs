using Converter = SamplesApp.Converters.Converter;
using System;
using System.Data;

namespace SamplesApp.Models
{
    public class Sample
    {
        public Sample(DataRow row)
        {
            SampleId = new Converter().ConvertTo<int>(row["SampleId"]);
            Barcode = row["Barcode"].ToString();
            CreatedAt = new Converter().ConvertTo<DateTime>(row["CreatedAt"]);
            CreatedBy = new User(row);
            Status = new Status(row);
        }

        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public User CreatedBy { get; set; }
        public Status Status { get; set; }
    }
}