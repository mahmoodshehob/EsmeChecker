namespace EsmeChecker.Models.Ussd
{

	public partial class MultiRequestUSSD
	{
		public string TransactionId { get; set; }
		public string TransactionTime { get; set; }
		public string MSISDN { get; set; }
		public string USSDServiceCode { get; set; }
		public string USSDRequestString { get; set; }
		public string Response { get; set; }
	}
}
