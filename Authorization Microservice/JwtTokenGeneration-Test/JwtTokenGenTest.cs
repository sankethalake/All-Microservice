using JwtAuthentication;
using JwtAuthentication.Models;
using NUnit.Framework;

namespace JwtAuthentication_Test
{
    [TestFixture]
    public class JwtTokenTests
    {
        private IJwtAuthenticationManager jwtAuthenticationManager;
        [SetUp]
        public void Setup()
        {
            jwtAuthenticationManager = new JwtAuthenticationManager("ys5bjLWh81C2H0AIT6blryt0ut1eP7g4");
        }

        [Test]
        public void GenerateToken_ReturnsTokenInStringFormat()
        {
            var token = jwtAuthenticationManager.GenerateToken("User", "Password");
            Assert.IsNotEmpty(token);
        }
        [TearDown]
        public void TearDown()
        {
            jwtAuthenticationManager = null;
        }
    }
}