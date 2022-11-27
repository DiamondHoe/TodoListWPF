namespace TodoList.Core.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DestinatedDay { get; set; }
    }
}
