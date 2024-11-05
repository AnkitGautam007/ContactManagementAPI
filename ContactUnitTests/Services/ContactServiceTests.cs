using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManagementAPI.Repositories;
using ContactManagementAPI.Services;
using ContactManagementAPI.Models;

namespace ContactUnitTests.Services
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly ContactService _contactService;

        public ContactServiceTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _contactService = new ContactService(_mockContactRepository.Object);
        }

        [Fact]
        public async Task AddNewContact_ShouldCallAddContactAsync()
        {
            // Arrange
            var contact = new Contact { Id = 1, FirstName = "John", LastName= "Doe", Email = "john.doe@example.com" };

            // Act
            await _contactService.AddNewContact(contact);

            // Assert
            _mockContactRepository.Verify(repo => repo.AddContactAsync(contact), Times.Once);
        }

        [Fact]
        public async Task DeleteContact_ShouldCallDeleteContactByIdAsync()
        {
            // Arrange
            int contactId = 1;

            // Act
            await _contactService.DeleteContact(contactId);

            // Assert
            _mockContactRepository.Verify(repo => repo.DeleteContactByIdAsync(contactId), Times.Once);
        }

        [Fact]
        public async Task GetAllContact_ShouldCallGetAllContactAsync()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, FirstName = "John", LastName= "Doe", Email = "john.doe@example.com" },
                new Contact { Id = 2, FirstName = "John", LastName= "Doe", Email = "jane.doe@example.com" }
            };
            _mockContactRepository.Setup(repo => repo.GetAllContactAsync()).ReturnsAsync(contacts);

            // Act
            var result = await _contactService.GetAllContact();

            // Assert
            _mockContactRepository.Verify(repo => repo.GetAllContactAsync(), Times.Once);
            Assert.Equal(contacts, result);
        }

        [Fact]
        public async Task GetContactById_ShouldCallGetContactByIdAsync()
        {
            // Arrange
            int contactId = 1;
            var contact = new Contact { Id = contactId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _mockContactRepository.Setup(repo => repo.GetContactByIdAsync(contactId)).ReturnsAsync(contact);

            // Act
            var result = await _contactService.GetContactById(contactId);

            // Assert
            _mockContactRepository.Verify(repo => repo.GetContactByIdAsync(contactId), Times.Once);
            Assert.Equal(contact, result);
        }

        [Fact]
        public async Task UpdateContact_ShouldCallUpdateContactAsync()
        {
            // Arrange
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            // Act
            await _contactService.UpdateContact(contact);

            // Assert
            _mockContactRepository.Verify(repo => repo.UpdateContactAsync(contact), Times.Once);
        }
    }
}
