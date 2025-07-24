using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using AuthApi.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AuthApi.Tests
{
    public class LoginTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public LoginTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        public async Task Login_WithValidCredentials_ReturnsOk()
        {
            var loginRequest = new LoginRequest
            {
                Username = "admin",
                Password = "admin123"
            };

            var response = await _client.PostAsJsonAsync("/api/auth/login", loginRequest);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            Assert.NotNull(result?.Username);
        }
    }
}   