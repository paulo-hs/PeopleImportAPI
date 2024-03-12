using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace UnitTests
{
    public class AuthServiceTest
    {
        private IAuthService _authService;

        public AuthServiceTest()
        {
            _authService = new AuthService();
        }

        [Fact]
        public void ValidKey_GetToken_ShouldReturnToken()
        {
            Environment.SetEnvironmentVariable("PRIVATE_KEY", "Teste12345678901011121314151617181920");

            string token = _authService.GetToken();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var securityToken = jsonToken as JwtSecurityToken;
            Assert.Equal("iss",securityToken.Issuer);
            Assert.Equal("aud", securityToken.Audiences.First());
        }

        [Fact]
        public void InvalidKey_GetToken_ShouldThrowInvalidKeyException()
        {
            Environment.SetEnvironmentVariable("PRIVATE_KEY", "Teste123");
                        
            Action act = () => _authService.GetToken();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(act);            
            Assert.Contains("requires a key size of at least '128' bits", exception.Message);
        }
    }
}