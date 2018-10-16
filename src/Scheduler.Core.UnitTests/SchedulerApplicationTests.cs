using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.UnitTests
{
    [TestClass]
    public class SchedulerApplicationTests
    {
        private ISchedulerApplication _schedulerApplication;
        private Mock<ISchedulerConfigurator> _schedulerConfiguratorMock;
        private Mock<IConfigurationProvider> _configurationProviderMock;
        private Mock<ILogger<SchedulerApplication>> _loggerMock;

        [TestInitialize]
        public void Initialize()
        {
            _schedulerConfiguratorMock = new Mock<ISchedulerConfigurator>();
            _configurationProviderMock = new Mock<IConfigurationProvider>();
            _loggerMock = new Mock<ILogger<SchedulerApplication>>();

            _schedulerApplication = new SchedulerApplication(
                _schedulerConfiguratorMock.Object,
                _configurationProviderMock.Object,
                _loggerMock.Object
                );
        }

        [TestMethod]
        public void GivenApplicationIsStarting_WhenCallingInitializeWithSomeSettings_ThenAllJobsAreLoaded()
        {
            var jobs = new IJobSettings[]
            {
                new JobSettingsStub
                {
                    Credentials = "jwt",
                    Endpoint = "http://somedomain.com/api/my-job",
                    Expression = "cron_expression_1",
                    Name = "my-job-1"
                },
                new JobSettingsStub
                {
                    Credentials = "oauth",
                    Endpoint = "http://somedomain.com/api/my-job",
                    Expression = "cron_expression_2",
                    Name = "my-job-2"
                }
            };

            var jobSettingsMock = new Mock<IJobConfiguration>();
            jobSettingsMock.SetupGet(p => p.Jobs).Returns(jobs);

            _schedulerConfiguratorMock.Setup(m => m.Schedule(jobs[0].Name, jobs[0].Expression));
            _schedulerConfiguratorMock.Setup(m => m.Schedule(jobs[1].Name, jobs[1].Expression));
            _schedulerConfiguratorMock.Setup(m => m.Start());
            _configurationProviderMock.SetupGet(p => p.JobSettings).Returns(jobSettingsMock.Object);

            _schedulerApplication.Initialize();

            _schedulerConfiguratorMock.Verify(m => m.Schedule(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(jobs.Length));
            _schedulerConfiguratorMock.Verify(m => m.Start(), Times.Once);
        }

        [TestMethod]
        public void GivenApplicationIsStarting_WhenCallingInitializeWithoutSettings_ThenNoneAreLoaded()
        {
            var jobSettingsMock = new Mock<IJobConfiguration>();
            jobSettingsMock.SetupGet(p => p.Jobs).Returns(() => new IJobSettings[0]);
            _configurationProviderMock.SetupGet(p => p.JobSettings).Returns(jobSettingsMock.Object);

            _schedulerApplication.Initialize();

            _schedulerConfiguratorMock.Verify(m => m.Schedule(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _schedulerConfiguratorMock.Verify(m => m.Start(), Times.Never);
        }
    }
}
