using Application.DTO;
using Application.Services;
using Domain.Entities;
using Moq;
using Persistence.Interfaces;
using Xunit;
using Mapster;
using System.Linq.Expressions;

namespace Application.Tests.Services
{
    public class ExhibitionServiceTests
    {
        private readonly Mock<IExhibitionRepository> _mockExhibitionRepository;
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly ExhibitionService _exhibitionService;

        public ExhibitionServiceTests()
        {
            _mockExhibitionRepository = new Mock<IExhibitionRepository>();
            _mockBookRepository = new Mock<IBookRepository>();
            _exhibitionService = new ExhibitionService(_mockExhibitionRepository.Object, _mockBookRepository.Object);
        }

        [Fact]
        public async Task CreateExhibition_ValidData_ReturnsSuccess()
        {
            // Arrange
            var exhibitionDto = new ExhibitionDto { Name = "Test Exhibition" };

            // Act
            var result = await _exhibitionService.CreateExhibition(exhibitionDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.Message);
            Assert.Equal(exhibitionDto, result.Exhibition);
            _mockExhibitionRepository.Verify(x => x.CreateExhibition(It.IsAny<Exhibition>()), Times.Once);
        }

        [Fact]
        public async Task CreateExhibition_InvalidData_ReturnsFailure()
        {
            // Arrange
            var exhibitionDto = new ExhibitionDto { Name = "" }; // Invalid data

            // Act
            var result = await _exhibitionService.CreateExhibition(exhibitionDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid exhibition data", result.Message);
            _mockExhibitionRepository.Verify(x => x.CreateExhibition(It.IsAny<Exhibition>()), Times.Never);
        }

        [Fact]
        public async Task GetExhibition_ExistingExhibition_ReturnsExhibition()
        {
            // Arrange
            var exhibition = new Exhibition { Id = 1, Name = "Test Exhibition" };
            _mockExhibitionRepository.Setup(x => x.GetExhibition(1)).ReturnsAsync(exhibition);

            // Act
            var result = await _exhibitionService.GetExhibition(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(exhibition.Adapt<ExhibitionDto>(), result);
        }

        [Fact]
        public async Task GetExhibition_NonExistingExhibition_ReturnsNull()
        {
            // Arrange
            _mockExhibitionRepository.Setup(x => x.GetExhibition(1)).ReturnsAsync((Exhibition)null);

            // Act
            var result = await _exhibitionService.GetExhibition(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetExhibitions_ReturnsPaginatedExhibitions()
        {
            // Arrange
            var exhibitions = new List<Exhibition>
            {
                new Exhibition { Id = 1, Name = "Exhibition 1" },
                new Exhibition { Id = 2, Name = "Exhibition 2" },
                new Exhibition { Id = 3, Name = "Exhibition 3" }
            };
            _mockExhibitionRepository.Setup(x => x.GetExhibitions()).ReturnsAsync(exhibitions);

            // Act
            var result = await _exhibitionService.GetExhibitions(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.ItemsCount);
            Assert.Equal(50, result.PageLimit);
            Assert.Equal(1, result.Page);
            Assert.Single(result.Pages);
            Assert.Equal(exhibitions.Select(e => e.Adapt<ExhibitionDto>()).ToList(), result.Exhibitions);



            // Test full list request with page = -1
            result = await _exhibitionService.GetExhibitions(-1);
            Assert.NotNull(result);
            Assert.Equal(3, result.ItemsCount);
            Assert.Equal(exhibitions.Select(e => e.Adapt<ExhibitionDto>()).ToList(), result.Exhibitions);

        }


        [Fact]
        public async Task AddBookInExhibition_ValidData_ReturnsSuccess()
        {
            // Arrange
            var exhibition = new Exhibition { Id = 1, Name = "Test Exhibition" };
            var book = new Book { Id = 1, Title = "Test Book" };
            _mockExhibitionRepository.Setup(x => x.GetExhibition(1)).ReturnsAsync(exhibition);
            _mockBookRepository.Setup(x => x.GetBook(1)).ReturnsAsync(book);

            // Act
            var result = await _exhibitionService.AddBookInExhibition(1, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.Message);
            _mockExhibitionRepository.Verify(x => x.EditExhibition(exhibition), Times.Once);
            Assert.Contains(book, exhibition.Books);
        }

        [Fact]
        public async Task AddBookInExhibition_NonExistingExhibition_ReturnsFailure()
        {
            _mockExhibitionRepository.Setup(repo => repo.GetExhibition(It.IsAny<int>())).ReturnsAsync((Exhibition)null);

            var (isSuccess, message) = await _exhibitionService.AddBookInExhibition(1, 1);

            Assert.False(isSuccess);
            Assert.Equal("Exhibition not found", message);
        }

        [Fact]
        public async Task AddBookInExhibition_NonExistingBook_ReturnsFailure()
        {
            var exhibition = new Exhibition { Id = 1 };
            _mockExhibitionRepository.Setup(repo => repo.GetExhibition(It.IsAny<int>())).ReturnsAsync(exhibition);
            _mockBookRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).ReturnsAsync((Book)null);

            var (isSuccess, message) = await _exhibitionService.AddBookInExhibition(1, 1);


            Assert.False(isSuccess);
            Assert.Equal("Book not found", message);
        }




        [Fact]
        public async Task RemoveBookFromExhibition_ValidData_ReturnsSuccess()
        {
            // Arrange
            var exhibition = new Exhibition { Id = 1, Name = "Test Exhibition" };
            var book = new Book { Id = 1, Title = "Test Book" };
            exhibition.Books.Add(book);
            _mockExhibitionRepository.Setup(x => x.GetExhibition(1)).ReturnsAsync(exhibition);
            _mockBookRepository.Setup(x => x.GetBook(1)).ReturnsAsync(book);

            // Act
            var result = await _exhibitionService.RemoveBookFromExhibition(1, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.Message);
            _mockExhibitionRepository.Verify(x => x.EditExhibition(exhibition), Times.Once);
            Assert.DoesNotContain(book, exhibition.Books);
        }


        [Fact]
        public async Task RemoveBookFromExhibition_NonExistingExhibition_ReturnsFailure()
        {
            _mockExhibitionRepository.Setup(repo => repo.GetExhibition(It.IsAny<int>())).ReturnsAsync((Exhibition)null);

            var (isSuccess, message) = await _exhibitionService.RemoveBookFromExhibition(1, 1);

            Assert.False(isSuccess);
            Assert.Equal("Exhibition not found", message);
        }

        [Fact]
        public async Task RemoveBookFromExhibition_NonExistingBook_ReturnsFailure()
        {
            var exhibition = new Exhibition { Id = 1 };
            _mockExhibitionRepository.Setup(repo => repo.GetExhibition(It.IsAny<int>())).ReturnsAsync(exhibition);
            _mockBookRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).ReturnsAsync((Book)null);

            var (isSuccess, message) = await _exhibitionService.RemoveBookFromExhibition(1, 1);

            Assert.False(isSuccess);
            Assert.Equal("Book not found", message);
        }



    }
}
