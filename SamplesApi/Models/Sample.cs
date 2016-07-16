using System;

namespace SamplesApi.Models
{
    public class Sample
    {
        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public User CreatedBy { get; set; }
        public Status Status { get; set; }
    }
}