namespace EAVDB.Models
{
    public class Record : Entity<Record>
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}