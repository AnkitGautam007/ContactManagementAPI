using ContactManagementAPI.Data;
using ContactManagementAPI.Models;
using ContactManagementAPI.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactUnitTests.Repositories
{
    public class ContactRepositoryTests
    {
        private readonly Mock<DbContext> _mockDbContext;
        private readonly ContactRepository _contactRepository;

        public ContactRepositoryTests()
        {
            _mockDbContext = new Mock<DbContext>();
            _contactRepository = new ContactRepository(_mockDbContext.Object);
        }

        [Fact]
        public async Task AddContactAsync_ShouldAddContact()
        {
            // Arrange
            var contacts = new List<Contact>();
            var newContact = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _mockDbContext.Setup(db => db.GetContactsAsync()).ReturnsAsync(contacts);
            _mockDbContext.Setup(db => db.SaveContactAsync(It.IsAny<List<Contact>>())).Returns(Task.CompletedTask);

            // Act
            await _contactRepository.AddContactAsync(newContact);

            // Assert
            _mockDbContext.Verify(db => db.GetContactsAsync(), Times.Once);
            _mockDbContext.Verify(db => db.SaveContactAsync(It.Is<List<Contact>>(c => c.Contains(newContact))), Times.Once);
            Assert.Equal(1, newContact.Id);
        }

        [Fact]
        public async Task DeleteContactByIdAsync_ShouldDeleteContact()
        {
            // Arrange
            var contactId = 1;
            var contacts = new List<Contact>
            {
                new Contact { Id = contactId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" }
            };
            _mockDbContext.Setup(db => db.GetContactsAsync()).ReturnsAsync(contacts);
            _mockDbContext.Setup(db => db.SaveContactAsync(It.IsAny<List<Contact>>())).Returns(Task.CompletedTask);

            // Act
            await _contactRepository.DeleteContactByIdAsync(contactId);

            // Assert
            _mockDbContext.Verify(db => db.GetContactsAsync(), Times.Once);
            _mockDbContext.Verify(db => db.SaveContactAsync(It.Is<List<Contact>>(c => !c.Any(x => x.Id == contactId))), Times.Once);
        }

        [Fact]
        public async Task GetAllContactAsync_ShouldReturnAllContacts()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Contact { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
            };
            _mockDbContext.Setup(db => db.GetContactsAsync()).ReturnsAsync(contacts);

            // Act
            var result = await _contactRepository.GetAllContactAsync();

            // Assert
            _mockDbContext.Verify(db => db.GetContactsAsync(), Times.Once);
            Assert.Equal(contacts, result);
        }

        [Fact]
        public async Task GetContactByIdAsync_ShouldReturnContact()
        {
            // Arrange
            var contactId = 1;
            var contacts = new List<Contact>
            {
                new Contact { Id = contactId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" }
            };
            _mockDbContext.Setup(db => db.GetContactsAsync()).ReturnsAsync(contacts);

            // Act
            var result = await _contactRepository.GetContactByIdAsync(contactId);

            // Assert
            _mockDbContext.Verify(db => db.GetContactsAsync(), Times.Once);
            Assert.Equal(contacts.First(), result);
        }

        [Fact]
        public async Task UpdateContactAsync_ShouldUpdateContact()
        {
            // Arrange
            var contactId = 1;
            var contacts = new List<Contact>
            {
                new Contact { Id = contactId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" }
            };
            var updatedContact = new Contact { Id = contactId, FirstName = "Johnny", LastName = "Doe", Email = "johnny.doe@example.com" };
            _mockDbContext.Setup(db => db.GetContactsAsync()).ReturnsAsync(contacts);
            _mockDbContext.Setup(db => db.SaveContactAsync(It.IsAny<List<Contact>>())).Returns(Task.CompletedTask);

            // Act
            await _contactRepository.UpdateContactAsync(updatedContact);

            // Assert
            _mockDbContext.Verify(db => db.GetContactsAsync(), Times.Once);
            _mockDbContext.Verify(db => db.SaveContactAsync(It.Is<List<Contact>>(c => c.Contains(updatedContact))), Times.Once);
            Assert.Equal("Johnny", contacts.First().FirstName);
            Assert.Equal("johnny.doe@example.com", contacts.First().Email);
        }
    }
}
