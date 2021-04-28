using Api;
using NUnit.Framework;

namespace ApiTests
{
    public class AuthenticationControllerTests
    {
        [Test]
        public void IsAuthenticated_ReturnsTrue_WhenIsAdmin()
        {
            var authenticationServiceSpy = new AuthenticationServiceSpy();
            var authenticationController = new AuthenticationController(new IdentityProviderServiceStub(), 
                authenticationServiceSpy);
            var result = authenticationController.IsAuthenticated();
            
            Assert.AreEqual(1, authenticationServiceSpy.GetNumberOfCalls());
            Assert.AreEqual("Stub Username", authenticationServiceSpy.GetUsername());
            Assert.AreEqual(true, result);
        }
    }

    public class IdentityProviderServiceStub : IIdentityProviderService
    {
        public Identity GetIdentity()
        {
            return new() { Username = "Stub Username", IdentityType = IdentityType.Admin };
        }
    }

    public class AuthenticationServiceSpy : IAuthenticationService
    {
        private int _numberOfCalls;
        private string _username;

        public bool ValidateAuthorization(string username)
        {
            _numberOfCalls++;
            _username = username;
            var authenticationService = new AuthenticationService();
            return authenticationService.ValidateAuthorization(username);
        }

        public int GetNumberOfCalls()
        {
            return _numberOfCalls;
        }

        public string GetUsername()
        {
            return _username;
        }
    }
}