namespace EASY.Models
{
    public class KnowledgeXEmployee
    {
        public int KnowledgeXEmployeeID { get; set; }
        public int Level { get; set; }
        public virtual Knowledge Knowledge { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
