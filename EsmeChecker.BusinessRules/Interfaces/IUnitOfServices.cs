using EsmeChecker.BusinessRules.Interfaces;

namespace EsmeChecker.BusinessRules.Interfaces
{
	public interface IUnitOfServices
	{
		IEsmeDbServices EsmeDbServices { get; }
		IMainServices MainServices { get; }
		IMessageServices MessageServices { get; }
	}
}