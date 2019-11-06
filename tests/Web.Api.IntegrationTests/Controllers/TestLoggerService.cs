using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.IntegrationTests.Controllers
{
    public class TestLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine("foo");
        }
    }
}
