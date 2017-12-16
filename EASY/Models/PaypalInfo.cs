namespace EASY.Models
{
    public class PaypalInfo
    {
        public int PaypalInfoID { get; set; }
        public string Description { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
