using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.Core.Communication;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.UnitTests
{
    [TestClass]
    public class RequestorFactoryTests
    {
        private IRequestorFactory _requestorFactory;

        [TestInitialize]
        public void Initialize()
        {
            _requestorFactory = new RequestorFactory();
        }

        [DataRow("jwt")]
        [DataRow("oauth")]
        [TestMethod]
        public void GivenCredentialsSettings_WhenCreating_ThenGetInstance(string credentialType)
        {
            var credentials = GetSettings(credentialType);

            var instance = _requestorFactory.Create(credentials);

            Assert.IsNotNull(instance);
            if (credentialType == "jwt")
            {
                Assert.IsInstanceOfType(instance, typeof(JwtHttpRequestor));
            }
            if (credentialType == "oauth")
            {
                Assert.IsInstanceOfType(instance, typeof(OAuthHttpRequestor));
            }
        }

        [TestMethod]
        public void GivenNotUsingCredentialSetting_WhenCreating_ThenGetDefaultInstance()
        {
            var instance = _requestorFactory.Create(null);
            Assert.IsInstanceOfType(instance, typeof(HttpRequestorBase));
        }

        [TestMethod]
        public void GivenInvalidCredentialsSettings_WhenCreating_ThenThrowException()
        {
            var credentials = GetSettings("whatever");
            Assert.ThrowsException<NotImplementedException>(() => _requestorFactory.Create(credentials));
        }

        [DataRow(null)]
        [DataRow(" ")]
        [DataRow("")]
        [DataRow("Scheduler.Core.UnitTests.InvalidNamespace, Scheduler.Core.UnitTests")]        
        [TestMethod]
        public void GivenInvalidResponseReaderTye_WhenCreating_ThenThrowException(string responseReaderType)
        {
            var credentials = new CredentialSettingsStub {ResponseReader = responseReaderType};
            Assert.ThrowsException<InvalidOperationException>(() => _requestorFactory.Create(credentials));
        }

        private ICredentialSettings GetSettings(string credentialType)
        {
            return new CredentialSettingsStub
            {
                Type = credentialType,
                ResponseReader = "Scheduler.Core.UnitTests.ResponseReaderStub, Scheduler.Core.UnitTests"
            };
        }        
    }
}
