using System;
using System.Collections.Generic;
using System.Text;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.UnitTests
{
    internal class JobSettingsStub : IJobSettings
    {
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Credentials { get; set; }
        public string Endpoint { get; set; }
    }
}
