using System;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public class LoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
