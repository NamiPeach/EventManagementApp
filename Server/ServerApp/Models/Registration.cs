namespace ServerApp.Models
{
    public class Registration
    {
        public int ID { get; set; }
        public int OrganizerID { get; set; }
        public int EventID { get; set; }
        public string ParticipantName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public Organizer? Organizer { get; set; }
        public Event? Event { get; set; }
    }
}