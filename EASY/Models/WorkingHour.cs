namespace EASY.Models
{
    public class WorkingHour
    {
        public int WorkingHourID { get; set; }
        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Night { get; set; }
        public bool Dawn { get; set; }
        public bool Business { get; set; }
    }
}
