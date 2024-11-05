using ContactManagementAPI.Controllers;
using ContactManagementAPI.Models;
using ContactManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactUnitTests.Controller
{
    public class ContactControllerTests
    {
        private readonly Mock<IContactService> _mockContactService;
        private readonly ContactController _controller;

        public ContactControllerTests()
        {
            _mockContactService = new Mock<IContactService>();
            _controller = new ContactController(_mockContactService.Object);
        }

        [Fact]
        public async Task GetContactList_ReturnsOkResult_WithListOfContacts()
        {
            // Arrange
            var contacts = new List<Contact>
        {
            new Contact { Id = 1, FirstName = "Anil", LastName = "Kumar", Email = "anil.kumar@gmail.com" },
            new Contact { Id = 2, FirstName = "Sunil", LastName = "Sharma", Email = "sunil.sharma@gmail.com" }
        };
            _mockContactService.Setup(service => service.GetAllContact()).ReturnsAsync(contacts);

            // Act
            var result = await _controller.GetContactList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Contact>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetContact_ReturnsOkResult_WithContact()
        {
            // Arrange
            var contact = new Contact { Id = 1, FirstName = "Anil", LastName = "Kumar", Email = "anil.kumar@gmail.com" };
            _mockContactService.Setup(service => service.GetContactById(1)).ReturnsAsync(contact);

            // Act
            var result = await _controller.GetContact(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}
