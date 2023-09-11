using Moq;
using NUnit.Framework;
using System;
using AutoMapper;
using Timelogger.App.Features;
using Timelogger.DTOs;
using Timelogger.Entities;
using Timelogger.Repository.Interfaces;

namespace Timelogger.App.Tests
{
    
    [TestFixture]
    public class CreateTimeRegistrationHandlerTests
    {
        private Mock<ITimeRegistrationRepository> _mockTimeRegistrationRepository;
        private Mock<IProjectRepository> _mockProjectRepository;
        private Mock<IMapper> _mockMapper;
        private CreateTimeRegistrationHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockTimeRegistrationRepository = new Mock<ITimeRegistrationRepository>();
            _mockProjectRepository = new Mock<IProjectRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateTimeRegistrationHandler(_mockTimeRegistrationRepository.Object, _mockProjectRepository.Object, _mockMapper.Object);
        }

        [Test]
        public void Handle_ProjectNotFound_ReturnsError()
        {
            // Arrange
            var dto = new TimeRegistrationDTO { ProjectId = 1 };
            _mockProjectRepository.Setup(r => r.GetById(1)).Returns((Project)null);

            // Act
            var result = _handler.Handle(dto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Project not found!", result.ErrorMessage);
        }

        [Test]
        public void Handle_ProjectComplete_ReturnsError()
        {
            // Arrange
            var dto = new TimeRegistrationDTO { ProjectId = 1 };
            var project = new Project { Deadline = DateTime.Now.AddDays(-1) };
            _mockProjectRepository.Setup(r => r.GetById(1)).Returns(project);

            // Act
            var result = _handler.Handle(dto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("This project is already complete. No more time registrations accepted.", result.ErrorMessage);
        }
        
        [Test]
        public void Handle_EndDateBeforeStartDate_ReturnsError()
        {
            // Arrange
            var dto = new TimeRegistrationDTO 
            { 
                ProjectId = 1, 
                Start = DateTime.Now.AddHours(1), 
                End = DateTime.Now 
            };
            var project = new Project { Deadline = DateTime.Now.AddDays(2) };
            _mockProjectRepository.Setup(r => r.GetById(1)).Returns(project);

            // Act
            var result = _handler.Handle(dto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("End date should be after start date", result.ErrorMessage);
        }

        [Test]
        public void Handle_SpanLessThan30Minutes_ReturnsError()
        {
            // Arrange
            var dto = new TimeRegistrationDTO 
            { 
                ProjectId = 1, 
                Start = DateTime.Now, 
                End = DateTime.Now.AddMinutes(20) 
            };
            var project = new Project { Deadline = DateTime.Now.AddDays(2) };
            _mockProjectRepository.Setup(r => r.GetById(1)).Returns(project);

            // Act
            var result = _handler.Handle(dto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Span between start and end must be 30 minutes or longer", result.ErrorMessage);
        }

        [Test]
        public void Handle_EndDateAfterProjectDeadline_ReturnsError()
        {
            // Arrange
            var dto = new TimeRegistrationDTO
            {
                ProjectId = 1,
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(2)
            };
            var project = new Project { Deadline = DateTime.Now.AddDays(1) };
            _mockProjectRepository.Setup(r => r.GetById(1)).Returns(project);

            // Act
            var result = _handler.Handle(dto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("End date should be before project deadline", result.ErrorMessage);
        }

        [Test]
        public void Handle_ValidInput_CreatesTimeRegistration()
        {
            // Arrange
            var dto = new TimeRegistrationDTO { ProjectId = 1, Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            var project = new Project { Deadline = DateTime.Now.AddDays(1) };
            var timeReg = new TimeRegistration();
            var returnedTimeReg = new TimeRegistration();
            var returnedDto = new TimeRegistrationDTO();

            _mockProjectRepository.Setup(r => r.GetById(1)).Returns(project);
            _mockMapper.Setup(m => m.Map<TimeRegistration>(dto)).Returns(timeReg);
            _mockTimeRegistrationRepository.Setup(r => r.CreateTimeRegistration(timeReg)).Returns(returnedTimeReg);
            _mockMapper.Setup(m => m.Map<TimeRegistrationDTO>(returnedTimeReg)).Returns(returnedDto);

            // Act
            var result = _handler.Handle(dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(returnedDto, result.Data);
        }
    }

}