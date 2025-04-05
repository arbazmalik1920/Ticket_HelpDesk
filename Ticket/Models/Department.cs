namespace Ticket.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Key Relation
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
