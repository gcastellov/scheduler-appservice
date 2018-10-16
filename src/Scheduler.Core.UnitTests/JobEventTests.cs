using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quartz;
using Scheduler.Core.Communication;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.UnitTests
{
    [TestClass]
    public class JobEventTests
    {
        private ICredentialSettings _credentialSettings;
        private IJobSettings _jobSettings;
        private const string JobName = "my-job";
        private const string CredentialId = "jwt-credentail";

        [TestInitialize]
        public void Initialize()
        {
            _credentialSettings = new CredentialSettingsStub
            {
                Id = CredentialId,
                Type = "jwt",
                Endpoint = "http://somedomain.com/api/token",
                Username = "my-username",
                Password = "my-password",
                ResponseTokenPayload = "Token",
                ResponseType = "Scheduler.Core.Communication.AuthorizationResponse, Scheduler.Core"
            };

            _jobSettings = new JobSettingsStub
            {
                Credentials = CredentialId,
                Endpoint = "http://somedomain.com/api/my-job",
                Expression = "cron_expression_1",
                Name = JobName
            };
        }


        [TestMethod]
        public async Task GivenJobWithAuthentication_WhenExecuting_ThenAuthenticateAndFireJob()
        {
            var configurationProviderMock = new Mock<IConfigurationProvider>();
            configurationProviderMock.Setup(p => p.GetJobSettings(JobName)).Returns(_jobSettings);
            configurationProviderMock.Setup(p => p.GetCredentials(CredentialId)).Returns(_credentialSettings);

            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var loggerMock = new Mock<ILogger<JobEvent>>();
            loggerFactoryMock.Setup(m => m.CreateLogger(It.IsAny<string>())).Returns(loggerMock.Object);

            var requestorMock = new Mock<IHttpRequestor>();
            requestorMock.Setup(m =>
                    m.Authorize(_credentialSettings.Username, _credentialSettings.Password, _credentialSettings.Endpoint))
                .Returns(Task.CompletedTask);
            requestorMock.Setup(m => m.Post(_jobSettings.Endpoint))
                .Returns(Task.FromResult<HttpResponseMessage>(null));

            var requestorFactoryMock = new Mock<IRequestorFactory>();
            requestorFactoryMock.Setup(m => m.Create(_credentialSettings)).Returns(requestorMock.Object);

            var schedulerMock = new Mock<IScheduler>();
            schedulerMock.SetupGet(p => p.Context).Returns(new SchedulerContext(new Dictionary<string, object>
            {
                { SchedulerConfigurator.ConfigurationKey, configurationProviderMock.Object },
                { SchedulerConfigurator.LoggerContextKey, loggerFactoryMock.Object },
                { SchedulerConfigurator.RequestorKey, requestorFactoryMock.Object }
            }));

            var jobContextMock = new Mock<IJobExecutionContext>();
            jobContextMock.SetupGet(p => p.Scheduler).Returns(schedulerMock.Object);
            jobContextMock.SetupGet(p => p.Trigger.Description).Returns(JobName);
            
            var job = new JobEvent();
            await job.Execute(jobContextMock.Object);

            requestorMock.Verify(m => 
                m.Authorize(_credentialSettings.Username, _credentialSettings.Password, _credentialSettings.Endpoint), 
                Times.Once);
            requestorMock.Verify(m => m.Post(_jobSettings.Endpoint), Times.Once);
        }

        [TestMethod]
        public async Task GivenJobWithoutAuthentication_WhenExecuting_ThenJustFireJob()
        {
            _jobSettings.Credentials = null;

            var configurationProviderMock = new Mock<IConfigurationProvider>();
            configurationProviderMock.Setup(p => p.GetJobSettings(JobName)).Returns(_jobSettings);
            configurationProviderMock.Setup(p => p.GetCredentials(CredentialId)).Returns(_credentialSettings);

            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var loggerMock = new Mock<ILogger<JobEvent>>();
            loggerFactoryMock.Setup(m => m.CreateLogger(It.IsAny<string>())).Returns(loggerMock.Object);

            var requestorMock = new Mock<IHttpRequestor>();
            requestorMock.Setup(m => m.Post(_jobSettings.Endpoint)).Returns(Task.FromResult<HttpResponseMessage>(null));

            var requestorFactoryMock = new Mock<IRequestorFactory>();
            requestorFactoryMock.Setup(m => m.Create(null)).Returns(requestorMock.Object);

            var schedulerMock = new Mock<IScheduler>();
            schedulerMock.SetupGet(p => p.Context).Returns(new SchedulerContext(new Dictionary<string, object>
            {
                { SchedulerConfigurator.ConfigurationKey, configurationProviderMock.Object },
                { SchedulerConfigurator.LoggerContextKey, loggerFactoryMock.Object },
                { SchedulerConfigurator.RequestorKey, requestorFactoryMock.Object }
            }));

            var jobContextMock = new Mock<IJobExecutionContext>();
            jobContextMock.SetupGet(p => p.Scheduler).Returns(schedulerMock.Object);
            jobContextMock.SetupGet(p => p.Trigger.Description).Returns(JobName);

            var job = new JobEvent();
            await job.Execute(jobContextMock.Object);

            requestorMock.Verify(m => m.Authorize(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            requestorMock.Verify(m => m.Post(_jobSettings.Endpoint), Times.Once);
        }
    }
}