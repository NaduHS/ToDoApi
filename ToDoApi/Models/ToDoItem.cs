namespace ToDoApi.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompeletDate { get; set; }
    }
}
