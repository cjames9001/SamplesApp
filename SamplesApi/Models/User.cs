﻿using Converter = SamplesApi.Converters.Converter;
using System.Data;

namespace SamplesApi.Models
{
    public class User
    {
        public User(DataRow row)
        {
            UserId = new Converter().ConvertTo<int>(row["UserId"]);
            FirstName = row["FirstName"].ToString();
            LastName = row["LastName"].ToString();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}