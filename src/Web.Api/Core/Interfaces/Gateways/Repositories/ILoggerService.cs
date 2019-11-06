using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface ILoggerService
    {
        void Log(string message);
    }
}
