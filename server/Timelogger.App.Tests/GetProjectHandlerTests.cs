using System;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Timelogger.App.Features;
using Timelogger.DTOs;
using Timelogger.Entities;
using Timelogger.Repository.Interfaces;

namespace Timelogger.App.Tests
{
    [TestFixture]
    public class GetProjectHandlerTests
    {
            private Mock<IProjectRepository> _mockProjectRepository;
            private Mock<IMapper> _mockMapper;
            private GetProjectHandler _handler;

            [SetUp]
            public void SetUp()
            {
                _mockProjectRepository = new Mock<IProjectRepository>();
                _mockMapper = new Mock<IMapper>();
                _handler = new GetProjectHandler(_mockProjectRepository.Object, _mockMapper.Object);
            }
        
            [Test]
            public void Handle_ReturnsProjectDTO_WhenProjectExists()
            {
                // Arrange
                var projectId = 1;
                var name = "test";
                var deadline = new DateTime();

                var mockProject = new Project { Id = projectId, Name = name, Deadline = deadline};
                var mockProjectDTO = new ProjectDTO { Id = projectId, Name = name, Deadline = deadline};

                _mockProjectRepository.Setup(repo => repo.GetById(projectId)).Returns(mockProject);
                _mockMapper.Setup(mapper => mapper.Map<ProjectDTO>(mockProject)).Returns(mockProjectDTO);

                // Act
                var result = _handler.Handle(projectId);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(mockProjectDTO.Id, result.Id);
                Assert.AreEqual(mockProjectDTO.Name, result.Name);

            }
            
            [Test]
            public void Handle_ReturnsNull_WhenProjectDoesNotExist()
            {
                // Arrange
                var projectId = 1;
                _mockProjectRepository.Setup(repo => repo.GetById(projectId)).Returns((Project) null);

                // Act
                var result = _handler.Handle(projectId);

                // Assert
                Assert.IsNull(result);
            }
       
    }
} 
