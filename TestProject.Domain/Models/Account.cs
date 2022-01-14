namespace TestProject.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }


        public Contact Contact { get; set; }

        public Guid ContactId { get; set; }

        public ICollection<Incident> Incidents { get; set; }
    }
}
