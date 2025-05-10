using EsmeChecker.BusinessRules.Interfaces;

namespace EsmeChecker.BusinessRules.Interfaces
{
	public interface IUnitOfServices
	{
		IEsmeDbServices EsmeDbServices { get; }
		IMainServices MainServices { get; }
		IEmploweeServcie EmploweeServcie { get; }
		IKannelService KannelService { get; }
	}
}