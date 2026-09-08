namespace ServerApp.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}