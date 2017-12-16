namespace EASY.Models
{
    public class Availability
    {
        public int AvailabilityID { get; set; }
        public bool Until4Hours { get; set; }
        public bool From4To6Hours { get; set; }
        public bool From6To8Hours { get; set; }
        public bool Upto8Hours { get; set; }
    }
}
