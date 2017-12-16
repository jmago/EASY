namespace EASY.Models
{
    public class BankInfo
    {
        public int BankInfoID { get; set; }
        public string HolderName { get; set; }
        public decimal Cpf { get; set; }
        public string BankName { get; set; }
        public string Agency { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
