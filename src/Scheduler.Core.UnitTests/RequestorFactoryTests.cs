using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.Core.Communication;

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
            var instance = _requestorFactory.Create(new CredentialSettingsStub {Type = credentialType});
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
        public void GivenNotUsingCredentialSetting_WhenCreating_ThenDefault()
        {
            var instance = _requestorFactory.Create(null);
            Assert.IsInstanceOfType(instance, typeof(HttpRequestorBase));
        }

        [TestMethod]
        public void GivenInvalidCredentialsSettings_WhenCreating_ThenThrowException()
        {
            Assert.ThrowsException<NotImplementedException>(() => _requestorFactory.Create(new CredentialSettingsStub { Type = "whatever" }));
        }

    }
}
