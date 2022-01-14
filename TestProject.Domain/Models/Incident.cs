﻿namespace TestProject.Domain.Models
{
    public class Incident
    {
        public string Name { get; set; }

        public string Description { get; set; }


        public Guid AccountId { get; set; }

        public Account Account { get; set; }
    }
}