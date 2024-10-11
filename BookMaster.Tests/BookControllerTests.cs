using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using Moq;
namespace BookMaster.Tests
{
    public class BookSearchIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BookSearchIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void SearchBooks_ByTitle_ReturnsCorrectResult()
        {
            var response = await _client.GetAsync("http://localhost:8080/api/Books/search?title=Harry");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Harry Potter", responseContent);
        }

        [Fact]
        public async void SearchBooks_ByAuthor_ReturnsCorrectResult()
        {
            var response = await _client.GetAsync("http://localhost:8080/api/Books/search?author=Rowling");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("J.K. Rowling", responseContent);
        }

        [Fact]
        public async void SearchBooks_BySubject_ReturnsCorrectResult()
        {
            var response = await _client.GetAsync("http://localhost:8080/api/Books/search?subject=Fantasy");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("Fantasy", responseContent);
        }

        [Fact]
        public async void SearchBooks_NoMatch_ReturnsEmpty()
        {
            var response = await _client.GetAsync("http://localhost:8080/api/Books/search?title=NonExistingTitle");

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.DoesNotContain("Harry Potter", responseContent);
        }
    }
}
