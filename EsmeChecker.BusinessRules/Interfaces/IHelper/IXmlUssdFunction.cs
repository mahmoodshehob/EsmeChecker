using EsmeChecker.Models.Ussd;

namespace EsmeChecker.BusinessRules.Interfaces.IHelper
{
	public interface IXmlUssdFunction
	{
		Task<MultiRequestUSSD> Parse(string xmlString);
		string Response_Success(MultiResponseUSSD multiResponse);
		string Response_Fault(MultiResponseUSSD multiResponse);
	}
}
