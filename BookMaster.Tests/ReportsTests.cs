using Application.DTO;
using Application.Services;
using Domain.Entities;
using Mapster;
using Moq;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Services
{
    public class ClientCardServiceTests
    {
        [Fact]
        public async Task GetBookReminders_ReturnsListOfCirculationRecords()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard { Issues = new List<Issue> { new Issue { BookId = 1, IssueFrom = DateTime.Now, IssueTo = DateTime.Now.AddDays(7) } } },
                new ClientCard { Issues = new List<Issue> { new Issue { BookId = 2, IssueFrom = DateTime.Now, IssueTo = DateTime.Now.AddDays(7) }, new Issue { BookId = 3, IssueFrom = DateTime.Now, IssueTo = DateTime.Now.AddDays(7) } } }
            };
            _mockClientCardRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientCardService.GetBookReminders();

            // Assert
            Assert.Equal(3, result.Count);
            // Add assertions to check properties of CirculationRecord if necessary
            Assert.Equal(1, result[0].BookId);

        }

        [Fact]
        public async Task GetBookCirculationHistory_ReturnsListOfCirculationRecordsForSpecificBook()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard
                {
                    Returns = new List<Issue>
                    {
                        new Issue { BookId = 1, IssueFrom = DateTime.Now.AddDays(-5), IssueTo = DateTime.Now.AddDays(-5).AddDays(7), ReturnDate = DateTime.Now.AddDays(-2) },
                        new Issue { BookId = 2, IssueFrom = DateTime.Now.AddDays(-3), IssueTo = DateTime.Now.AddDays(-3).AddDays(7), ReturnDate = DateTime.Now.AddDays(-1) }
                    }
                },
                new ClientCard
                {
                    Returns = new List<Issue>
                    {
                        new Issue { BookId = 1, IssueFrom = DateTime.Now.AddDays(-10), IssueTo = DateTime.Now.AddDays(-10).AddDays(7), ReturnDate = DateTime.Now.AddDays(-7) },
                        new Issue { BookId = 3, IssueFrom = DateTime.Now.AddDays(-1), IssueTo = DateTime.Now.AddDays(-1).AddDays(7), ReturnDate = DateTime.Now }
                    }
                }
            };

            _mockClientCardRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientCardService.GetBookCirculationHistory(1);


            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(DateTime.Now.AddDays(-2), result[0].ReturnDate); // Verify sorting by IssueFrom descending and data
            Assert.Equal(DateTime.Now.AddDays(-7), result[1].ReturnDate);
        }



        [Fact]
        public async Task GetBookCirculationHistory_NoMatchingBookId_ReturnsEmptyList()
        {
            // Arrange
             var clients = new List<ClientCard>
            {
                new ClientCard
                {
                    Returns = new List<Issue>
                    {
                        new Issue { BookId = 2, IssueFrom = DateTime.Now.AddDays(-5) },
                    }
                },
            };
            _mockClientCardRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientCardService.GetBookCirculationHistory(1); //Searching for bookId=1

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBookCirculationHistory_EmptyReturnsList_ReturnsEmptyList()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard
                {
                    Returns = new List<Issue>()
                },
            };
            _mockClientCardRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);


            // Act
            var result = await _clientCardService.GetBookCirculationHistory(1);

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public async Task GetBookCirculationHistory_NullReturnsList_ReturnsEmptyList()
        {
            // Arrange
            var client = new ClientCard();
            client.Returns = null!; //Setting explicitly to null for test case
            var clients = new List<ClientCard>() { client };

            _mockClientCardRepository.Setup(x => x.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientCardService.GetBookCirculationHistory(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBookReminders_ReturnsListOfCirculationRecords()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard { Issues = new List<Issue> { new Issue { BookId = 1 } } },
                new ClientCard { Issues = new List<Issue> { new Issue { BookId = 2 }, new Issue { BookId = 3 } } }
            };
            _mockClientCardRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientCardService.GetBookReminders();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetBookCirculationHistory_ReturnsListOfCirculationRecordsForSpecificBook()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard
                {
                    Returns = new List<Issue>
                    {
                        new Issue { BookId = 1, IssueFrom = DateTime.Now.AddDays(-5) },
                        new Issue { BookId = 2, IssueFrom = DateTime.Now.AddDays(-3) }
                    }
                },
                new ClientCard
                {
                    Returns = new List<Issue>
                    {
                        new Issue { BookId = 1, IssueFrom = DateTime.Now.AddDays(-10) },
                        new Issue { BookId = 3, IssueFrom = DateTime.Now.AddDays(-1) }
                    }
                }
            };

            _mockClientCardRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientCardService.GetBookCirculationHistory(1);


            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(DateTime.Now.AddDays(-5), result[0].IssueFrom);
            Assert.Equal(DateTime.Now.AddDays(-10), result[1].IssueFrom);
        }

        [Fact]
        public async Task GetBookCirculationHistory_ReturnsEmptyList_WhenNoHistoryFound()
        {

            _mockClientCardRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(new List<ClientCard>());

            var result = await _clientCardService.GetBookCirculationHistory(1);

            Assert.Empty(result);
        }
    }
}
